namespace EAAddIn.Windows
{
    partial class ReleaseForm
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
            this.ReleasesListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ReleaseDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.StreamComboBox = new System.Windows.Forms.ComboBox();
            this.NewButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ReleasesListBox
            // 
            this.ReleasesListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.ReleasesListBox.FormattingEnabled = true;
            this.ReleasesListBox.Location = new System.Drawing.Point(0, 0);
            this.ReleasesListBox.Name = "ReleasesListBox";
            this.ReleasesListBox.Size = new System.Drawing.Size(156, 264);
            this.ReleasesListBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(216, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Release Date:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ReleaseDateTimePicker
            // 
            this.ReleaseDateTimePicker.Location = new System.Drawing.Point(297, 12);
            this.ReleaseDateTimePicker.Name = "ReleaseDateTimePicker";
            this.ReleaseDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.ReleaseDateTimePicker.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(248, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Stream:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // StreamComboBox
            // 
            this.StreamComboBox.FormattingEnabled = true;
            this.StreamComboBox.Items.AddRange(new object[] {
            "Release",
            "Fix"});
            this.StreamComboBox.Location = new System.Drawing.Point(297, 38);
            this.StreamComboBox.Name = "StreamComboBox";
            this.StreamComboBox.Size = new System.Drawing.Size(132, 21);
            this.StreamComboBox.TabIndex = 4;
            this.StreamComboBox.Text = "Release";
            // 
            // NewButton
            // 
            this.NewButton.Location = new System.Drawing.Point(273, 231);
            this.NewButton.Name = "NewButton";
            this.NewButton.Size = new System.Drawing.Size(75, 23);
            this.NewButton.TabIndex = 5;
            this.NewButton.Text = "New";
            this.NewButton.UseVisualStyleBackColor = true;
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(354, 231);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 6;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(442, 231);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 7;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            // 
            // ReleaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 266);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.NewButton);
            this.Controls.Add(this.StreamComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ReleaseDateTimePicker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ReleasesListBox);
            this.Name = "ReleaseForm";
            this.Text = "Maintain Releases";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ReleasesListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker ReleaseDateTimePicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox StreamComboBox;
        private System.Windows.Forms.Button NewButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button SaveButton;
    }
}