namespace EAAddIn.Windows
{
    partial class DatabaseReleaseManagerForm
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
            this.MaintainReleasesButton = new System.Windows.Forms.Button();
            this.CreateStereotypesButton = new System.Windows.Forms.Button();
            this.CleanupReleaseRepositoryButton = new System.Windows.Forms.Button();
            this.CleanupDbRepositoryButton = new System.Windows.Forms.Button();
            this.ReleaseLabel = new System.Windows.Forms.Label();
            this.ReleasesComboBox = new System.Windows.Forms.ComboBox();
            this.MessagesDataGridView = new System.Windows.Forms.DataGridView();
            this.MessagesContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MessagesLabel = new System.Windows.Forms.Label();
            this.DeleteStereotypesButton = new System.Windows.Forms.Button();
            this.toolTipProvider = new System.Windows.Forms.ToolTip(this.components);
            this.FindUsagesButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MessagesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // MaintainReleasesButton
            // 
            this.MaintainReleasesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MaintainReleasesButton.Location = new System.Drawing.Point(12, 225);
            this.MaintainReleasesButton.Name = "MaintainReleasesButton";
            this.MaintainReleasesButton.Size = new System.Drawing.Size(75, 23);
            this.MaintainReleasesButton.TabIndex = 1;
            this.MaintainReleasesButton.Text = "Releases";
            this.toolTipProvider.SetToolTip(this.MaintainReleasesButton, "Display the Maintain Releases screen.\r\n\r\nThis is where you create, update and del" +
                    "ete releases.");
            this.MaintainReleasesButton.UseVisualStyleBackColor = true;
            this.MaintainReleasesButton.Click += new System.EventHandler(this.MaintainReleasesButton_Click);
            // 
            // CreateStereotypesButton
            // 
            this.CreateStereotypesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CreateStereotypesButton.Location = new System.Drawing.Point(93, 225);
            this.CreateStereotypesButton.Name = "CreateStereotypesButton";
            this.CreateStereotypesButton.Size = new System.Drawing.Size(136, 23);
            this.CreateStereotypesButton.TabIndex = 2;
            this.CreateStereotypesButton.Text = "Create Stereotypes";
            this.toolTipProvider.SetToolTip(this.CreateStereotypesButton, "Create stereotypes for a release.\r\n\r\nThis is also automatically done when you cre" +
                    "ate a new release.");
            this.CreateStereotypesButton.UseVisualStyleBackColor = true;
            this.CreateStereotypesButton.Visible = false;
            this.CreateStereotypesButton.Click += new System.EventHandler(this.CreateStereotypesButton_Click);
            // 
            // CleanupReleaseRepositoryButton
            // 
            this.CleanupReleaseRepositoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CleanupReleaseRepositoryButton.Location = new System.Drawing.Point(540, 225);
            this.CleanupReleaseRepositoryButton.Name = "CleanupReleaseRepositoryButton";
            this.CleanupReleaseRepositoryButton.Size = new System.Drawing.Size(136, 23);
            this.CleanupReleaseRepositoryButton.TabIndex = 3;
            this.CleanupReleaseRepositoryButton.Text = "Cleanup Release";
            this.toolTipProvider.SetToolTip(this.CleanupReleaseRepositoryButton, "Remove stereotypes from tables in the main repository.");
            this.CleanupReleaseRepositoryButton.UseVisualStyleBackColor = true;
            this.CleanupReleaseRepositoryButton.Click += new System.EventHandler(this.CleanupEaRelease1Button_Click);
            // 
            // CleanupDbRepositoryButton
            // 
            this.CleanupDbRepositoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CleanupDbRepositoryButton.Location = new System.Drawing.Point(682, 225);
            this.CleanupDbRepositoryButton.Name = "CleanupDbRepositoryButton";
            this.CleanupDbRepositoryButton.Size = new System.Drawing.Size(136, 23);
            this.CleanupDbRepositoryButton.TabIndex = 4;
            this.CleanupDbRepositoryButton.Text = "Cleanup DB Repository";
            this.toolTipProvider.SetToolTip(this.CleanupDbRepositoryButton, "Remove future release stereotypes in the database repository");
            this.CleanupDbRepositoryButton.UseVisualStyleBackColor = true;
            this.CleanupDbRepositoryButton.Click += new System.EventHandler(this.CleanupEaDbRepositoryButton_Click);
            // 
            // ReleaseLabel
            // 
            this.ReleaseLabel.AutoSize = true;
            this.ReleaseLabel.Location = new System.Drawing.Point(18, 15);
            this.ReleaseLabel.Name = "ReleaseLabel";
            this.ReleaseLabel.Size = new System.Drawing.Size(49, 13);
            this.ReleaseLabel.TabIndex = 5;
            this.ReleaseLabel.Text = "Release:";
            // 
            // ReleasesComboBox
            // 
            this.ReleasesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ReleasesComboBox.FormattingEnabled = true;
            this.ReleasesComboBox.Location = new System.Drawing.Point(73, 12);
            this.ReleasesComboBox.Name = "ReleasesComboBox";
            this.ReleasesComboBox.Size = new System.Drawing.Size(234, 21);
            this.ReleasesComboBox.TabIndex = 6;
            this.ReleasesComboBox.SelectedIndexChanged += new System.EventHandler(this.ReleasesComboBox_SelectedIndexChanged);
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
            this.MessagesDataGridView.Location = new System.Drawing.Point(12, 58);
            this.MessagesDataGridView.Name = "MessagesDataGridView";
            this.MessagesDataGridView.ReadOnly = true;
            this.MessagesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.MessagesDataGridView.Size = new System.Drawing.Size(806, 145);
            this.MessagesDataGridView.TabIndex = 7;
            this.MessagesDataGridView.SelectionChanged += new System.EventHandler(this.MessagesDataGridView_SelectionChanged);
            // 
            // MessagesContextMenuStrip
            // 
            this.MessagesContextMenuStrip.Name = "MessagesContextMenuStrip";
            this.MessagesContextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // MessagesLabel
            // 
            this.MessagesLabel.AutoSize = true;
            this.MessagesLabel.Location = new System.Drawing.Point(12, 40);
            this.MessagesLabel.Name = "MessagesLabel";
            this.MessagesLabel.Size = new System.Drawing.Size(58, 13);
            this.MessagesLabel.TabIndex = 8;
            this.MessagesLabel.Text = "Messages:";
            // 
            // DeleteStereotypesButton
            // 
            this.DeleteStereotypesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteStereotypesButton.Location = new System.Drawing.Point(235, 225);
            this.DeleteStereotypesButton.Name = "DeleteStereotypesButton";
            this.DeleteStereotypesButton.Size = new System.Drawing.Size(136, 23);
            this.DeleteStereotypesButton.TabIndex = 9;
            this.DeleteStereotypesButton.Text = "Delete Stereotypes";
            this.toolTipProvider.SetToolTip(this.DeleteStereotypesButton, "Delete stereotypes for a release.");
            this.DeleteStereotypesButton.UseVisualStyleBackColor = true;
            this.DeleteStereotypesButton.Visible = false;
            this.DeleteStereotypesButton.Click += new System.EventHandler(this.DeleteStereotypesButton_Click);
            // 
            // FindUsagesButton
            // 
            this.FindUsagesButton.Location = new System.Drawing.Point(313, 12);
            this.FindUsagesButton.Name = "FindUsagesButton";
            this.FindUsagesButton.Size = new System.Drawing.Size(97, 23);
            this.FindUsagesButton.TabIndex = 10;
            this.FindUsagesButton.Text = "Find Usages";
            this.toolTipProvider.SetToolTip(this.FindUsagesButton, "Find usages of this stereotype.");
            this.FindUsagesButton.UseVisualStyleBackColor = true;
            this.FindUsagesButton.Click += new System.EventHandler(this.FindUsagesButton_Click);
            // 
            // DatabaseReleaseManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 260);
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
            this.Name = "DatabaseReleaseManagerForm";
            this.Text = "Database Release Manager";
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DatabaseReleaseManagerForm_MouseDoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.MessagesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button MaintainReleasesButton;
        private System.Windows.Forms.Button CreateStereotypesButton;
        private System.Windows.Forms.Button CleanupReleaseRepositoryButton;
        private System.Windows.Forms.Button CleanupDbRepositoryButton;
        private System.Windows.Forms.Label ReleaseLabel;
        private System.Windows.Forms.ComboBox ReleasesComboBox;
        private System.Windows.Forms.DataGridView MessagesDataGridView;
        private System.Windows.Forms.Label MessagesLabel;
        private System.Windows.Forms.Button DeleteStereotypesButton;
        private System.Windows.Forms.ToolTip toolTipProvider;
        private System.Windows.Forms.ContextMenuStrip MessagesContextMenuStrip;
        private System.Windows.Forms.Button FindUsagesButton;

    }
}