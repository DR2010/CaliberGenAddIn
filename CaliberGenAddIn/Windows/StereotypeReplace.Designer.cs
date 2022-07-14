namespace EAAddIn.Windows
{
    partial class StereotypeReplace
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
            this.btnApply = new System.Windows.Forms.Button();
            this.dgvElementList = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cboAppliesTo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboStereotype = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxStatusList = new System.Windows.Forms.ComboBox();
            this.btnStatusReplace = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvElementList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(386, 46);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(104, 23);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "Apply Stereotype";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // dgvElementList
            // 
            this.dgvElementList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvElementList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvElementList.Location = new System.Drawing.Point(12, 119);
            this.dgvElementList.Name = "dgvElementList";
            this.dgvElementList.Size = new System.Drawing.Size(548, 305);
            this.dgvElementList.TabIndex = 3;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRefresh.Location = new System.Drawing.Point(256, 430);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cboAppliesTo
            // 
            this.cboAppliesTo.FormattingEnabled = true;
            this.cboAppliesTo.Location = new System.Drawing.Point(107, 21);
            this.cboAppliesTo.Name = "cboAppliesTo";
            this.cboAppliesTo.Size = new System.Drawing.Size(264, 21);
            this.cboAppliesTo.TabIndex = 5;
            this.cboAppliesTo.SelectedIndexChanged += new System.EventHandler(this.cboAppliesTo_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Element Type:";
            // 
            // cboStereotype
            // 
            this.cboStereotype.FormattingEnabled = true;
            this.cboStereotype.Location = new System.Drawing.Point(107, 48);
            this.cboStereotype.Name = "cboStereotype";
            this.cboStereotype.Size = new System.Drawing.Size(264, 21);
            this.cboStereotype.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Stereotype:";
            // 
            // cbxStatusList
            // 
            this.cbxStatusList.FormattingEnabled = true;
            this.cbxStatusList.Location = new System.Drawing.Point(107, 75);
            this.cbxStatusList.Name = "cbxStatusList";
            this.cbxStatusList.Size = new System.Drawing.Size(264, 21);
            this.cbxStatusList.TabIndex = 9;
            // 
            // btnStatusReplace
            // 
            this.btnStatusReplace.Location = new System.Drawing.Point(386, 73);
            this.btnStatusReplace.Name = "btnStatusReplace";
            this.btnStatusReplace.Size = new System.Drawing.Size(104, 23);
            this.btnStatusReplace.TabIndex = 10;
            this.btnStatusReplace.Text = "Apply Status";
            this.btnStatusReplace.UseVisualStyleBackColor = true;
            this.btnStatusReplace.Click += new System.EventHandler(this.btnStatusReplace_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Status:";
            // 
            // StereotypeReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 458);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStatusReplace);
            this.Controls.Add(this.cbxStatusList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboStereotype);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboAppliesTo);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvElementList);
            this.Controls.Add(this.btnApply);
            this.Name = "StereotypeReplace";
            this.Text = "Stereotype/ Status Replace";
            this.Load += new System.EventHandler(this.StereotypeReplace_Load);
            this.Activated += new System.EventHandler(this.StereotypeReplace_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.dgvElementList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.DataGridView dgvElementList;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox cboAppliesTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboStereotype;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxStatusList;
        private System.Windows.Forms.Button btnStatusReplace;
        private System.Windows.Forms.Label label1;
    }
}