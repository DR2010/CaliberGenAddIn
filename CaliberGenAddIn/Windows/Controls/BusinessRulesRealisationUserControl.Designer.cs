namespace EAAddIn.Windows.Controls
{
    partial class BusinessRulesRealisationUserControl
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
            this.cbxIgnoreDeleted = new System.Windows.Forms.CheckBox();
            this.cbxRecursive = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxConnectedTo = new System.Windows.Forms.ComboBox();
            this.cbxType = new System.Windows.Forms.ComboBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvRealisation = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.locateInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToTBDPackageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRealisation)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxIgnoreDeleted
            // 
            this.cbxIgnoreDeleted.AutoSize = true;
            this.cbxIgnoreDeleted.Location = new System.Drawing.Point(17, 102);
            this.cbxIgnoreDeleted.Name = "cbxIgnoreDeleted";
            this.cbxIgnoreDeleted.Size = new System.Drawing.Size(126, 17);
            this.cbxIgnoreDeleted.TabIndex = 21;
            this.cbxIgnoreDeleted.Text = "Ignore Deleted Rules";
            this.cbxIgnoreDeleted.UseVisualStyleBackColor = true;
            // 
            // cbxRecursive
            // 
            this.cbxRecursive.AutoSize = true;
            this.cbxRecursive.Checked = true;
            this.cbxRecursive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxRecursive.Location = new System.Drawing.Point(17, 79);
            this.cbxRecursive.Name = "cbxRecursive";
            this.cbxRecursive.Size = new System.Drawing.Size(133, 17);
            this.cbxRecursive.TabIndex = 20;
            this.cbxRecursive.Text = "Include Sub packages";
            this.cbxRecursive.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(250, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Element type:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(459, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Connected to:";
            // 
            // cbxConnectedTo
            // 
            this.cbxConnectedTo.FormattingEnabled = true;
            this.cbxConnectedTo.Items.AddRange(new object[] {
            "All",
            "Class",
            "UseCase",
            "Requirement"});
            this.cbxConnectedTo.Location = new System.Drawing.Point(462, 47);
            this.cbxConnectedTo.Name = "cbxConnectedTo";
            this.cbxConnectedTo.Size = new System.Drawing.Size(203, 21);
            this.cbxConnectedTo.TabIndex = 17;
            // 
            // cbxType
            // 
            this.cbxType.FormattingEnabled = true;
            this.cbxType.Items.AddRange(new object[] {
            "All",
            "Requirement",
            "Class",
            "UseCase"});
            this.cbxType.Location = new System.Drawing.Point(253, 47);
            this.cbxType.Name = "cbxType";
            this.cbxType.Size = new System.Drawing.Size(203, 21);
            this.cbxType.TabIndex = 16;
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilter.Location = new System.Drawing.Point(253, 95);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(203, 26);
            this.txtFilter.TabIndex = 15;
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(111, 17);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(136, 56);
            this.btnFilter.TabIndex = 14;
            this.btnFilter.Text = "List implemented business rules for element";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(17, 17);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(88, 56);
            this.btnRefresh.TabIndex = 12;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvRealisation
            // 
            this.dgvRealisation.AllowUserToOrderColumns = true;
            this.dgvRealisation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRealisation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRealisation.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvRealisation.Location = new System.Drawing.Point(17, 139);
            this.dgvRealisation.Name = "dgvRealisation";
            this.dgvRealisation.Size = new System.Drawing.Size(791, 290);
            this.dgvRealisation.TabIndex = 11;
            this.dgvRealisation.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRealisation_RowEnter);
            this.dgvRealisation.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvRealisation_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.locateInBrowserToolStripMenuItem,
            this.moveToTBDPackageToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(190, 70);
            // 
            // locateInBrowserToolStripMenuItem
            // 
            this.locateInBrowserToolStripMenuItem.Name = "locateInBrowserToolStripMenuItem";
            this.locateInBrowserToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.locateInBrowserToolStripMenuItem.Text = "Locate in Browser";
            this.locateInBrowserToolStripMenuItem.Click += new System.EventHandler(this.locateInBrowserToolStripMenuItem_Click);
            // 
            // moveToTBDPackageToolStripMenuItem
            // 
            this.moveToTBDPackageToolStripMenuItem.Name = "moveToTBDPackageToolStripMenuItem";
            this.moveToTBDPackageToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.moveToTBDPackageToolStripMenuItem.Text = "Move to TBD Package";
            this.moveToTBDPackageToolStripMenuItem.Click += new System.EventHandler(this.moveToTBDPackageToolStripMenuItem_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(250, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Filter";
            // 
            // BusinessRulesRealisationUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxIgnoreDeleted);
            this.Controls.Add(this.cbxRecursive);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxConnectedTo);
            this.Controls.Add(this.cbxType);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvRealisation);
            this.Name = "BusinessRulesRealisationUserControl";
            this.Size = new System.Drawing.Size(824, 450);
            this.Load += new System.EventHandler(this.BusinessRulesRealisationUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRealisation)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbxIgnoreDeleted;
        private System.Windows.Forms.CheckBox cbxRecursive;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxConnectedTo;
        private System.Windows.Forms.ComboBox cbxType;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvRealisation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem locateInBrowserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveToTBDPackageToolStripMenuItem;
    }
}
