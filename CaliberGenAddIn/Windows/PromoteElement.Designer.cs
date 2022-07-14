namespace EAAddIn.Windows
{
    partial class PromoteElement
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
            this.btnFrom = new System.Windows.Forms.Button();
            this.btnTo = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFromGuid = new System.Windows.Forms.TextBox();
            this.txtFromName = new System.Windows.Forms.TextBox();
            this.txtFromType = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtToGuid = new System.Windows.Forms.TextBox();
            this.txtToName = new System.Windows.Forms.TextBox();
            this.txtToType = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnPromote = new System.Windows.Forms.Button();
            this.cbxReplaceInDiagrams = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbxDuplicateConnectors = new System.Windows.Forms.CheckBox();
            this.cbxAddMethods = new System.Windows.Forms.CheckBox();
            this.cbxAddAttributes = new System.Windows.Forms.CheckBox();
            this.VerifyPromotionButton = new System.Windows.Forms.Button();
            this.MessagesDataGridView = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.btnMergeTables = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MessagesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFrom
            // 
            this.btnFrom.Location = new System.Drawing.Point(4, 12);
            this.btnFrom.Name = "btnFrom";
            this.btnFrom.Size = new System.Drawing.Size(98, 23);
            this.btnFrom.TabIndex = 0;
            this.btnFrom.Text = "Promote From";
            this.btnFrom.UseVisualStyleBackColor = true;
            this.btnFrom.Click += new System.EventHandler(this.btnFrom_Click);
            // 
            // btnTo
            // 
            this.btnTo.Location = new System.Drawing.Point(4, 133);
            this.btnTo.Name = "btnTo";
            this.btnTo.Size = new System.Drawing.Size(95, 23);
            this.btnTo.TabIndex = 1;
            this.btnTo.Text = "Promote To";
            this.btnTo.UseVisualStyleBackColor = true;
            this.btnTo.Click += new System.EventHandler(this.btnTo_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtFromGuid);
            this.groupBox1.Controls.Add(this.txtFromName);
            this.groupBox1.Controls.Add(this.txtFromType);
            this.groupBox1.Location = new System.Drawing.Point(4, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(750, 75);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "From Element";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "GUID:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(525, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Type:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtFromGuid
            // 
            this.txtFromGuid.Location = new System.Drawing.Point(92, 45);
            this.txtFromGuid.Name = "txtFromGuid";
            this.txtFromGuid.ReadOnly = true;
            this.txtFromGuid.Size = new System.Drawing.Size(337, 20);
            this.txtFromGuid.TabIndex = 2;
            // 
            // txtFromName
            // 
            this.txtFromName.Location = new System.Drawing.Point(92, 16);
            this.txtFromName.Name = "txtFromName";
            this.txtFromName.ReadOnly = true;
            this.txtFromName.Size = new System.Drawing.Size(337, 20);
            this.txtFromName.TabIndex = 1;
            // 
            // txtFromType
            // 
            this.txtFromType.Location = new System.Drawing.Point(565, 16);
            this.txtFromType.Name = "txtFromType";
            this.txtFromType.ReadOnly = true;
            this.txtFromType.Size = new System.Drawing.Size(161, 20);
            this.txtFromType.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtToGuid);
            this.groupBox2.Controls.Add(this.txtToName);
            this.groupBox2.Controls.Add(this.txtToType);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(4, 162);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(750, 74);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "To Element";
            // 
            // txtToGuid
            // 
            this.txtToGuid.Location = new System.Drawing.Point(92, 42);
            this.txtToGuid.Name = "txtToGuid";
            this.txtToGuid.ReadOnly = true;
            this.txtToGuid.Size = new System.Drawing.Size(337, 20);
            this.txtToGuid.TabIndex = 5;
            // 
            // txtToName
            // 
            this.txtToName.Location = new System.Drawing.Point(92, 16);
            this.txtToName.Name = "txtToName";
            this.txtToName.ReadOnly = true;
            this.txtToName.Size = new System.Drawing.Size(337, 20);
            this.txtToName.TabIndex = 4;
            // 
            // txtToType
            // 
            this.txtToType.Location = new System.Drawing.Point(565, 16);
            this.txtToType.Name = "txtToType";
            this.txtToType.ReadOnly = true;
            this.txtToType.Size = new System.Drawing.Size(161, 20);
            this.txtToType.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(49, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "GUID:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(45, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Name:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(525, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Type:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnPromote
            // 
            this.btnPromote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPromote.Location = new System.Drawing.Point(659, 425);
            this.btnPromote.Name = "btnPromote";
            this.btnPromote.Size = new System.Drawing.Size(95, 23);
            this.btnPromote.TabIndex = 4;
            this.btnPromote.Text = "Promote";
            this.btnPromote.UseVisualStyleBackColor = true;
            this.btnPromote.Click += new System.EventHandler(this.btnPromote_Click);
            // 
            // cbxReplaceInDiagrams
            // 
            this.cbxReplaceInDiagrams.AutoSize = true;
            this.cbxReplaceInDiagrams.Checked = true;
            this.cbxReplaceInDiagrams.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxReplaceInDiagrams.Location = new System.Drawing.Point(6, 42);
            this.cbxReplaceInDiagrams.Name = "cbxReplaceInDiagrams";
            this.cbxReplaceInDiagrams.Size = new System.Drawing.Size(124, 17);
            this.cbxReplaceInDiagrams.TabIndex = 5;
            this.cbxReplaceInDiagrams.Text = "Replace in Diagrams";
            this.cbxReplaceInDiagrams.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbxDuplicateConnectors);
            this.groupBox3.Controls.Add(this.cbxAddMethods);
            this.groupBox3.Controls.Add(this.cbxAddAttributes);
            this.groupBox3.Controls.Add(this.cbxReplaceInDiagrams);
            this.groupBox3.Location = new System.Drawing.Point(4, 242);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(168, 177);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Options";
            // 
            // cbxDuplicateConnectors
            // 
            this.cbxDuplicateConnectors.AutoSize = true;
            this.cbxDuplicateConnectors.Checked = true;
            this.cbxDuplicateConnectors.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxDuplicateConnectors.Location = new System.Drawing.Point(6, 19);
            this.cbxDuplicateConnectors.Name = "cbxDuplicateConnectors";
            this.cbxDuplicateConnectors.Size = new System.Drawing.Size(128, 17);
            this.cbxDuplicateConnectors.TabIndex = 8;
            this.cbxDuplicateConnectors.Text = "Duplicate Connectors";
            this.cbxDuplicateConnectors.UseVisualStyleBackColor = true;
            // 
            // cbxAddMethods
            // 
            this.cbxAddMethods.AutoSize = true;
            this.cbxAddMethods.Location = new System.Drawing.Point(6, 88);
            this.cbxAddMethods.Name = "cbxAddMethods";
            this.cbxAddMethods.Size = new System.Drawing.Size(89, 17);
            this.cbxAddMethods.TabIndex = 7;
            this.cbxAddMethods.Text = "Add Methods";
            this.cbxAddMethods.UseVisualStyleBackColor = true;
            // 
            // cbxAddAttributes
            // 
            this.cbxAddAttributes.AutoSize = true;
            this.cbxAddAttributes.Location = new System.Drawing.Point(6, 65);
            this.cbxAddAttributes.Name = "cbxAddAttributes";
            this.cbxAddAttributes.Size = new System.Drawing.Size(92, 17);
            this.cbxAddAttributes.TabIndex = 6;
            this.cbxAddAttributes.Text = "Add Attributes";
            this.cbxAddAttributes.UseVisualStyleBackColor = true;
            // 
            // VerifyPromotionButton
            // 
            this.VerifyPromotionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.VerifyPromotionButton.Location = new System.Drawing.Point(553, 425);
            this.VerifyPromotionButton.Name = "VerifyPromotionButton";
            this.VerifyPromotionButton.Size = new System.Drawing.Size(100, 23);
            this.VerifyPromotionButton.TabIndex = 7;
            this.VerifyPromotionButton.Text = "Preview Changes";
            this.VerifyPromotionButton.UseVisualStyleBackColor = true;
            this.VerifyPromotionButton.Click += new System.EventHandler(this.VerifyPromotionButton_Click);
            // 
            // MessagesDataGridView
            // 
            this.MessagesDataGridView.AllowUserToAddRows = false;
            this.MessagesDataGridView.AllowUserToDeleteRows = false;
            this.MessagesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MessagesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MessagesDataGridView.Location = new System.Drawing.Point(189, 261);
            this.MessagesDataGridView.Name = "MessagesDataGridView";
            this.MessagesDataGridView.ReadOnly = true;
            this.MessagesDataGridView.Size = new System.Drawing.Size(565, 158);
            this.MessagesDataGridView.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(186, 242);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Promotion Messages:";
            // 
            // btnMergeTables
            // 
            this.btnMergeTables.Location = new System.Drawing.Point(4, 425);
            this.btnMergeTables.Name = "btnMergeTables";
            this.btnMergeTables.Size = new System.Drawing.Size(101, 24);
            this.btnMergeTables.TabIndex = 10;
            this.btnMergeTables.Text = "Merge Tables";
            this.btnMergeTables.UseVisualStyleBackColor = true;
            this.btnMergeTables.Click += new System.EventHandler(this.btnMergeTables_Click);
            // 
            // PromoteElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 460);
            this.Controls.Add(this.btnMergeTables);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.MessagesDataGridView);
            this.Controls.Add(this.VerifyPromotionButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnPromote);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnTo);
            this.Controls.Add(this.btnFrom);
            this.Name = "PromoteElement";
            this.Text = "Promote Element";
            this.Load += new System.EventHandler(this.PromoteElement_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MessagesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFrom;
        private System.Windows.Forms.Button btnTo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnPromote;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFromGuid;
        private System.Windows.Forms.TextBox txtFromName;
        private System.Windows.Forms.TextBox txtFromType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtToGuid;
        private System.Windows.Forms.TextBox txtToName;
        private System.Windows.Forms.TextBox txtToType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbxReplaceInDiagrams;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbxAddMethods;
        private System.Windows.Forms.CheckBox cbxAddAttributes;
        private System.Windows.Forms.CheckBox cbxDuplicateConnectors;
        private System.Windows.Forms.Button VerifyPromotionButton;
        private System.Windows.Forms.DataGridView MessagesDataGridView;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnMergeTables;
    }
}