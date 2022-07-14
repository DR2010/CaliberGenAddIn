namespace EAAddIn.Windows.Controls
{
    partial class UCDuplicatedElements
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

        #region Component Designer generated code

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
            this.tvReport = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.locateInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteTaggedValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeElementsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reportsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(368, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuDupLoadModule,
            this.duplicateCaliberIDToolStripMenuItem,
            this.duplicateTableTagToolStripMenuItem});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.reportsToolStripMenuItem.Text = "Duplicate";
            // 
            // MenuDupLoadModule
            // 
            this.MenuDupLoadModule.Name = "MenuDupLoadModule";
            this.MenuDupLoadModule.Size = new System.Drawing.Size(192, 22);
            this.MenuDupLoadModule.Text = "Duplicate Load Module";
            this.MenuDupLoadModule.Click += new System.EventHandler(this.MenuDupLoadModule_Click);
            // 
            // duplicateCaliberIDToolStripMenuItem
            // 
            this.duplicateCaliberIDToolStripMenuItem.Name = "duplicateCaliberIDToolStripMenuItem";
            this.duplicateCaliberIDToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.duplicateCaliberIDToolStripMenuItem.Text = "Duplicate Caliber ID";
            this.duplicateCaliberIDToolStripMenuItem.Click += new System.EventHandler(this.duplicateCaliberIDToolStripMenuItem_Click);
            // 
            // duplicateTableTagToolStripMenuItem
            // 
            this.duplicateTableTagToolStripMenuItem.Name = "duplicateTableTagToolStripMenuItem";
            this.duplicateTableTagToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.duplicateTableTagToolStripMenuItem.Text = "Duplicate Table Tag";
            this.duplicateTableTagToolStripMenuItem.Click += new System.EventHandler(this.duplicateTableTagToolStripMenuItem_Click);
            // 
            // tvReport
            // 
            this.tvReport.ContextMenuStrip = this.contextMenuStrip1;
            this.tvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvReport.Location = new System.Drawing.Point(0, 24);
            this.tvReport.Name = "tvReport";
            this.tvReport.Size = new System.Drawing.Size(368, 255);
            this.tvReport.TabIndex = 2;
            this.tvReport.DoubleClick += new System.EventHandler(this.tvReport_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.locateInBrowserToolStripMenuItem,
            this.deleteTaggedValueToolStripMenuItem,
            this.mergeElementsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(185, 92);
            // 
            // locateInBrowserToolStripMenuItem
            // 
            this.locateInBrowserToolStripMenuItem.Name = "locateInBrowserToolStripMenuItem";
            this.locateInBrowserToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.locateInBrowserToolStripMenuItem.Text = "Locate in Browser";
            // 
            // deleteTaggedValueToolStripMenuItem
            // 
            this.deleteTaggedValueToolStripMenuItem.Name = "deleteTaggedValueToolStripMenuItem";
            this.deleteTaggedValueToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.deleteTaggedValueToolStripMenuItem.Text = "Delete Tagged Value";
            // 
            // mergeElementsToolStripMenuItem
            // 
            this.mergeElementsToolStripMenuItem.Name = "mergeElementsToolStripMenuItem";
            this.mergeElementsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.mergeElementsToolStripMenuItem.Text = "Merge Elements";
            this.mergeElementsToolStripMenuItem.Click += new System.EventHandler(this.mergeElementsToolStripMenuItem_Click);
            // 
            // UCDuplicatedElements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvReport);
            this.Controls.Add(this.menuStrip1);
            this.Name = "UCDuplicatedElements";
            this.Size = new System.Drawing.Size(368, 279);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuDupLoadModule;
        private System.Windows.Forms.ToolStripMenuItem duplicateCaliberIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicateTableTagToolStripMenuItem;
        private System.Windows.Forms.TreeView tvReport;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem locateInBrowserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteTaggedValueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mergeElementsToolStripMenuItem;
    }
}
