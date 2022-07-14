namespace EAAddIn.Windows
{
    partial class SQLScriptIndexAttributes
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ConstraintTextBox = new System.Windows.Forms.TextBox();
            this.ConstraintLabel = new System.Windows.Forms.Label();
            this.ColumnsListView = new System.Windows.Forms.ListView();
            this.ColumnsLabel = new System.Windows.Forms.Label();
            this.TypeTextBox = new System.Windows.Forms.TextBox();
            this.TypeLabel = new System.Windows.Forms.Label();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ColumnsListView);
            this.splitContainer1.Panel1.Controls.Add(this.ColumnsLabel);
            this.splitContainer1.Panel1.Controls.Add(this.TypeTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.TypeLabel);
            this.splitContainer1.Panel1.Controls.Add(this.NameTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.NameLabel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ConstraintTextBox);
            this.splitContainer1.Panel2.Controls.Add(this.ConstraintLabel);
            this.splitContainer1.Size = new System.Drawing.Size(644, 138);
            this.splitContainer1.SplitterDistance = 314;
            this.splitContainer1.TabIndex = 0;
            // 
            // ConstraintTextBox
            // 
            this.ConstraintTextBox.AcceptsTab = true;
            this.ConstraintTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConstraintTextBox.Location = new System.Drawing.Point(67, 3);
            this.ConstraintTextBox.Multiline = true;
            this.ConstraintTextBox.Name = "ConstraintTextBox";
            this.ConstraintTextBox.ReadOnly = true;
            this.ConstraintTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ConstraintTextBox.Size = new System.Drawing.Size(256, 105);
            this.ConstraintTextBox.TabIndex = 30;
            // 
            // ConstraintLabel
            // 
            this.ConstraintLabel.AutoSize = true;
            this.ConstraintLabel.Location = new System.Drawing.Point(7, 6);
            this.ConstraintLabel.Name = "ConstraintLabel";
            this.ConstraintLabel.Size = new System.Drawing.Size(54, 13);
            this.ConstraintLabel.TabIndex = 29;
            this.ConstraintLabel.Text = "Constraint";
            // 
            // ColumnsListView
            // 
            this.ColumnsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ColumnsListView.Location = new System.Drawing.Point(65, 55);
            this.ColumnsListView.Name = "ColumnsListView";
            this.ColumnsListView.Size = new System.Drawing.Size(239, 53);
            this.ColumnsListView.TabIndex = 38;
            this.ColumnsListView.UseCompatibleStateImageBehavior = false;
            this.ColumnsListView.View = System.Windows.Forms.View.List;
            // 
            // ColumnsLabel
            // 
            this.ColumnsLabel.AutoSize = true;
            this.ColumnsLabel.Location = new System.Drawing.Point(5, 55);
            this.ColumnsLabel.Name = "ColumnsLabel";
            this.ColumnsLabel.Size = new System.Drawing.Size(47, 13);
            this.ColumnsLabel.TabIndex = 37;
            this.ColumnsLabel.Text = "Columns";
            // 
            // TypeTextBox
            // 
            this.TypeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TypeTextBox.Location = new System.Drawing.Point(65, 29);
            this.TypeTextBox.Name = "TypeTextBox";
            this.TypeTextBox.ReadOnly = true;
            this.TypeTextBox.Size = new System.Drawing.Size(241, 20);
            this.TypeTextBox.TabIndex = 36;
            // 
            // TypeLabel
            // 
            this.TypeLabel.AutoSize = true;
            this.TypeLabel.Location = new System.Drawing.Point(5, 32);
            this.TypeLabel.Name = "TypeLabel";
            this.TypeLabel.Size = new System.Drawing.Size(31, 13);
            this.TypeLabel.TabIndex = 35;
            this.TypeLabel.Text = "Type";
            // 
            // NameTextBox
            // 
            this.NameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.NameTextBox.Location = new System.Drawing.Point(65, 3);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.ReadOnly = true;
            this.NameTextBox.Size = new System.Drawing.Size(241, 20);
            this.NameTextBox.TabIndex = 34;
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(5, 6);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(33, 13);
            this.NameLabel.TabIndex = 33;
            this.NameLabel.Text = "Index";
            // 
            // SQLScriptIndexAttributes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "SQLScriptIndexAttributes";
            this.Size = new System.Drawing.Size(644, 138);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        internal System.Windows.Forms.ListView ColumnsListView;
        private System.Windows.Forms.Label ColumnsLabel;
        internal System.Windows.Forms.TextBox TypeTextBox;
        private System.Windows.Forms.Label TypeLabel;
        internal System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.Label NameLabel;
        internal System.Windows.Forms.TextBox ConstraintTextBox;
        private System.Windows.Forms.Label ConstraintLabel;

    }
}
