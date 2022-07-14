namespace EAAddIn.Windows
{
    partial class SelectStereotype
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
            this.cboStereotype = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddStereotype = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbAppendStereotype = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cboStereotype
            // 
            this.cboStereotype.FormattingEnabled = true;
            this.cboStereotype.Location = new System.Drawing.Point(98, 12);
            this.cboStereotype.Name = "cboStereotype";
            this.cboStereotype.Size = new System.Drawing.Size(363, 21);
            this.cboStereotype.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Stereotypes";
            // 
            // btnAddStereotype
            // 
            this.btnAddStereotype.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAddStereotype.Location = new System.Drawing.Point(272, 39);
            this.btnAddStereotype.Name = "btnAddStereotype";
            this.btnAddStereotype.Size = new System.Drawing.Size(129, 23);
            this.btnAddStereotype.TabIndex = 2;
            this.btnAddStereotype.Text = "Add Stereotypes";
            this.btnAddStereotype.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(407, 39);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(54, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // cbAppendStereotype
            // 
            this.cbAppendStereotype.AutoSize = true;
            this.cbAppendStereotype.Location = new System.Drawing.Point(0, 43);
            this.cbAppendStereotype.Name = "cbAppendStereotype";
            this.cbAppendStereotype.Size = new System.Drawing.Size(160, 17);
            this.cbAppendStereotype.TabIndex = 5;
            this.cbAppendStereotype.Text = "Add to existing Stereotypes?";
            this.cbAppendStereotype.UseVisualStyleBackColor = true;
            // 
            // SelectStereotype
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 68);
            this.Controls.Add(this.cbAppendStereotype);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddStereotype);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboStereotype);
            this.Name = "SelectStereotype";
            this.Text = "Select Stereotype";
            this.Load += new System.EventHandler(this.SelectStereotype_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboStereotype;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddStereotype;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbAppendStereotype;
    }
}