namespace EAAddIn.Windows
{
    partial class DesignSignOffReport
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
            this.CreateReportButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ProjectComboBox = new System.Windows.Forms.ComboBox();
            this.ProjectYearTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.IncludeArchivedProjectsCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // CreateReportButton
            // 
            this.CreateReportButton.Location = new System.Drawing.Point(594, 33);
            this.CreateReportButton.Name = "CreateReportButton";
            this.CreateReportButton.Size = new System.Drawing.Size(107, 36);
            this.CreateReportButton.TabIndex = 0;
            this.CreateReportButton.Text = "Create Report";
            this.CreateReportButton.UseVisualStyleBackColor = true;
            this.CreateReportButton.Click += new System.EventHandler(this.CreateReportButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Project:";
            // 
            // ProjectComboBox
            // 
            this.ProjectComboBox.FormattingEnabled = true;
            this.ProjectComboBox.Location = new System.Drawing.Point(61, 6);
            this.ProjectComboBox.Name = "ProjectComboBox";
            this.ProjectComboBox.Size = new System.Drawing.Size(509, 21);
            this.ProjectComboBox.Sorted = true;
            this.ProjectComboBox.TabIndex = 2;
            // 
            // ProjectYearTextBox
            // 
            this.ProjectYearTextBox.Location = new System.Drawing.Point(608, 6);
            this.ProjectYearTextBox.Name = "ProjectYearTextBox";
            this.ProjectYearTextBox.Size = new System.Drawing.Size(93, 20);
            this.ProjectYearTextBox.TabIndex = 3;
            this.ProjectYearTextBox.Text = "projects 0910";
            this.ProjectYearTextBox.Validated += new System.EventHandler(this.DesignSignOffReport_Load);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(576, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Year:";
            // 
            // IncludeArchivedProjectsCheckBox
            // 
            this.IncludeArchivedProjectsCheckBox.AutoSize = true;
            this.IncludeArchivedProjectsCheckBox.Location = new System.Drawing.Point(15, 44);
            this.IncludeArchivedProjectsCheckBox.Name = "IncludeArchivedProjectsCheckBox";
            this.IncludeArchivedProjectsCheckBox.Size = new System.Drawing.Size(153, 17);
            this.IncludeArchivedProjectsCheckBox.TabIndex = 5;
            this.IncludeArchivedProjectsCheckBox.Text = "Include Archived Projects?";
            this.IncludeArchivedProjectsCheckBox.UseVisualStyleBackColor = true;
            this.IncludeArchivedProjectsCheckBox.Validated += new System.EventHandler(this.DesignSignOffReport_Load);
            // 
            // DesignSignOffReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 81);
            this.Controls.Add(this.IncludeArchivedProjectsCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ProjectYearTextBox);
            this.Controls.Add(this.ProjectComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CreateReportButton);
            this.Name = "DesignSignOffReport";
            this.Text = "Design Sign-Off Report";
            this.Load += new System.EventHandler(this.DesignSignOffReport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CreateReportButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ProjectComboBox;
        private System.Windows.Forms.TextBox ProjectYearTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox IncludeArchivedProjectsCheckBox;
    }
}