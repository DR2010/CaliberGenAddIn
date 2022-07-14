using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using EA;
using EAAddIn.Applications.AuditConverter;
using File = System.IO.File;

namespace EAAddIn.Windows
{
    public partial class ExtractAndClearAuditLogs : Form
    {
        private readonly AuditConverter auditConverter = new AuditConverter();

        private Repository _repository;


        public ExtractAndClearAuditLogs()
        {
            InitializeComponent();
            auditConverter.InitialaseExtract();
            ApplyAuditDefaults();
        }

        #region Extract Logs

        private void ApplyAuditDefaults()
        {
            dtpFrom.Value = auditConverter.FromDate;
            dtpFrom.Checked = auditConverter.IncludeFromDate;
            dtpTo.Text = auditConverter.ToDate.ToString(@"dd/MM/yy hh:mm:ss tt");
            dtpTo.Value = auditConverter.ToDate;
            dtpTo.Text = auditConverter.ToDate.ToString(@"dd/MM/yy hh:mm:ss tt");


            tbPathLocation.Text = auditConverter.SaveAuditFilePath;
            tbAuditFileName.Text = auditConverter.SaveAuditFileFullName;

            cbSaveLogs.Checked = auditConverter.SaveAudit;
            cbClearLogs.Checked = auditConverter.ClearAudit;

            _repository = auditConverter.Repository;
        }

        private void DtpValueChanged(object sender, EventArgs e)
        {
            tbAuditFileName.Text = Path.Combine(tbPathLocation.Text,
                                                AuditConverter.Createfilename(dtpFrom.Value, dtpTo.Value,
                                                                              dtpFrom.Checked));
        }

        private void BtnOpenFileDialogueClick(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog
                          {
                              DefaultExt = "xml",
                              AddExtension = true,
                              RestoreDirectory = true,
                              Title = @"Select a location to save the Audit XML file to",
                              FileName = tbAuditFileName.Text,
                              InitialDirectory = tbPathLocation.Text
                          };


            if (sfd.ShowDialog() == DialogResult.OK)
            {
                tbAuditFileName.Text = sfd.FileName;
            }
        }

        private void BtnExtractAndClearLogsClick(object sender, EventArgs e)
        {
            ExtractEALogs();
        }

        private bool ExtractEALogs(bool quiet = false)
        {
            Cursor.Current = Cursors.WaitCursor;

            tbresults.Text = "Extracting Logs";
            tbresults.Text = tbresults.Text + "...";
            var messages = new List<Message>();
            var file = tbAuditFileName.Text;

            if (File.Exists(file))
            {
                if ( quiet ||
                    MessageBox.Show(
                        "File " + file + " exists and will be deleted." + Environment.NewLine +
                        Environment.NewLine + "Continue?", "Extract and Clear Audit Logs", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
                {
                    Cursor.Current = Cursors.Default;
                    return false;
                }
                else
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (IOException ex)
                    {
                        file = Path.GetTempFileName();
                        tbAuditFileName.Text = file;
                        MessageBox.Show("Error deleting file.  A randomly generated file name will be used (" + file + ")" + Environment.NewLine + Environment.NewLine + ex.Message, "Extract and Clear Audit Logs", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
            }

            SetResultsStatus("Extracting audit logs...");

            DateTime? fromDate;
            
            if (dtpFrom.Checked)
                fromDate = dtpFrom.Value;
            else 
                fromDate =  null;

             
            bool auditExtractOk = AuditConverter.ExtractAuditLogs(file, fromDate, dtpTo.Value , ref messages);
            
            if (auditExtractOk)
            {
                SetResultsStatus("Logs extracted ok.");
                tbXMLToLoadfileName.Text = file;
            }
            else
            {
                SetResultsStatus("Error Extracting Logs");
            }
            ShowMessages(messages);
            Cursor.Current = Cursors.Default;

            return auditExtractOk;
        }

        #endregion

        #region Convert XML Logs and Update DB

        private void btnOpenFDForLogToConver_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
                                     {
                                         InitialDirectory = @"c:\temp",
                                         Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*",
                                         FilterIndex = 1,
                                         RestoreDirectory = true
                                     };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbXMLToLoadfileName.Text = openFileDialog.FileName;
            }
        }

        private void btnConvertLog_Click(object sender, EventArgs e)
        {
            ConvertXmlLog();
        }

        private bool ConvertXmlLog()
        {
            Cursor.Current = Cursors.WaitCursor;

            toolStripProgressBar.Visible = true;
            var messages = new List<Message>();
            SetResultsStatus("Converting (Unzipping) XML Logs and saving to the database...");

            bool ok = AuditConverter.ConvertAndSaveLogToDb(tbXMLToLoadfileName.Text, SetResultsStatus, ref messages, System.IO.File.GetCreationTime(tbXMLToLoadfileName.Text), UpdateProgress);

            SetResultsStatus(ok
                                 ? "Logs converted and saved to database succesfully"
                                 : "Error occurred during conversion.  Manual cleanup may be required.");

            ShowMessages(messages);

            toolStripProgressBar.Visible = false;

            Cursor.Current = Cursors.WaitCursor;

            return ok;
        }

        #endregion

        #region One Click

        private void btnExtractClearConvertUpdateAudit_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            ExtractClearConvertUpdateAudit();
            Cursor.Current = Cursors.Default;
        }

        public void ExtractClearConvertUpdateAudit()
        {
            bool ok = ExtractEALogs();

            if (ok)
            {
                ok = ConvertXmlLog();
            }

            if (ok)
            {
                ok = ClearEALogs();
            }

        }

        #endregion

        #region Helper Methods

        private void ShowMessages(List<Message> messages)
        {
            foreach (Message message in messages)
            {
                if (message.Type == MessageType.Error)
                {
                    MessageBox.Show(message.Text, "Extract and Clear Audit Logs", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
                else if (message.Type == MessageType.Warning)
                {
                    MessageBox.Show(message.Text, "Extract and Clear Audit Logs", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
                else if (message.Type == MessageType.Information)
                {
                    SetResultsStatus(message.Text);
                }
                else if (message.Type == MessageType.Audit)
                {
                    MessageBox.Show(message.Text, "Extract and Clear Audit Logs", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    AppendToAuditLog(message.Text);
                }
            }
        }

        public void SetResultsStatus(string text)
        {
            tbresults.Text = text;
            Application.DoEvents();
        }

        public void UpdateProgress(int progress, int total)
        {
            toolStripProgressBar.Visible = (progress <= total);

            if (progress <= total)
            {
                if (toolStripProgressBar.Maximum != total)
                {
                    toolStripProgressBar.Maximum = total;
                }

                toolStripProgressBar.Visible = true;
                toolStripProgressBar.Value = progress;
            }
            else
            {
                toolStripProgressBar.Visible = false;
 
            }
            Application.DoEvents();
        }


        private void AppendToAuditLog(string s)
        {
            StreamWriter sw;
            if (!File.Exists(auditConverter.ResultsFileName))
            {
                sw = File.CreateText(auditConverter.ResultsFileName);
            }
            else
            {
                sw = File.AppendText(auditConverter.ResultsFileName);
            }
            sw.WriteLine(s);
            sw.Close();
        }

        #endregion

        private void buttonClearLogs_Click(object sender, EventArgs e)
        {
            ClearEALogs();
        }

        private bool ClearEALogs()
        {
            Cursor.Current = Cursors.WaitCursor;

            SetResultsStatus("Clearing audit logs...");
            var messages = new List<Message>();

            bool auditClearOk = AuditConverter.ClearAuditLogs( dtpFrom.Checked ? dtpFrom.Value : (DateTime?) null, dtpTo.Value, ref messages);

            if (auditClearOk)
            {
                SetResultsStatus("Logs cleared ok.");
            }
            else
            {
                SetResultsStatus("Error Extracting Logs");
            }
            ShowMessages(messages);
            Cursor.Current = Cursors.Default;

            return auditClearOk;
        }

        private void buttonGetCount_Click(object sender, EventArgs e)
        {
            var rows = AuditConverter.GetRowCount(tbXMLToLoadfileName.Text, SetResultsStatus);
        }
    }
}
