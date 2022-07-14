namespace EAAddIn.Windows
{
    partial class SQLScriptCreator
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
            this.TopPanel = new System.Windows.Forms.Panel();
            this.DatabaseTextBox = new System.Windows.Forms.TextBox();
            this.DatabaseLabel = new System.Windows.Forms.Label();
            this.ServerLabel = new System.Windows.Forms.Label();
            this.SqlServerConnectionTextBox = new System.Windows.Forms.TextBox();
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.progressToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.TableDefinitionsSplitContainer = new System.Windows.Forms.SplitContainer();
            this.CompareButton = new System.Windows.Forms.Button();
            this.ObjectTextBox = new System.Windows.Forms.TextBox();
            this.ObjectLabel = new System.Windows.Forms.Label();
            this.CreateScriptButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.EASchemaTreeView = new System.Windows.Forms.TreeView();
            this.ThisImageList = new System.Windows.Forms.ImageList(this.components);
            this.SQLSchemaTreeView = new System.Windows.Forms.TreeView();
            this.TopPanel.SuspendLayout();
            this.BottomPanel.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.TableDefinitionsSplitContainer.Panel1.SuspendLayout();
            this.TableDefinitionsSplitContainer.Panel2.SuspendLayout();
            this.TableDefinitionsSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.DatabaseTextBox);
            this.TopPanel.Controls.Add(this.DatabaseLabel);
            this.TopPanel.Controls.Add(this.ServerLabel);
            this.TopPanel.Controls.Add(this.SqlServerConnectionTextBox);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(938, 39);
            this.TopPanel.TabIndex = 0;
            // 
            // DatabaseTextBox
            // 
            this.DatabaseTextBox.Location = new System.Drawing.Point(84, 12);
            this.DatabaseTextBox.Name = "DatabaseTextBox";
            this.DatabaseTextBox.Size = new System.Drawing.Size(211, 20);
            this.DatabaseTextBox.TabIndex = 9;
            // 
            // DatabaseLabel
            // 
            this.DatabaseLabel.AutoSize = true;
            this.DatabaseLabel.Location = new System.Drawing.Point(15, 15);
            this.DatabaseLabel.Name = "DatabaseLabel";
            this.DatabaseLabel.Size = new System.Drawing.Size(53, 13);
            this.DatabaseLabel.TabIndex = 11;
            this.DatabaseLabel.Text = "Database";
            // 
            // ServerLabel
            // 
            this.ServerLabel.AutoSize = true;
            this.ServerLabel.Location = new System.Drawing.Point(314, 15);
            this.ServerLabel.Name = "ServerLabel";
            this.ServerLabel.Size = new System.Drawing.Size(91, 13);
            this.ServerLabel.TabIndex = 10;
            this.ServerLabel.Text = "Connection String";
            // 
            // SqlServerConnectionTextBox
            // 
            this.SqlServerConnectionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SqlServerConnectionTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::EAAddIn.Properties.Settings.Default, "SQLServerScriptGeneratorConnectionString", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.SqlServerConnectionTextBox.Location = new System.Drawing.Point(411, 12);
            this.SqlServerConnectionTextBox.Name = "SqlServerConnectionTextBox";
            this.SqlServerConnectionTextBox.Size = new System.Drawing.Size(524, 20);
            this.SqlServerConnectionTextBox.TabIndex = 8;
            this.SqlServerConnectionTextBox.Text = global::EAAddIn.Properties.Settings.Default.SQLServerScriptGeneratorConnectionString;
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.toolStrip);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 485);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(938, 165);
            this.BottomPanel.TabIndex = 1;
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.progressToolStripLabel});
            this.toolStrip.Location = new System.Drawing.Point(0, 140);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(938, 25);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 22);
            this.toolStripProgressBar.Visible = false;
            // 
            // progressToolStripLabel
            // 
            this.progressToolStripLabel.Name = "progressToolStripLabel";
            this.progressToolStripLabel.Size = new System.Drawing.Size(119, 22);
            this.progressToolStripLabel.Text = "Refreshing EA tables...";
            this.progressToolStripLabel.Visible = false;
            // 
            // TableDefinitionsSplitContainer
            // 
            this.TableDefinitionsSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableDefinitionsSplitContainer.Location = new System.Drawing.Point(0, 39);
            this.TableDefinitionsSplitContainer.Name = "TableDefinitionsSplitContainer";
            // 
            // TableDefinitionsSplitContainer.Panel1
            // 
            this.TableDefinitionsSplitContainer.Panel1.Controls.Add(this.CompareButton);
            this.TableDefinitionsSplitContainer.Panel1.Controls.Add(this.ObjectTextBox);
            this.TableDefinitionsSplitContainer.Panel1.Controls.Add(this.ObjectLabel);
            this.TableDefinitionsSplitContainer.Panel1.Controls.Add(this.CreateScriptButton);
            this.TableDefinitionsSplitContainer.Panel1.Controls.Add(this.RefreshButton);
            this.TableDefinitionsSplitContainer.Panel1.Controls.Add(this.EASchemaTreeView);
            // 
            // TableDefinitionsSplitContainer.Panel2
            // 
            this.TableDefinitionsSplitContainer.Panel2.Controls.Add(this.SQLSchemaTreeView);
            this.TableDefinitionsSplitContainer.Size = new System.Drawing.Size(938, 446);
            this.TableDefinitionsSplitContainer.SplitterDistance = 479;
            this.TableDefinitionsSplitContainer.TabIndex = 2;
            // 
            // CompareButton
            // 
            this.CompareButton.Location = new System.Drawing.Point(191, 8);
            this.CompareButton.Name = "CompareButton";
            this.CompareButton.Size = new System.Drawing.Size(101, 23);
            this.CompareButton.TabIndex = 25;
            this.CompareButton.Text = "Compare";
            this.CompareButton.UseVisualStyleBackColor = true;
            this.CompareButton.Click += new System.EventHandler(this.CompareButton_Click);
            // 
            // ObjectTextBox
            // 
            this.ObjectTextBox.AcceptsReturn = true;
            this.ObjectTextBox.AcceptsTab = true;
            this.ObjectTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ObjectTextBox.Enabled = false;
            this.ObjectTextBox.Location = new System.Drawing.Point(68, 420);
            this.ObjectTextBox.Name = "ObjectTextBox";
            this.ObjectTextBox.Size = new System.Drawing.Size(253, 20);
            this.ObjectTextBox.TabIndex = 24;
            // 
            // ObjectLabel
            // 
            this.ObjectLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ObjectLabel.AutoSize = true;
            this.ObjectLabel.Location = new System.Drawing.Point(3, 423);
            this.ObjectLabel.Name = "ObjectLabel";
            this.ObjectLabel.Size = new System.Drawing.Size(35, 13);
            this.ObjectLabel.TabIndex = 23;
            this.ObjectLabel.Text = "Name";
            // 
            // CreateScriptButton
            // 
            this.CreateScriptButton.Location = new System.Drawing.Point(84, 8);
            this.CreateScriptButton.Name = "CreateScriptButton";
            this.CreateScriptButton.Size = new System.Drawing.Size(101, 23);
            this.CreateScriptButton.TabIndex = 22;
            this.CreateScriptButton.Text = "Create Script";
            this.CreateScriptButton.UseVisualStyleBackColor = true;
            this.CreateScriptButton.Click += new System.EventHandler(this.CreateScriptButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(4, 8);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(75, 23);
            this.RefreshButton.TabIndex = 21;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // EASchemaTreeView
            // 
            this.EASchemaTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.EASchemaTreeView.ImageIndex = 0;
            this.EASchemaTreeView.ImageList = this.ThisImageList;
            this.EASchemaTreeView.Location = new System.Drawing.Point(2, 37);
            this.EASchemaTreeView.Name = "EASchemaTreeView";
            this.EASchemaTreeView.SelectedImageIndex = 0;
            this.EASchemaTreeView.Size = new System.Drawing.Size(476, 380);
            this.EASchemaTreeView.TabIndex = 20;
            this.EASchemaTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.EATablesTreeView_AfterSelect);
            this.EASchemaTreeView.MouseLeave += new System.EventHandler(this.EATablesTreeView_MouseLeave);
            // 
            // ThisImageList
            // 
            this.ThisImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ThisImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.ThisImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // SQLSchemaTreeView
            // 
            this.SQLSchemaTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SQLSchemaTreeView.ImageIndex = 0;
            this.SQLSchemaTreeView.ImageList = this.ThisImageList;
            this.SQLSchemaTreeView.Location = new System.Drawing.Point(3, 37);
            this.SQLSchemaTreeView.Name = "SQLSchemaTreeView";
            this.SQLSchemaTreeView.SelectedImageIndex = 0;
            this.SQLSchemaTreeView.Size = new System.Drawing.Size(449, 380);
            this.SQLSchemaTreeView.TabIndex = 13;
            this.SQLSchemaTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.SQLTablesTreeView_AfterSelect);
            this.SQLSchemaTreeView.MouseLeave += new System.EventHandler(this.SQLTablesTreeView_MouseLeave);
            // 
            // SQLScriptCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 650);
            this.Controls.Add(this.TableDefinitionsSplitContainer);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.TopPanel);
            this.Name = "SQLScriptCreator";
            this.Text = "SQL Script Creator";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SQLScriptCreator_FormClosed);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.BottomPanel.ResumeLayout(false);
            this.BottomPanel.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.TableDefinitionsSplitContainer.Panel1.ResumeLayout(false);
            this.TableDefinitionsSplitContainer.Panel1.PerformLayout();
            this.TableDefinitionsSplitContainer.Panel2.ResumeLayout(false);
            this.TableDefinitionsSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Panel BottomPanel;
        private System.Windows.Forms.SplitContainer TableDefinitionsSplitContainer;
        private System.Windows.Forms.ImageList ThisImageList;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.Button CreateScriptButton;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.TreeView EASchemaTreeView;
        private System.Windows.Forms.TextBox DatabaseTextBox;
        private System.Windows.Forms.Label DatabaseLabel;
        private System.Windows.Forms.TreeView SQLSchemaTreeView;
        private System.Windows.Forms.Label ServerLabel;
        private System.Windows.Forms.TextBox SqlServerConnectionTextBox;
        internal System.Windows.Forms.ToolStripLabel progressToolStripLabel;
        private System.Windows.Forms.Label ObjectLabel;
        private System.Windows.Forms.TextBox ObjectTextBox;
        private System.Windows.Forms.Button CompareButton;
    }
}