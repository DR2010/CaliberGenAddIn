namespace EAAddIn.Windows
{
    partial class SQLScriptStoredProcedureAttributes
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
            this.ConstraintTextBox = new System.Windows.Forms.TextBox();
            this.CodeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ConstraintTextBox
            // 
            this.ConstraintTextBox.AcceptsTab = true;
            this.ConstraintTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConstraintTextBox.Location = new System.Drawing.Point(6, 22);
            this.ConstraintTextBox.Multiline = true;
            this.ConstraintTextBox.Name = "ConstraintTextBox";
            this.ConstraintTextBox.ReadOnly = true;
            this.ConstraintTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ConstraintTextBox.Size = new System.Drawing.Size(624, 79);
            this.ConstraintTextBox.TabIndex = 20;
            // 
            // CodeLabel
            // 
            this.CodeLabel.AutoSize = true;
            this.CodeLabel.Location = new System.Drawing.Point(3, 6);
            this.CodeLabel.Name = "CodeLabel";
            this.CodeLabel.Size = new System.Drawing.Size(32, 13);
            this.CodeLabel.TabIndex = 19;
            this.CodeLabel.Text = "Code";
            // 
            // SQLScriptStoredProcedureAttributes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ConstraintTextBox);
            this.Controls.Add(this.CodeLabel);
            this.Name = "SQLScriptStoredProcedureAttributes";
            this.Size = new System.Drawing.Size(644, 138);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox ConstraintTextBox;
        private System.Windows.Forms.Label CodeLabel;
    }
}
