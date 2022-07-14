namespace EAAddIn.Windows
{
    partial class BusinessRulesRealisation
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
            this.dgvRealisation = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnPlaceInDiagram = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.cbxType = new System.Windows.Forms.ComboBox();
            this.cbxConnectedTo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxRecursive = new System.Windows.Forms.CheckBox();
            this.cbxIgnoreDeleted = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRealisation)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvRealisation
            // 
            this.dgvRealisation.AllowUserToOrderColumns = true;
            this.dgvRealisation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRealisation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRealisation.Location = new System.Drawing.Point(12, 111);
            this.dgvRealisation.Name = "dgvRealisation";
            this.dgvRealisation.Size = new System.Drawing.Size(843, 374);
            this.dgvRealisation.TabIndex = 0;
            this.dgvRealisation.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRealisation_RowEnter);
            this.dgvRealisation.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvRealisation_MouseDoubleClick);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(150, 56);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnPlaceInDiagram
            // 
            this.btnPlaceInDiagram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlaceInDiagram.Location = new System.Drawing.Point(710, 12);
            this.btnPlaceInDiagram.Name = "btnPlaceInDiagram";
            this.btnPlaceInDiagram.Size = new System.Drawing.Size(145, 56);
            this.btnPlaceInDiagram.TabIndex = 2;
            this.btnPlaceInDiagram.Text = "Place on diagram";
            this.btnPlaceInDiagram.UseVisualStyleBackColor = true;
            this.btnPlaceInDiagram.Click += new System.EventHandler(this.btnPlaceInDiagram_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(168, 12);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(136, 56);
            this.btnFilter.TabIndex = 3;
            this.btnFilter.Text = "List implemented business rules for element";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilter.Location = new System.Drawing.Point(310, 12);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(394, 26);
            this.txtFilter.TabIndex = 4;
            // 
            // cbxType
            // 
            this.cbxType.FormattingEnabled = true;
            this.cbxType.Items.AddRange(new object[] {
            "All",
            "Requirement",
            "Class",
            "UseCase"});
            this.cbxType.Location = new System.Drawing.Point(428, 44);
            this.cbxType.Name = "cbxType";
            this.cbxType.Size = new System.Drawing.Size(203, 21);
            this.cbxType.TabIndex = 5;
            // 
            // cbxConnectedTo
            // 
            this.cbxConnectedTo.FormattingEnabled = true;
            this.cbxConnectedTo.Items.AddRange(new object[] {
            "All",
            "Class",
            "UseCase",
            "Requirement"});
            this.cbxConnectedTo.Location = new System.Drawing.Point(428, 71);
            this.cbxConnectedTo.Name = "cbxConnectedTo";
            this.cbxConnectedTo.Size = new System.Drawing.Size(203, 21);
            this.cbxConnectedTo.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(348, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Connected to:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(348, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Element type:";
            // 
            // cbxRecursive
            // 
            this.cbxRecursive.AutoSize = true;
            this.cbxRecursive.Checked = true;
            this.cbxRecursive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxRecursive.Location = new System.Drawing.Point(12, 88);
            this.cbxRecursive.Name = "cbxRecursive";
            this.cbxRecursive.Size = new System.Drawing.Size(133, 17);
            this.cbxRecursive.TabIndex = 9;
            this.cbxRecursive.Text = "Include Sub packages";
            this.cbxRecursive.UseVisualStyleBackColor = true;
            // 
            // cbxIgnoreDeleted
            // 
            this.cbxIgnoreDeleted.AutoSize = true;
            this.cbxIgnoreDeleted.Location = new System.Drawing.Point(168, 88);
            this.cbxIgnoreDeleted.Name = "cbxIgnoreDeleted";
            this.cbxIgnoreDeleted.Size = new System.Drawing.Size(126, 17);
            this.cbxIgnoreDeleted.TabIndex = 10;
            this.cbxIgnoreDeleted.Text = "Ignore Deleted Rules";
            this.cbxIgnoreDeleted.UseVisualStyleBackColor = true;
            this.cbxIgnoreDeleted.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // BusinessRulesRealisation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 497);
            this.Controls.Add(this.cbxIgnoreDeleted);
            this.Controls.Add(this.cbxRecursive);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxConnectedTo);
            this.Controls.Add(this.cbxType);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.btnPlaceInDiagram);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvRealisation);
            this.Name = "BusinessRulesRealisation";
            this.Text = "BusinessRulesRealisation";
            this.Load += new System.EventHandler(this.BusinessRulesRealisation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRealisation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRealisation;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnPlaceInDiagram;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.ComboBox cbxType;
        private System.Windows.Forms.ComboBox cbxConnectedTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbxRecursive;
        private System.Windows.Forms.CheckBox cbxIgnoreDeleted;
    }
}