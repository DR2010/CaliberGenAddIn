namespace EAAddIn.Windows
{
    partial class CaliberImportForm
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
            isShown = false;
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToCaliberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unlinkElementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkElementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortCaliberTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeViewCaliber = new System.Windows.Forms.TreeView();
            this.contextMenuCaliberTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.linkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unlinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.locateInEAProjectBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.treeViewEAAddIn = new System.Windows.Forms.TreeView();
            this.dataGridViewTableForAddIn = new System.Windows.Forms.DataGridView();
            this.cmbProjectSelect = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoadCaliber = new System.Windows.Forms.Button();
            this.btnSync = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenuCaliberTree.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTableForAddIn)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(808, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToCaliberToolStripMenuItem,
            this.unlinkElementToolStripMenuItem,
            this.linkElementToolStripMenuItem,
            this.sortCaliberTreeToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.ShowShortcutKeys = false;
            this.editToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.editToolStripMenuItem.Text = "Options";
            // 
            // loginToCaliberToolStripMenuItem
            // 
            this.loginToCaliberToolStripMenuItem.Name = "loginToCaliberToolStripMenuItem";
            this.loginToCaliberToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.loginToCaliberToolStripMenuItem.Text = "Login to Caliber";
            this.loginToCaliberToolStripMenuItem.Click += new System.EventHandler(this.loginToCaliberToolStripMenuItem_Click);
            // 
            // unlinkElementToolStripMenuItem
            // 
            this.unlinkElementToolStripMenuItem.Name = "unlinkElementToolStripMenuItem";
            this.unlinkElementToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.unlinkElementToolStripMenuItem.Text = "Unlink Element";
            this.unlinkElementToolStripMenuItem.Click += new System.EventHandler(this.unlinkElementToolStripMenuItem_Click);
            // 
            // linkElementToolStripMenuItem
            // 
            this.linkElementToolStripMenuItem.Name = "linkElementToolStripMenuItem";
            this.linkElementToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.linkElementToolStripMenuItem.Text = "Link Element";
            this.linkElementToolStripMenuItem.Click += new System.EventHandler(this.linkElementToolStripMenuItem_Click);
            // 
            // sortCaliberTreeToolStripMenuItem
            // 
            this.sortCaliberTreeToolStripMenuItem.CheckOnClick = true;
            this.sortCaliberTreeToolStripMenuItem.Name = "sortCaliberTreeToolStripMenuItem";
            this.sortCaliberTreeToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.sortCaliberTreeToolStripMenuItem.Text = "Sort Caliber Tree";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(784, 234);
            this.splitContainer1.SplitterDistance = 289;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeViewCaliber);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 234);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Caliber View";
            // 
            // treeViewCaliber
            // 
            this.treeViewCaliber.CheckBoxes = true;
            this.treeViewCaliber.ContextMenuStrip = this.contextMenuCaliberTree;
            this.treeViewCaliber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewCaliber.Location = new System.Drawing.Point(3, 16);
            this.treeViewCaliber.Name = "treeViewCaliber";
            this.treeViewCaliber.Size = new System.Drawing.Size(283, 215);
            this.treeViewCaliber.TabIndex = 0;
            this.treeViewCaliber.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewCaliber_AfterCheck);
            this.treeViewCaliber.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewCaliber_AfterSelect);
            // 
            // contextMenuCaliberTree
            // 
            this.contextMenuCaliberTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.linkToolStripMenuItem,
            this.unlinkToolStripMenuItem,
            this.locateInEAProjectBrowserToolStripMenuItem});
            this.contextMenuCaliberTree.Name = "contextMenuStrip1";
            this.contextMenuCaliberTree.Size = new System.Drawing.Size(225, 70);
            // 
            // linkToolStripMenuItem
            // 
            this.linkToolStripMenuItem.Name = "linkToolStripMenuItem";
            this.linkToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.linkToolStripMenuItem.Text = "Link";
            this.linkToolStripMenuItem.Click += new System.EventHandler(this.linkToolStripMenuItem_Click);
            // 
            // unlinkToolStripMenuItem
            // 
            this.unlinkToolStripMenuItem.Name = "unlinkToolStripMenuItem";
            this.unlinkToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.unlinkToolStripMenuItem.Text = "Unlink";
            this.unlinkToolStripMenuItem.Click += new System.EventHandler(this.unlinkToolStripMenuItem_Click);
            // 
            // locateInEAProjectBrowserToolStripMenuItem
            // 
            this.locateInEAProjectBrowserToolStripMenuItem.Name = "locateInEAProjectBrowserToolStripMenuItem";
            this.locateInEAProjectBrowserToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.locateInEAProjectBrowserToolStripMenuItem.Text = "Locate in EA Project Browser";
            this.locateInEAProjectBrowserToolStripMenuItem.Click += new System.EventHandler(this.locateInEAProjectBrowserToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.treeViewEAAddIn);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(491, 234);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "EA View";
            // 
            // treeViewEAAddIn
            // 
            this.treeViewEAAddIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewEAAddIn.Location = new System.Drawing.Point(3, 16);
            this.treeViewEAAddIn.Name = "treeViewEAAddIn";
            this.treeViewEAAddIn.Size = new System.Drawing.Size(485, 215);
            this.treeViewEAAddIn.TabIndex = 0;
            // 
            // dataGridViewTableForAddIn
            // 
            this.dataGridViewTableForAddIn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTableForAddIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTableForAddIn.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewTableForAddIn.Name = "dataGridViewTableForAddIn";
            this.dataGridViewTableForAddIn.Size = new System.Drawing.Size(784, 232);
            this.dataGridViewTableForAddIn.TabIndex = 0;
            // 
            // cmbProjectSelect
            // 
            this.cmbProjectSelect.FormattingEnabled = true;
            this.cmbProjectSelect.Location = new System.Drawing.Point(70, 37);
            this.cmbProjectSelect.Name = "cmbProjectSelect";
            this.cmbProjectSelect.Size = new System.Drawing.Size(343, 21);
            this.cmbProjectSelect.TabIndex = 2;
            this.cmbProjectSelect.SelectedIndexChanged += new System.EventHandler(this.cmbProjectSelect_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Project:";
            // 
            // btnLoadCaliber
            // 
            this.btnLoadCaliber.Location = new System.Drawing.Point(441, 37);
            this.btnLoadCaliber.Name = "btnLoadCaliber";
            this.btnLoadCaliber.Size = new System.Drawing.Size(109, 50);
            this.btnLoadCaliber.TabIndex = 4;
            this.btnLoadCaliber.Text = "&Load Caliber Data";
            this.btnLoadCaliber.UseVisualStyleBackColor = true;
            this.btnLoadCaliber.Click += new System.EventHandler(this.btnLoadCaliber_Click);
            // 
            // btnSync
            // 
            this.btnSync.Enabled = false;
            this.btnSync.Location = new System.Drawing.Point(642, 35);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(151, 52);
            this.btnSync.TabIndex = 5;
            this.btnSync.Text = "&Sync Caliber and EA";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(12, 93);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dataGridViewTableForAddIn);
            this.splitContainer2.Size = new System.Drawing.Size(784, 470);
            this.splitContainer2.SplitterDistance = 234;
            this.splitContainer2.TabIndex = 6;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripStatusLabel1,
            this.toolStripProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 566);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(808, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel.Text = "Ready.";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(751, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "               ";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar.Visible = false;
            // 
            // CaliberImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 588);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.btnLoadCaliber);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbProjectSelect);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CaliberImportForm";
            this.Text = "Caliber Import Tool";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CaliberImportForm_FormClosed);
            this.Load += new System.EventHandler(this.CaliberImportForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.contextMenuCaliberTree.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTableForAddIn)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginToCaliberToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView treeViewEAAddIn;
        private System.Windows.Forms.DataGridView dataGridViewTableForAddIn;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TreeView treeViewCaliber;
        public System.Windows.Forms.ComboBox cmbProjectSelect;
        private System.Windows.Forms.Button btnLoadCaliber;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStripMenuItem unlinkElementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linkElementToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuCaliberTree;
        private System.Windows.Forms.ToolStripMenuItem linkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unlinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem locateInEAProjectBrowserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortCaliberTreeToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}
