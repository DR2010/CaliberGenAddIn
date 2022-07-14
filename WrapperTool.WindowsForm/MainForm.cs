using System;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;


namespace WrapperTool.WindowsForm
{
    public partial class MainForm : Form
    {
        private string dataFolder = Application.ExecutablePath;
        private string lastTemplateLocation = string.Empty;
        private string cvsFile;
        private string cvsFileLocationName;
        private string cvsDALocation; 

        public MainForm()
        {
            InitializeComponent();
            cvsDALocation = "\\\\idcom050s1\\ESG_Web_Content\\ftp\\FtpData\\CVSFiles\\DA\\";
            
        }


        public MainForm(string xmlGen) : this()
        {
            cvsFile = xmlGen + ".cvs";
            cvsFileLocationName = cvsDALocation + cvsFile;

            if (! string.IsNullOrEmpty(xmlGen))
            {
                OpenFileCVS(cvsFileLocationName);
            }

        }

        private IDockContent FindDocument(string text)
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                    if (form.Text == text)
                        return form as IDockContent;

                return null;
            }
            else
            {
                foreach (IDockContent content in dockPanel.Documents)
                    if (content.DockHandler.TabText == text)
                        return content;

                return null;
            }
        }
        private void LoadFile(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "XML files (*.XML)|*.XML";
            openFile.FilterIndex = 1;
            openFile.RestoreDirectory = false;
            openFile.InitialDirectory = dataFolder;

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string fullName = openFile.FileName;
                string fileName = Path.GetFileName(fullName);
                Launcher dummyDoc = FindDocument(fileName) as Launcher;
                if (dummyDoc == null)
                {
                    dummyDoc = new Launcher();
                    dummyDoc.Show(dockPanel);
                }

                try
                {
                    dummyDoc.OpenFromData(fullName);

                }
                catch (Exception exception)
                {
                    dummyDoc.Close();
                    MessageBox.Show(string.Format("{0}\n{1}", exception.Message, exception.StackTrace));
                }

            }
        }
        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "CVS files (ZEI*.CVS)|ZEI*.CVS";
            openFile.FilterIndex = 1;
            openFile.RestoreDirectory = false;
            openFile.InitialDirectory = "\\\\idcom050s1\\ESG_Web_Content\\ftp\\FtpData\\CVSFiles\\DA";

            if (!string.IsNullOrEmpty(lastTemplateLocation))
            {
                openFile.InitialDirectory = lastTemplateLocation;
            }
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string fullName = openFile.FileName;
                lastTemplateLocation = Path.GetDirectoryName(fullName);
                Launcher dummyDoc = FindDocument(fullName) as Launcher;
                if (dummyDoc==null)
                {
                    dummyDoc = new Launcher();
                    dummyDoc.Show(dockPanel);
                }

                try
                {
                    dummyDoc.OpenFromTemplate(fullName);
                }
                catch (Exception exception)
                {
                    dummyDoc.Close();
                    MessageBox.Show(string.Format("{0}\n{1}", exception.Message,exception.StackTrace));
                }

            }

        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null) return;
            ((Launcher)this.ActiveMdiChild).Save(); 

        }

        private void MainForm_MdiChildActivate(object sender, EventArgs e)
        {
            saveToolStripButton.Enabled = this.MdiChildren != null && this.MdiChildren.Length > 0;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void OpenFileCVS(string fullName)
        {

            lastTemplateLocation = Path.GetDirectoryName(fullName);
            Launcher dummyDoc = FindDocument(fullName) as Launcher;
            if (dummyDoc == null)
            {
                dummyDoc = new Launcher();
                dummyDoc.Show(dockPanel);
            }

            try
            {
                dummyDoc.OpenFromTemplate(fullName);
            }
            catch (Exception exception)
            {
                dummyDoc.Close();
                MessageBox.Show(string.Format("{0}\n{1}", exception.Message, exception.StackTrace));
            }

        }

    }
}
