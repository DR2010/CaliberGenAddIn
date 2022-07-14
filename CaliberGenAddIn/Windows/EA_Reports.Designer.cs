namespace EAAddIn.Windows
{
    partial class EaReports
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuDupLoadModule = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateCaliberIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateTableTagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvReportContents = new System.Windows.Forms.DataGridView();
            this.tvReport = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.locateInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteTaggedValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpAdmin = new System.Windows.Forms.TabPage();
            this.tpManager = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpManagerFrom = new System.Windows.Forms.DateTimePicker();
            this.buttonSingleTrafficLightReport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDetailedProjectTrackingReport = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpManagerTo = new System.Windows.Forms.DateTimePicker();
            this.tpProject = new System.Windows.Forms.TabPage();
            this.btnCreateFinaldDesignSignOff = new System.Windows.Forms.Button();
            this.cbProjects = new System.Windows.Forms.ComboBox();
            this.cbReleases = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbIncludeArchivedProjects = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportContents)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpAdmin.SuspendLayout();
            this.tpManager.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tpProject.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reportsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(352, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuDupLoadModule,
            this.duplicateCaliberIDToolStripMenuItem,
            this.duplicateTableTagToolStripMenuItem});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.reportsToolStripMenuItem.Text = "Duplicates";
            // 
            // MenuDupLoadModule
            // 
            this.MenuDupLoadModule.Name = "MenuDupLoadModule";
            this.MenuDupLoadModule.Size = new System.Drawing.Size(149, 22);
            this.MenuDupLoadModule.Text = "Load Modules";
            this.MenuDupLoadModule.Click += new System.EventHandler(this.MenuDupLoadModule_Click);
            // 
            // duplicateCaliberIDToolStripMenuItem
            // 
            this.duplicateCaliberIDToolStripMenuItem.Name = "duplicateCaliberIDToolStripMenuItem";
            this.duplicateCaliberIDToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.duplicateCaliberIDToolStripMenuItem.Text = "Caliber IDs";
            this.duplicateCaliberIDToolStripMenuItem.Click += new System.EventHandler(this.duplicateCaliberIDToolStripMenuItem_Click);
            // 
            // duplicateTableTagToolStripMenuItem
            // 
            this.duplicateTableTagToolStripMenuItem.Name = "duplicateTableTagToolStripMenuItem";
            this.duplicateTableTagToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.duplicateTableTagToolStripMenuItem.Text = "Table Tags";
            this.duplicateTableTagToolStripMenuItem.Click += new System.EventHandler(this.duplicateTableTagToolStripMenuItem_Click);
            // 
            // dgvReportContents
            // 
            this.dgvReportContents.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvReportContents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReportContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReportContents.Location = new System.Drawing.Point(0, 0);
            this.dgvReportContents.Name = "dgvReportContents";
            this.dgvReportContents.Size = new System.Drawing.Size(88, 677);
            this.dgvReportContents.TabIndex = 1;
            // 
            // tvReport
            // 
            this.tvReport.ContextMenuStrip = this.contextMenuStrip1;
            this.tvReport.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tvReport.Location = new System.Drawing.Point(0, -555);
            this.tvReport.Name = "tvReport";
            this.tvReport.Size = new System.Drawing.Size(352, 1232);
            this.tvReport.TabIndex = 2;
            this.tvReport.DoubleClick += new System.EventHandler(this.tvReport_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.locateInBrowserToolStripMenuItem,
            this.deleteTaggedValueToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(185, 48);
            // 
            // locateInBrowserToolStripMenuItem
            // 
            this.locateInBrowserToolStripMenuItem.Name = "locateInBrowserToolStripMenuItem";
            this.locateInBrowserToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.locateInBrowserToolStripMenuItem.Text = "Locate in Browser";
            this.locateInBrowserToolStripMenuItem.Click += new System.EventHandler(this.locateInBrowserToolStripMenuItem_Click);
            // 
            // deleteTaggedValueToolStripMenuItem
            // 
            this.deleteTaggedValueToolStripMenuItem.Name = "deleteTaggedValueToolStripMenuItem";
            this.deleteTaggedValueToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.deleteTaggedValueToolStripMenuItem.Text = "Delete Tagged Value";
            this.deleteTaggedValueToolStripMenuItem.Click += new System.EventHandler(this.deleteTaggedValueToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
            this.splitContainer1.Panel1.Controls.Add(this.tvReport);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvReportContents);
            this.splitContainer1.Size = new System.Drawing.Size(444, 677);
            this.splitContainer1.SplitterDistance = 352;
            this.splitContainer1.TabIndex = 4;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpAdmin);
            this.tabControl1.Controls.Add(this.tpManager);
            this.tabControl1.Controls.Add(this.tpProject);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(452, 703);
            this.tabControl1.TabIndex = 0;
            // 
            // tpAdmin
            // 
            this.tpAdmin.Controls.Add(this.splitContainer1);
            this.tpAdmin.Location = new System.Drawing.Point(4, 22);
            this.tpAdmin.Name = "tpAdmin";
            this.tpAdmin.Size = new System.Drawing.Size(444, 677);
            this.tpAdmin.TabIndex = 0;
            this.tpAdmin.Text = "Admin";
            this.tpAdmin.UseVisualStyleBackColor = true;
            // 
            // tpManager
            // 
            this.tpManager.Controls.Add(this.groupBox1);
            this.tpManager.Location = new System.Drawing.Point(4, 22);
            this.tpManager.Name = "tpManager";
            this.tpManager.Size = new System.Drawing.Size(444, 677);
            this.tpManager.TabIndex = 0;
            this.tpManager.Text = "Manager";
            this.tpManager.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpManagerFrom);
            this.groupBox1.Controls.Add(this.buttonSingleTrafficLightReport);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.buttonDetailedProjectTrackingReport);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpManagerTo);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(411, 137);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Management Reports";
            // 
            // dtpManagerFrom
            // 
            this.dtpManagerFrom.Location = new System.Drawing.Point(44, 19);
            this.dtpManagerFrom.Name = "dtpManagerFrom";
            this.dtpManagerFrom.Size = new System.Drawing.Size(200, 20);
            this.dtpManagerFrom.TabIndex = 14;
            // 
            // buttonSingleTrafficLightReport
            // 
            this.buttonSingleTrafficLightReport.Location = new System.Drawing.Point(38, 104);
            this.buttonSingleTrafficLightReport.Name = "buttonSingleTrafficLightReport";
            this.buttonSingleTrafficLightReport.Size = new System.Drawing.Size(206, 23);
            this.buttonSingleTrafficLightReport.TabIndex = 17;
            this.buttonSingleTrafficLightReport.Text = "Project Tracking Single Light";
            this.buttonSingleTrafficLightReport.UseVisualStyleBackColor = true;
            this.buttonSingleTrafficLightReport.Click += new System.EventHandler(this.buttonSingleTrafficLightReport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "From:";
            // 
            // buttonDetailedProjectTrackingReport
            // 
            this.buttonDetailedProjectTrackingReport.Location = new System.Drawing.Point(38, 75);
            this.buttonDetailedProjectTrackingReport.Name = "buttonDetailedProjectTrackingReport";
            this.buttonDetailedProjectTrackingReport.Size = new System.Drawing.Size(206, 23);
            this.buttonDetailedProjectTrackingReport.TabIndex = 16;
            this.buttonDetailedProjectTrackingReport.Text = "Project Tracking Detailed";
            this.buttonDetailedProjectTrackingReport.UseVisualStyleBackColor = true;
            this.buttonDetailedProjectTrackingReport.Click += new System.EventHandler(this.buttonDetailedProjectTrackingReport_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "To:";
            // 
            // dtpManagerTo
            // 
            this.dtpManagerTo.Location = new System.Drawing.Point(44, 49);
            this.dtpManagerTo.Name = "dtpManagerTo";
            this.dtpManagerTo.Size = new System.Drawing.Size(200, 20);
            this.dtpManagerTo.TabIndex = 15;
            // 
            // tpProject
            // 
            this.tpProject.Controls.Add(this.btnCreateFinaldDesignSignOff);
            this.tpProject.Controls.Add(this.cbProjects);
            this.tpProject.Controls.Add(this.cbReleases);
            this.tpProject.Controls.Add(this.groupBox2);
            this.tpProject.Location = new System.Drawing.Point(4, 22);
            this.tpProject.Name = "tpProject";
            this.tpProject.Size = new System.Drawing.Size(444, 677);
            this.tpProject.TabIndex = 1;
            this.tpProject.Text = "Project";
            this.tpProject.UseVisualStyleBackColor = true;
            // 
            // btnCreateFinaldDesignSignOff
            // 
            this.btnCreateFinaldDesignSignOff.Location = new System.Drawing.Point(8, 89);
            this.btnCreateFinaldDesignSignOff.Name = "btnCreateFinaldDesignSignOff";
            this.btnCreateFinaldDesignSignOff.Size = new System.Drawing.Size(160, 23);
            this.btnCreateFinaldDesignSignOff.TabIndex = 11;
            this.btnCreateFinaldDesignSignOff.Text = "Create Final Design Sign Off";
            this.btnCreateFinaldDesignSignOff.UseVisualStyleBackColor = true;
            this.btnCreateFinaldDesignSignOff.Click += new System.EventHandler(this.btnCreateFinaldDesignSignOff_Click);
            // 
            // cbProjects
            // 
            this.cbProjects.FormattingEnabled = true;
            this.cbProjects.Location = new System.Drawing.Point(60, 49);
            this.cbProjects.Name = "cbProjects";
            this.cbProjects.Size = new System.Drawing.Size(358, 21);
            this.cbProjects.Sorted = true;
            this.cbProjects.TabIndex = 8;
            // 
            // cbReleases
            // 
            this.cbReleases.FormattingEnabled = true;
            this.cbReleases.Location = new System.Drawing.Point(60, 22);
            this.cbReleases.Name = "cbReleases";
            this.cbReleases.Size = new System.Drawing.Size(188, 21);
            this.cbReleases.TabIndex = 6;
            this.cbReleases.SelectedIndexChanged += new System.EventHandler(this.cbReleaes_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbIncludeArchivedProjects);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(8, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(421, 80);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Locate Project";
            // 
            // cbIncludeArchivedProjects
            // 
            this.cbIncludeArchivedProjects.AutoSize = true;
            this.cbIncludeArchivedProjects.Location = new System.Drawing.Point(262, 23);
            this.cbIncludeArchivedProjects.Name = "cbIncludeArchivedProjects";
            this.cbIncludeArchivedProjects.Size = new System.Drawing.Size(153, 17);
            this.cbIncludeArchivedProjects.TabIndex = 0;
            this.cbIncludeArchivedProjects.Text = "Include Archived Projects?";
            this.cbIncludeArchivedProjects.UseVisualStyleBackColor = true;
            this.cbIncludeArchivedProjects.CheckedChanged += new System.EventHandler(this.cbReleaes_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Project:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Release:";
            // 
            // EA_Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 703);
            this.Controls.Add(this.tabControl1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "EaReports";
            this.Text = "EA Reports";
            this.Load += new System.EventHandler(this.EA_Reports_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportContents)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpAdmin.ResumeLayout(false);
            this.tpManager.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tpProject.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuDupLoadModule;
        private System.Windows.Forms.DataGridView dgvReportContents;
        private System.Windows.Forms.ToolStripMenuItem duplicateCaliberIDToolStripMenuItem;
        private System.Windows.Forms.TreeView tvReport;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem locateInBrowserToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem duplicateTableTagToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteTaggedValueToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpAdmin;
        private System.Windows.Forms.TabPage tpManager;
        private System.Windows.Forms.TabPage tpProject;
        private System.Windows.Forms.Button buttonDetailedProjectTrackingReport;
        private System.Windows.Forms.DateTimePicker dtpManagerTo;
        private System.Windows.Forms.DateTimePicker dtpManagerFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSingleTrafficLightReport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCreateFinaldDesignSignOff;
        private System.Windows.Forms.ComboBox cbProjects;
        private System.Windows.Forms.ComboBox cbReleases;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbIncludeArchivedProjects;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}