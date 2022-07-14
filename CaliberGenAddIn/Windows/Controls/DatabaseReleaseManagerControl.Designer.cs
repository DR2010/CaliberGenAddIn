namespace EAAddIn.Windows.Controls
{
    partial class DatabaseReleaseManagerControl
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
            this.FindUsagesButton = new System.Windows.Forms.Button();
            this.DeleteStereotypesButton = new System.Windows.Forms.Button();
            this.MessagesLabel = new System.Windows.Forms.Label();
            this.MessagesDataGridView = new System.Windows.Forms.DataGridView();
            this.ReleasesComboBox = new System.Windows.Forms.ComboBox();
            this.ReleaseLabel = new System.Windows.Forms.Label();
            this.CleanupDbRepositoryButton = new System.Windows.Forms.Button();
            this.CleanupReleaseRepositoryButton = new System.Windows.Forms.Button();
            this.CreateStereotypesButton = new System.Windows.Forms.Button();
            this.MaintainReleasesButton = new System.Windows.Forms.Button();
            this.MessagesContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.MessagesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // FindUsagesButton
            // 
            this.FindUsagesButton.Location = new System.Drawing.Point(304, 3);
            this.FindUsagesButton.Name = "FindUsagesButton";
            this.FindUsagesButton.Size = new System.Drawing.Size(97, 23);
            this.FindUsagesButton.TabIndex = 20;
            this.FindUsagesButton.Text = "Find Usages";
            this.FindUsagesButton.UseVisualStyleBackColor = true;
            this.FindUsagesButton.Click += new System.EventHandler(this.FindUsagesButton_Click);
            // 
            // DeleteStereotypesButton
            // 
            this.DeleteStereotypesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteStereotypesButton.Location = new System.Drawing.Point(228, 293);
            this.DeleteStereotypesButton.Name = "DeleteStereotypesButton";
            this.DeleteStereotypesButton.Size = new System.Drawing.Size(136, 23);
            this.DeleteStereotypesButton.TabIndex = 19;
            this.DeleteStereotypesButton.Text = "Delete Stereotypes";
            this.DeleteStereotypesButton.UseVisualStyleBackColor = true;
            this.DeleteStereotypesButton.Visible = false;
            this.DeleteStereotypesButton.Click += new System.EventHandler(this.DeleteStereotypesButton_Click);
            // 
            // MessagesLabel
            // 
            this.MessagesLabel.AutoSize = true;
            this.MessagesLabel.Location = new System.Drawing.Point(3, 31);
            this.MessagesLabel.Name = "MessagesLabel";
            this.MessagesLabel.Size = new System.Drawing.Size(58, 13);
            this.MessagesLabel.TabIndex = 18;
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
            this.MessagesDataGridView.ContextMenuStrip = this.MessagesContextMenuStrip;
            this.MessagesDataGridView.Location = new System.Drawing.Point(3, 49);
            this.MessagesDataGridView.Name = "MessagesDataGridView";
            this.MessagesDataGridView.ReadOnly = true;
            this.MessagesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.MessagesDataGridView.Size = new System.Drawing.Size(911, 238);
            this.MessagesDataGridView.TabIndex = 17;
            this.MessagesDataGridView.SelectionChanged += new System.EventHandler(this.MessagesDataGridView_SelectionChanged);
            // 
            // ReleasesComboBox
            // 
            this.ReleasesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ReleasesComboBox.FormattingEnabled = true;
            this.ReleasesComboBox.Location = new System.Drawing.Point(64, 3);
            this.ReleasesComboBox.Name = "ReleasesComboBox";
            this.ReleasesComboBox.Size = new System.Drawing.Size(234, 21);
            this.ReleasesComboBox.TabIndex = 16;
            this.ReleasesComboBox.SelectedIndexChanged += new System.EventHandler(this.ReleasesComboBox_SelectedIndexChanged);
            // 
            // ReleaseLabel
            // 
            this.ReleaseLabel.AutoSize = true;
            this.ReleaseLabel.Location = new System.Drawing.Point(9, 6);
            this.ReleaseLabel.Name = "ReleaseLabel";
            this.ReleaseLabel.Size = new System.Drawing.Size(49, 13);
            this.ReleaseLabel.TabIndex = 15;
            this.ReleaseLabel.Text = "Release:";
            // 
            // CleanupDbRepositoryButton
            // 
            this.CleanupDbRepositoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CleanupDbRepositoryButton.Location = new System.Drawing.Point(778, 293);
            this.CleanupDbRepositoryButton.Name = "CleanupDbRepositoryButton";
            this.CleanupDbRepositoryButton.Size = new System.Drawing.Size(136, 23);
            this.CleanupDbRepositoryButton.TabIndex = 14;
            this.CleanupDbRepositoryButton.Text = "Cleanup DB Repository";
            this.CleanupDbRepositoryButton.UseVisualStyleBackColor = true;
            this.CleanupDbRepositoryButton.Click += new System.EventHandler(this.CleanupEaDbRepositoryButton_Click);
            // 
            // CleanupReleaseRepositoryButton
            // 
            this.CleanupReleaseRepositoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CleanupReleaseRepositoryButton.Location = new System.Drawing.Point(636, 293);
            this.CleanupReleaseRepositoryButton.Name = "CleanupReleaseRepositoryButton";
            this.CleanupReleaseRepositoryButton.Size = new System.Drawing.Size(136, 23);
            this.CleanupReleaseRepositoryButton.TabIndex = 13;
            this.CleanupReleaseRepositoryButton.Text = "Cleanup Release";
            this.CleanupReleaseRepositoryButton.UseVisualStyleBackColor = true;
            this.CleanupReleaseRepositoryButton.Click += new System.EventHandler(this.CleanupEaRelease1Button_Click);
            // 
            // CreateStereotypesButton
            // 
            this.CreateStereotypesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CreateStereotypesButton.Location = new System.Drawing.Point(86, 293);
            this.CreateStereotypesButton.Name = "CreateStereotypesButton";
            this.CreateStereotypesButton.Size = new System.Drawing.Size(136, 23);
            this.CreateStereotypesButton.TabIndex = 12;
            this.CreateStereotypesButton.Text = "Create Stereotypes";
            this.CreateStereotypesButton.UseVisualStyleBackColor = true;
            this.CreateStereotypesButton.Visible = false;
            this.CreateStereotypesButton.Click += new System.EventHandler(this.CreateStereotypesButton_Click);
            // 
            // MaintainReleasesButton
            // 
            this.MaintainReleasesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MaintainReleasesButton.Location = new System.Drawing.Point(5, 293);
            this.MaintainReleasesButton.Name = "MaintainReleasesButton";
            this.MaintainReleasesButton.Size = new System.Drawing.Size(75, 23);
            this.MaintainReleasesButton.TabIndex = 11;
            this.MaintainReleasesButton.Text = "Releases";
            this.MaintainReleasesButton.UseVisualStyleBackColor = true;
            this.MaintainReleasesButton.Click += new System.EventHandler(this.MaintainReleasesButton_Click);
            // 
            // MessagesContextMenuStrip
            // 
            this.MessagesContextMenuStrip.Name = "MessagesContextMenuStrip";
            this.MessagesContextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // DatabaseReleaseManagerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FindUsagesButton);
            this.Controls.Add(this.DeleteStereotypesButton);
            this.Controls.Add(this.MessagesLabel);
            this.Controls.Add(this.MessagesDataGridView);
            this.Controls.Add(this.ReleasesComboBox);
            this.Controls.Add(this.ReleaseLabel);
            this.Controls.Add(this.CleanupDbRepositoryButton);
            this.Controls.Add(this.CleanupReleaseRepositoryButton);
            this.Controls.Add(this.CreateStereotypesButton);
            this.Controls.Add(this.MaintainReleasesButton);
            this.Name = "DatabaseReleaseManagerControl";
            this.Size = new System.Drawing.Size(917, 319);
            ((System.ComponentModel.ISupportInitialize)(this.MessagesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FindUsagesButton;
        private System.Windows.Forms.Button DeleteStereotypesButton;
        private System.Windows.Forms.Label MessagesLabel;
        private System.Windows.Forms.DataGridView MessagesDataGridView;
        private System.Windows.Forms.ComboBox ReleasesComboBox;
        private System.Windows.Forms.Label ReleaseLabel;
        private System.Windows.Forms.Button CleanupDbRepositoryButton;
        private System.Windows.Forms.Button CleanupReleaseRepositoryButton;
        private System.Windows.Forms.Button CreateStereotypesButton;
        private System.Windows.Forms.Button MaintainReleasesButton;
        private System.Windows.Forms.ContextMenuStrip MessagesContextMenuStrip;
    }
}
