namespace EAAddIn.Windows
{
    partial class WrapperFileSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WrapperFileSelection));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnLoadWrapperIntoEA = new System.Windows.Forms.Button();
            this.btnGetNewWrapper = new System.Windows.Forms.Button();
            this.fileSelectorGridView = new System.Windows.Forms.DataGridView();
            this.selectXMLTreeView = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.findInProjectBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.fileSelectorGridView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnLoadWrapperIntoEA
            // 
            this.btnLoadWrapperIntoEA.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnLoadWrapperIntoEA.Location = new System.Drawing.Point(270, 403);
            this.btnLoadWrapperIntoEA.Name = "btnLoadWrapperIntoEA";
            this.btnLoadWrapperIntoEA.Size = new System.Drawing.Size(168, 32);
            this.btnLoadWrapperIntoEA.TabIndex = 2;
            this.btnLoadWrapperIntoEA.Text = "Load GEN structure into EA";
            this.btnLoadWrapperIntoEA.UseVisualStyleBackColor = true;
            this.btnLoadWrapperIntoEA.Click += new System.EventHandler(this.btnLoadWrapperIntoEA_Click);
            // 
            // btnGetNewWrapper
            // 
            this.btnGetNewWrapper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGetNewWrapper.Location = new System.Drawing.Point(12, 453);
            this.btnGetNewWrapper.Name = "btnGetNewWrapper";
            this.btnGetNewWrapper.Size = new System.Drawing.Size(128, 32);
            this.btnGetNewWrapper.TabIndex = 4;
            this.btnGetNewWrapper.Text = "Load new wrapper";
            this.btnGetNewWrapper.UseVisualStyleBackColor = true;
            this.btnGetNewWrapper.Click += new System.EventHandler(this.btnGetNewWrapper_Click);
            // 
            // fileSelectorGridView
            // 
            this.fileSelectorGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fileSelectorGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileSelectorGridView.Location = new System.Drawing.Point(0, 0);
            this.fileSelectorGridView.Name = "fileSelectorGridView";
            this.fileSelectorGridView.Size = new System.Drawing.Size(299, 385);
            this.fileSelectorGridView.TabIndex = 0;
            // 
            // selectXMLTreeView
            // 
            this.selectXMLTreeView.ContextMenuStrip = this.contextMenuStrip1;
            this.selectXMLTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectXMLTreeView.Location = new System.Drawing.Point(0, 0);
            this.selectXMLTreeView.Name = "selectXMLTreeView";
            this.selectXMLTreeView.Size = new System.Drawing.Size(344, 385);
            this.selectXMLTreeView.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findInProjectBrowserToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(197, 26);
            // 
            // findInProjectBrowserToolStripMenuItem
            // 
            this.findInProjectBrowserToolStripMenuItem.Name = "findInProjectBrowserToolStripMenuItem";
            this.findInProjectBrowserToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.findInProjectBrowserToolStripMenuItem.Text = "Locate in Project Browser";
            this.findInProjectBrowserToolStripMenuItem.Click += new System.EventHandler(this.findInProjectBrowserToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.selectXMLTreeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.fileSelectorGridView);
            this.splitContainer1.Size = new System.Drawing.Size(647, 385);
            this.splitContainer1.SplitterDistance = 344;
            this.splitContainer1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(530, 435);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Program not found in EA";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(505, 462);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(22, 22);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(530, 466);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Program already in EA";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(505, 435);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(22, 22);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(270, 453);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(168, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // WrapperFileSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 488);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnLoadWrapperIntoEA);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnGetNewWrapper);
            this.Name = "WrapperFileSelection";
            this.Text = "COOLGen Structure Chart Importer";
            this.Load += new System.EventHandler(this.WrapperFileSelection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fileSelectorGridView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnLoadWrapperIntoEA;
        private System.Windows.Forms.Button btnGetNewWrapper;
        private System.Windows.Forms.DataGridView fileSelectorGridView;
        private System.Windows.Forms.TreeView selectXMLTreeView;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem findInProjectBrowserToolStripMenuItem;
    }
}