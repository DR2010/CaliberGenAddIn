namespace EAAddIn.Windows
{
    partial class ExtractAndClearAuditLogs
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbSaveLogs = new System.Windows.Forms.CheckBox();
            this.cbClearLogs = new System.Windows.Forms.CheckBox();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.tbAuditFileName = new System.Windows.Forms.TextBox();
            this.btnOpenFDForSaveLogTo = new System.Windows.Forms.Button();
            this.btnExtractLogs = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPathLocation = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbXMLToLoadfileName = new System.Windows.Forms.TextBox();
            this.btnOpenFDForLogToConver = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnConvertLog = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbresults = new System.Windows.Forms.TextBox();
            this.btnExtractClearConvertUpdateAudit = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonClearLogs = new System.Windows.Forms.Button();
            this.buttonGetCount = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbSaveLogs
            // 
            this.cbSaveLogs.AutoSize = true;
            this.cbSaveLogs.Location = new System.Drawing.Point(9, 28);
            this.cbSaveLogs.Name = "cbSaveLogs";
            this.cbSaveLogs.Size = new System.Drawing.Size(77, 17);
            this.cbSaveLogs.TabIndex = 0;
            this.cbSaveLogs.Text = "Save Logs";
            this.cbSaveLogs.UseVisualStyleBackColor = true;
            // 
            // cbClearLogs
            // 
            this.cbClearLogs.AutoSize = true;
            this.cbClearLogs.Location = new System.Drawing.Point(9, 51);
            this.cbClearLogs.Name = "cbClearLogs";
            this.cbClearLogs.Size = new System.Drawing.Size(76, 17);
            this.cbClearLogs.TabIndex = 1;
            this.cbClearLogs.Text = "Clear Logs";
            this.cbClearLogs.UseVisualStyleBackColor = true;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Checked = false;
            this.dtpFrom.CustomFormat = "ddd dd MMM yyyy , hh:mm tt";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(183, 25);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.ShowCheckBox = true;
            this.dtpFrom.ShowUpDown = true;
            this.dtpFrom.Size = new System.Drawing.Size(142, 20);
            this.dtpFrom.TabIndex = 2;
            this.dtpFrom.Value = new System.DateTime(2010, 7, 13, 0, 0, 0, 0);
            this.dtpFrom.ValueChanged += new System.EventHandler(this.DtpValueChanged);
            this.dtpFrom.Validated += new System.EventHandler(this.DtpValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Save Log to";
            // 
            // tbAuditFileName
            // 
            this.tbAuditFileName.Location = new System.Drawing.Point(93, 112);
            this.tbAuditFileName.Name = "tbAuditFileName";
            this.tbAuditFileName.Size = new System.Drawing.Size(413, 20);
            this.tbAuditFileName.TabIndex = 5;
            // 
            // btnOpenFDForSaveLogTo
            // 
            this.btnOpenFDForSaveLogTo.Location = new System.Drawing.Point(512, 111);
            this.btnOpenFDForSaveLogTo.Name = "btnOpenFDForSaveLogTo";
            this.btnOpenFDForSaveLogTo.Size = new System.Drawing.Size(24, 21);
            this.btnOpenFDForSaveLogTo.TabIndex = 6;
            this.btnOpenFDForSaveLogTo.Text = "...";
            this.btnOpenFDForSaveLogTo.UseVisualStyleBackColor = true;
            this.btnOpenFDForSaveLogTo.Click += new System.EventHandler(this.BtnOpenFileDialogueClick);
            // 
            // btnExtractLogs
            // 
            this.btnExtractLogs.Location = new System.Drawing.Point(19, 29);
            this.btnExtractLogs.Name = "btnExtractLogs";
            this.btnExtractLogs.Size = new System.Drawing.Size(377, 23);
            this.btnExtractLogs.TabIndex = 7;
            this.btnExtractLogs.Text = "1) Extract Logs";
            this.btnExtractLogs.UseVisualStyleBackColor = true;
            this.btnExtractLogs.Click += new System.EventHandler(this.BtnExtractAndClearLogsClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Path";
            // 
            // tbPathLocation
            // 
            this.tbPathLocation.Location = new System.Drawing.Point(93, 74);
            this.tbPathLocation.Multiline = true;
            this.tbPathLocation.Name = "tbPathLocation";
            this.tbPathLocation.ReadOnly = true;
            this.tbPathLocation.Size = new System.Drawing.Size(413, 32);
            this.tbPathLocation.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpTo);
            this.groupBox1.Controls.Add(this.tbPathLocation);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnOpenFDForSaveLogTo);
            this.groupBox1.Controls.Add(this.cbSaveLogs);
            this.groupBox1.Controls.Add(this.tbAuditFileName);
            this.groupBox1.Controls.Add(this.cbClearLogs);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpFrom);
            this.groupBox1.Location = new System.Drawing.Point(6, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(542, 141);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Extract and Clear Log Defaults";
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(183, 48);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.ShowUpDown = true;
            this.dtpTo.Size = new System.Drawing.Size(142, 20);
            this.dtpTo.TabIndex = 21;
            this.dtpTo.ValueChanged += new System.EventHandler(this.DtpValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(128, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "To Date:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(120, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "From Date:";
            // 
            // tbXMLToLoadfileName
            // 
            this.tbXMLToLoadfileName.Location = new System.Drawing.Point(91, 26);
            this.tbXMLToLoadfileName.Name = "tbXMLToLoadfileName";
            this.tbXMLToLoadfileName.Size = new System.Drawing.Size(267, 20);
            this.tbXMLToLoadfileName.TabIndex = 12;
            // 
            // btnOpenFDForLogToConver
            // 
            this.btnOpenFDForLogToConver.Location = new System.Drawing.Point(364, 23);
            this.btnOpenFDForLogToConver.Name = "btnOpenFDForLogToConver";
            this.btnOpenFDForLogToConver.Size = new System.Drawing.Size(24, 23);
            this.btnOpenFDForLogToConver.TabIndex = 13;
            this.btnOpenFDForLogToConver.Text = "...";
            this.btnOpenFDForLogToConver.UseVisualStyleBackColor = true;
            this.btnOpenFDForLogToConver.Click += new System.EventHandler(this.btnOpenFDForLogToConver_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Load XML";
            // 
            // btnConvertLog
            // 
            this.btnConvertLog.Location = new System.Drawing.Point(554, 29);
            this.btnConvertLog.Name = "btnConvertLog";
            this.btnConvertLog.Size = new System.Drawing.Size(394, 23);
            this.btnConvertLog.TabIndex = 19;
            this.btnConvertLog.Text = "2) Convert Log + Update Audit Tables";
            this.btnConvertLog.UseVisualStyleBackColor = true;
            this.btnConvertLog.Click += new System.EventHandler(this.btnConvertLog_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOpenFDForLogToConver);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tbXMLToLoadfileName);
            this.groupBox2.Location = new System.Drawing.Point(554, 58);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(394, 79);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Convert Log Defaults";
            // 
            // tbresults
            // 
            this.tbresults.Location = new System.Drawing.Point(12, 33);
            this.tbresults.Multiline = true;
            this.tbresults.Name = "tbresults";
            this.tbresults.ReadOnly = true;
            this.tbresults.Size = new System.Drawing.Size(959, 19);
            this.tbresults.TabIndex = 15;
            // 
            // btnExtractClearConvertUpdateAudit
            // 
            this.btnExtractClearConvertUpdateAudit.Location = new System.Drawing.Point(201, 4);
            this.btnExtractClearConvertUpdateAudit.Name = "btnExtractClearConvertUpdateAudit";
            this.btnExtractClearConvertUpdateAudit.Size = new System.Drawing.Size(441, 23);
            this.btnExtractClearConvertUpdateAudit.TabIndex = 23;
            this.btnExtractClearConvertUpdateAudit.Text = "Save, Load, Convert Audit Logs and Update Audit Tables";
            this.btnExtractClearConvertUpdateAudit.UseVisualStyleBackColor = true;
            this.btnExtractClearConvertUpdateAudit.Click += new System.EventHandler(this.btnExtractClearConvertUpdateAudit_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Controls.Add(this.btnExtractLogs);
            this.groupBox4.Controls.Add(this.btnConvertLog);
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Location = new System.Drawing.Point(12, 58);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(959, 203);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Audit Extraction Step By Step and Default Changes";
            // 
            // buttonClearLogs
            // 
            this.buttonClearLogs.Location = new System.Drawing.Point(977, 87);
            this.buttonClearLogs.Name = "buttonClearLogs";
            this.buttonClearLogs.Size = new System.Drawing.Size(377, 23);
            this.buttonClearLogs.TabIndex = 21;
            this.buttonClearLogs.Text = "3) Clear Logs";
            this.buttonClearLogs.UseVisualStyleBackColor = true;
            this.buttonClearLogs.Click += new System.EventHandler(this.buttonClearLogs_Click);
            // 
            // buttonGetCount
            // 
            this.buttonGetCount.Location = new System.Drawing.Point(978, 33);
            this.buttonGetCount.Name = "buttonGetCount";
            this.buttonGetCount.Size = new System.Drawing.Size(75, 23);
            this.buttonGetCount.TabIndex = 25;
            this.buttonGetCount.Text = "Get Count";
            this.buttonGetCount.UseVisualStyleBackColor = true;
            this.buttonGetCount.Click += new System.EventHandler(this.buttonGetCount_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 249);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1364, 22);
            this.statusStrip.TabIndex = 26;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(1300, 16);
            this.toolStripProgressBar.Visible = false;
            // 
            // ExtractAndClearAuditLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1364, 271);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.buttonGetCount);
            this.Controls.Add(this.buttonClearLogs);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.tbresults);
            this.Controls.Add(this.btnExtractClearConvertUpdateAudit);
            this.Name = "ExtractAndClearAuditLogs";
            this.Text = "Convert Log";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbSaveLogs;
        private System.Windows.Forms.CheckBox cbClearLogs;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbAuditFileName;
        private System.Windows.Forms.Button btnOpenFDForSaveLogTo;
        private System.Windows.Forms.Button btnExtractLogs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPathLocation;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbXMLToLoadfileName;
        private System.Windows.Forms.Button btnOpenFDForLogToConver;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnConvertLog;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbresults;
        private System.Windows.Forms.Button btnExtractClearConvertUpdateAudit;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button buttonClearLogs;
        private System.Windows.Forms.Button buttonGetCount;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
    }
}