namespace EAAddIn.Windows
{
    partial class MaintainReleases
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
            this.ReleaseLabel = new System.Windows.Forms.Label();
            this.ReleaseDateDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.ReleaseDateLabel = new System.Windows.Forms.Label();
            this.StreamLabel = new System.Windows.Forms.Label();
            this.ReleaseStreamComboBox = new System.Windows.Forms.ComboBox();
            this.NewButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ReleasesListBox = new System.Windows.Forms.ListBox();
            this.ReleaseNameLabel = new System.Windows.Forms.Label();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.MessagesLabel = new System.Windows.Forms.Label();
            this.MessagesDataGridView = new System.Windows.Forms.DataGridView();
            this.toolTipProvider = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.MessagesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ReleaseLabel
            // 
            this.ReleaseLabel.AutoSize = true;
            this.ReleaseLabel.Location = new System.Drawing.Point(3, 9);
            this.ReleaseLabel.Name = "ReleaseLabel";
            this.ReleaseLabel.Size = new System.Drawing.Size(54, 13);
            this.ReleaseLabel.TabIndex = 7;
            this.ReleaseLabel.Text = "Releases:";
            // 
            // ReleaseDateDateTimePicker
            // 
            this.ReleaseDateDateTimePicker.Location = new System.Drawing.Point(281, 58);
            this.ReleaseDateDateTimePicker.Name = "ReleaseDateDateTimePicker";
            this.ReleaseDateDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.ReleaseDateDateTimePicker.TabIndex = 9;
            // 
            // ReleaseDateLabel
            // 
            this.ReleaseDateLabel.AutoSize = true;
            this.ReleaseDateLabel.Location = new System.Drawing.Point(200, 62);
            this.ReleaseDateLabel.Name = "ReleaseDateLabel";
            this.ReleaseDateLabel.Size = new System.Drawing.Size(75, 13);
            this.ReleaseDateLabel.TabIndex = 10;
            this.ReleaseDateLabel.Text = "Release Date:";
            // 
            // StreamLabel
            // 
            this.StreamLabel.AutoSize = true;
            this.StreamLabel.Location = new System.Drawing.Point(232, 88);
            this.StreamLabel.Name = "StreamLabel";
            this.StreamLabel.Size = new System.Drawing.Size(43, 13);
            this.StreamLabel.TabIndex = 11;
            this.StreamLabel.Text = "Stream:";
            // 
            // ReleaseStreamComboBox
            // 
            this.ReleaseStreamComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ReleaseStreamComboBox.FormattingEnabled = true;
            this.ReleaseStreamComboBox.Location = new System.Drawing.Point(281, 85);
            this.ReleaseStreamComboBox.Name = "ReleaseStreamComboBox";
            this.ReleaseStreamComboBox.Size = new System.Drawing.Size(156, 21);
            this.ReleaseStreamComboBox.TabIndex = 12;
            // 
            // NewButton
            // 
            this.NewButton.Location = new System.Drawing.Point(244, 148);
            this.NewButton.Name = "NewButton";
            this.NewButton.Size = new System.Drawing.Size(75, 23);
            this.NewButton.TabIndex = 13;
            this.NewButton.Text = "New";
            this.toolTipProvider.SetToolTip(this.NewButton, "Clear the fields ready for entry of a new release.");
            this.NewButton.UseVisualStyleBackColor = true;
            this.NewButton.Click += new System.EventHandler(this.NewButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(325, 148);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 14;
            this.DeleteButton.Text = "Delete";
            this.toolTipProvider.SetToolTip(this.DeleteButton, "Delete the current release.");
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(406, 148);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 15;
            this.SaveButton.Text = "Save";
            this.toolTipProvider.SetToolTip(this.SaveButton, "Save a new release or changes  to the selected release.");
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ReleasesListBox
            // 
            this.ReleasesListBox.FormattingEnabled = true;
            this.ReleasesListBox.Location = new System.Drawing.Point(6, 25);
            this.ReleasesListBox.Name = "ReleasesListBox";
            this.ReleasesListBox.Size = new System.Drawing.Size(188, 147);
            this.ReleasesListBox.TabIndex = 16;
            this.toolTipProvider.SetToolTip(this.ReleasesListBox, "List of releases.");
            this.ReleasesListBox.SelectedIndexChanged += new System.EventHandler(this.ReleasesListBox_SelectedIndexChanged);
            // 
            // ReleaseNameLabel
            // 
            this.ReleaseNameLabel.AutoSize = true;
            this.ReleaseNameLabel.Location = new System.Drawing.Point(237, 35);
            this.ReleaseNameLabel.Name = "ReleaseNameLabel";
            this.ReleaseNameLabel.Size = new System.Drawing.Size(38, 13);
            this.ReleaseNameLabel.TabIndex = 17;
            this.ReleaseNameLabel.Text = "Name:";
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(281, 32);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(101, 20);
            this.NameTextBox.TabIndex = 18;
            // 
            // MessagesLabel
            // 
            this.MessagesLabel.AutoSize = true;
            this.MessagesLabel.Location = new System.Drawing.Point(6, 181);
            this.MessagesLabel.Name = "MessagesLabel";
            this.MessagesLabel.Size = new System.Drawing.Size(58, 13);
            this.MessagesLabel.TabIndex = 20;
            this.MessagesLabel.Text = "Messages:";
            // 
            // MessagesDataGridView
            // 
            this.MessagesDataGridView.AllowUserToAddRows = false;
            this.MessagesDataGridView.AllowUserToDeleteRows = false;
            this.MessagesDataGridView.AllowUserToOrderColumns = true;
            this.MessagesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MessagesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MessagesDataGridView.Location = new System.Drawing.Point(6, 199);
            this.MessagesDataGridView.Name = "MessagesDataGridView";
            this.MessagesDataGridView.ReadOnly = true;
            this.MessagesDataGridView.Size = new System.Drawing.Size(540, 99);
            this.MessagesDataGridView.TabIndex = 19;
            // 
            // MaintainReleases
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 310);
            this.Controls.Add(this.MessagesLabel);
            this.Controls.Add(this.MessagesDataGridView);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.ReleaseNameLabel);
            this.Controls.Add(this.ReleasesListBox);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.NewButton);
            this.Controls.Add(this.ReleaseStreamComboBox);
            this.Controls.Add(this.StreamLabel);
            this.Controls.Add(this.ReleaseDateLabel);
            this.Controls.Add(this.ReleaseDateDateTimePicker);
            this.Controls.Add(this.ReleaseLabel);
            this.Name = "MaintainReleases";
            this.Text = "Release Maintenance";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MaintainReleases_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.MessagesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ReleaseLabel;
        private System.Windows.Forms.DateTimePicker ReleaseDateDateTimePicker;
        private System.Windows.Forms.Label ReleaseDateLabel;
        private System.Windows.Forms.Label StreamLabel;
        private System.Windows.Forms.ComboBox ReleaseStreamComboBox;
        private System.Windows.Forms.Button NewButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.ListBox ReleasesListBox;
        private System.Windows.Forms.Label ReleaseNameLabel;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.Label MessagesLabel;
        private System.Windows.Forms.DataGridView MessagesDataGridView;
        private System.Windows.Forms.ToolTip toolTipProvider;
    }
}