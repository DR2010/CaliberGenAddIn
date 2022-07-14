﻿namespace EAAddIn.Windows
{
    partial class DatabaseReleaseToolbar
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
            this.ApplyStereotypeToTableButton = new System.Windows.Forms.Button();
            this.ReleaseLabel = new System.Windows.Forms.Label();
            this.ReleasesComboBox = new System.Windows.Forms.ComboBox();
            this.toolTipProvider = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.GetColumnsButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ToBeDeletedRadioButton = new System.Windows.Forms.RadioButton();
            this.NoChangesRadioButton = new System.Windows.Forms.RadioButton();
            this.ChangedTableRadioButton = new System.Windows.Forms.RadioButton();
            this.RenamedTableRadioButton = new System.Windows.Forms.RadioButton();
            this.DeletedTableRadioButton = new System.Windows.Forms.RadioButton();
            this.NewTableRadioButton = new System.Windows.Forms.RadioButton();
            this.ColumnsLabel = new System.Windows.Forms.Label();
            this.ColumnsDataGridView = new System.Windows.Forms.DataGridView();
            this.TableLabel = new System.Windows.Forms.Label();
            this.ElementNameTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.HideUnchangedColumnsButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnsDataGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ApplyStereotypeToTableButton
            // 
            this.ApplyStereotypeToTableButton.Location = new System.Drawing.Point(411, 88);
            this.ApplyStereotypeToTableButton.Name = "ApplyStereotypeToTableButton";
            this.ApplyStereotypeToTableButton.Size = new System.Drawing.Size(93, 23);
            this.ApplyStereotypeToTableButton.TabIndex = 4;
            this.ApplyStereotypeToTableButton.Text = "Apply";
            this.toolTipProvider.SetToolTip(this.ApplyStereotypeToTableButton, "Apply the change type to the selected tables on your diagram");
            this.ApplyStereotypeToTableButton.UseVisualStyleBackColor = true;
            this.ApplyStereotypeToTableButton.Click += new System.EventHandler(this.ApplyStereotypeToTableButton_Click);
            // 
            // ReleaseLabel
            // 
            this.ReleaseLabel.AutoSize = true;
            this.ReleaseLabel.Location = new System.Drawing.Point(18, 15);
            this.ReleaseLabel.Name = "ReleaseLabel";
            this.ReleaseLabel.Size = new System.Drawing.Size(49, 13);
            this.ReleaseLabel.TabIndex = 0;
            this.ReleaseLabel.Text = "Release:";
            // 
            // ReleasesComboBox
            // 
            this.ReleasesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ReleasesComboBox.FormattingEnabled = true;
            this.ReleasesComboBox.Location = new System.Drawing.Point(73, 12);
            this.ReleasesComboBox.Name = "ReleasesComboBox";
            this.ReleasesComboBox.Size = new System.Drawing.Size(234, 21);
            this.ReleasesComboBox.TabIndex = 1;
            this.ReleasesComboBox.SelectedIndexChanged += new System.EventHandler(this.ReleasesComboBox_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(347, 297);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Apply to Columns";
            this.toolTipProvider.SetToolTip(this.button1, "Apply the change type to the selected columns");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GetColumnsButton
            // 
            this.GetColumnsButton.Location = new System.Drawing.Point(399, 18);
            this.GetColumnsButton.Name = "GetColumnsButton";
            this.GetColumnsButton.Size = new System.Drawing.Size(83, 23);
            this.GetColumnsButton.TabIndex = 3;
            this.GetColumnsButton.Text = "Get Columns";
            this.toolTipProvider.SetToolTip(this.GetColumnsButton, "Get the columns for the selected table");
            this.GetColumnsButton.UseVisualStyleBackColor = true;
            this.GetColumnsButton.Click += new System.EventHandler(this.GetColumnsButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ToBeDeletedRadioButton);
            this.groupBox1.Controls.Add(this.NoChangesRadioButton);
            this.groupBox1.Controls.Add(this.ChangedTableRadioButton);
            this.groupBox1.Controls.Add(this.RenamedTableRadioButton);
            this.groupBox1.Controls.Add(this.DeletedTableRadioButton);
            this.groupBox1.Controls.Add(this.NewTableRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(21, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 42);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Change Type";
            // 
            // ToBeDeletedRadioButton
            // 
            this.ToBeDeletedRadioButton.AutoSize = true;
            this.ToBeDeletedRadioButton.Location = new System.Drawing.Point(290, 19);
            this.ToBeDeletedRadioButton.Name = "ToBeDeletedRadioButton";
            this.ToBeDeletedRadioButton.Size = new System.Drawing.Size(91, 17);
            this.ToBeDeletedRadioButton.TabIndex = 5;
            this.ToBeDeletedRadioButton.Text = "To be deleted";
            this.ToBeDeletedRadioButton.UseVisualStyleBackColor = true;
            // 
            // NoChangesRadioButton
            // 
            this.NoChangesRadioButton.AutoSize = true;
            this.NoChangesRadioButton.Location = new System.Drawing.Point(393, 19);
            this.NoChangesRadioButton.Name = "NoChangesRadioButton";
            this.NoChangesRadioButton.Size = new System.Drawing.Size(83, 17);
            this.NoChangesRadioButton.TabIndex = 4;
            this.NoChangesRadioButton.Text = "No changes";
            this.NoChangesRadioButton.UseVisualStyleBackColor = true;
            // 
            // ChangedTableRadioButton
            // 
            this.ChangedTableRadioButton.AutoSize = true;
            this.ChangedTableRadioButton.Location = new System.Drawing.Point(216, 19);
            this.ChangedTableRadioButton.Name = "ChangedTableRadioButton";
            this.ChangedTableRadioButton.Size = new System.Drawing.Size(68, 17);
            this.ChangedTableRadioButton.TabIndex = 3;
            this.ChangedTableRadioButton.Text = "Changed";
            this.ChangedTableRadioButton.UseVisualStyleBackColor = true;
            // 
            // RenamedTableRadioButton
            // 
            this.RenamedTableRadioButton.AutoSize = true;
            this.RenamedTableRadioButton.Location = new System.Drawing.Point(135, 19);
            this.RenamedTableRadioButton.Name = "RenamedTableRadioButton";
            this.RenamedTableRadioButton.Size = new System.Drawing.Size(71, 17);
            this.RenamedTableRadioButton.TabIndex = 2;
            this.RenamedTableRadioButton.Text = "Renamed";
            this.RenamedTableRadioButton.UseVisualStyleBackColor = true;
            // 
            // DeletedTableRadioButton
            // 
            this.DeletedTableRadioButton.AutoSize = true;
            this.DeletedTableRadioButton.Location = new System.Drawing.Point(63, 19);
            this.DeletedTableRadioButton.Name = "DeletedTableRadioButton";
            this.DeletedTableRadioButton.Size = new System.Drawing.Size(62, 17);
            this.DeletedTableRadioButton.TabIndex = 1;
            this.DeletedTableRadioButton.Text = "Deleted";
            this.DeletedTableRadioButton.UseVisualStyleBackColor = true;
            // 
            // NewTableRadioButton
            // 
            this.NewTableRadioButton.AutoSize = true;
            this.NewTableRadioButton.Checked = true;
            this.NewTableRadioButton.Location = new System.Drawing.Point(6, 19);
            this.NewTableRadioButton.Name = "NewTableRadioButton";
            this.NewTableRadioButton.Size = new System.Drawing.Size(47, 17);
            this.NewTableRadioButton.TabIndex = 0;
            this.NewTableRadioButton.TabStop = true;
            this.NewTableRadioButton.Text = "New";
            this.NewTableRadioButton.UseVisualStyleBackColor = true;
            // 
            // ColumnsLabel
            // 
            this.ColumnsLabel.AutoSize = true;
            this.ColumnsLabel.Location = new System.Drawing.Point(9, 45);
            this.ColumnsLabel.Name = "ColumnsLabel";
            this.ColumnsLabel.Size = new System.Drawing.Size(50, 13);
            this.ColumnsLabel.TabIndex = 7;
            this.ColumnsLabel.Text = "Columns:";
            // 
            // ColumnsDataGridView
            // 
            this.ColumnsDataGridView.AllowUserToAddRows = false;
            this.ColumnsDataGridView.AllowUserToDeleteRows = false;
            this.ColumnsDataGridView.AllowUserToOrderColumns = true;
            this.ColumnsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ColumnsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ColumnsDataGridView.Location = new System.Drawing.Point(9, 62);
            this.ColumnsDataGridView.Name = "ColumnsDataGridView";
            this.ColumnsDataGridView.ReadOnly = true;
            this.ColumnsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ColumnsDataGridView.Size = new System.Drawing.Size(474, 229);
            this.ColumnsDataGridView.TabIndex = 8;
            // 
            // TableLabel
            // 
            this.TableLabel.AutoSize = true;
            this.TableLabel.Location = new System.Drawing.Point(12, 24);
            this.TableLabel.Name = "TableLabel";
            this.TableLabel.Size = new System.Drawing.Size(37, 13);
            this.TableLabel.TabIndex = 5;
            this.TableLabel.Text = "Table:";
            // 
            // ElementNameTextBox
            // 
            this.ElementNameTextBox.Enabled = false;
            this.ElementNameTextBox.Location = new System.Drawing.Point(53, 21);
            this.ElementNameTextBox.Name = "ElementNameTextBox";
            this.ElementNameTextBox.ReadOnly = true;
            this.ElementNameTextBox.Size = new System.Drawing.Size(340, 20);
            this.ElementNameTextBox.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.ColumnsDataGridView);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.ElementNameTextBox);
            this.groupBox2.Controls.Add(this.GetColumnsButton);
            this.groupBox2.Controls.Add(this.TableLabel);
            this.groupBox2.Controls.Add(this.ColumnsLabel);
            this.groupBox2.Location = new System.Drawing.Point(12, 117);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(492, 326);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Columns";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // HideUnchangedColumnsButton
            // 
            this.HideUnchangedColumnsButton.Location = new System.Drawing.Point(12, 88);
            this.HideUnchangedColumnsButton.Name = "HideUnchangedColumnsButton";
            this.HideUnchangedColumnsButton.Size = new System.Drawing.Size(159, 23);
            this.HideUnchangedColumnsButton.TabIndex = 11;
            this.HideUnchangedColumnsButton.Text = "Hide Unchanged Columns";
            this.HideUnchangedColumnsButton.UseVisualStyleBackColor = true;
            this.HideUnchangedColumnsButton.Click += new System.EventHandler(this.HideUnchangedColumnsButton_Click);
            // 
            // DatabaseReleaseToolbar
            // 
            this.AcceptButton = this.ApplyStereotypeToTableButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 451);
            this.Controls.Add(this.HideUnchangedColumnsButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ReleasesComboBox);
            this.Controls.Add(this.ReleaseLabel);
            this.Controls.Add(this.ApplyStereotypeToTableButton);
            this.Name = "DatabaseReleaseToolbar";
            this.Text = "Database Release Toolbar";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnsDataGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ApplyStereotypeToTableButton;
        private System.Windows.Forms.Label ReleaseLabel;
        private System.Windows.Forms.ComboBox ReleasesComboBox;
        private System.Windows.Forms.ToolTip toolTipProvider;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton ChangedTableRadioButton;
        private System.Windows.Forms.RadioButton RenamedTableRadioButton;
        private System.Windows.Forms.RadioButton DeletedTableRadioButton;
        private System.Windows.Forms.RadioButton NewTableRadioButton;
        private System.Windows.Forms.Button GetColumnsButton;
        private System.Windows.Forms.Label ColumnsLabel;
        private System.Windows.Forms.DataGridView ColumnsDataGridView;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label TableLabel;
        private System.Windows.Forms.TextBox ElementNameTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton NoChangesRadioButton;
        private System.Windows.Forms.RadioButton ToBeDeletedRadioButton;
        private System.Windows.Forms.Button HideUnchangedColumnsButton;

    }
}