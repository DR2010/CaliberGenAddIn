namespace EAAddIn.Windows
{
    partial class UIDropImpact
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
            this.dgvPlacedInDiagrams = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvLinkedToElements = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtCaliberID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCaliberTagID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGENLoadModule = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtElementID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtElementName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtElementType = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtElementStereotype = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlacedInDiagrams)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLinkedToElements)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvPlacedInDiagrams
            // 
            this.dgvPlacedInDiagrams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlacedInDiagrams.Location = new System.Drawing.Point(6, 18);
            this.dgvPlacedInDiagrams.Name = "dgvPlacedInDiagrams";
            this.dgvPlacedInDiagrams.Size = new System.Drawing.Size(309, 207);
            this.dgvPlacedInDiagrams.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtElementStereotype);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtElementType);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtElementName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtElementID);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(655, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Element";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvPlacedInDiagrams);
            this.groupBox2.Location = new System.Drawing.Point(12, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(321, 231);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Placed in Diagrams";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvLinkedToElements);
            this.groupBox3.Location = new System.Drawing.Point(339, 118);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(328, 231);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Linked to Elements";
            // 
            // dgvLinkedToElements
            // 
            this.dgvLinkedToElements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLinkedToElements.Location = new System.Drawing.Point(6, 19);
            this.dgvLinkedToElements.Name = "dgvLinkedToElements";
            this.dgvLinkedToElements.Size = new System.Drawing.Size(316, 206);
            this.dgvLinkedToElements.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.txtCaliberTagID);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.txtCaliberID);
            this.groupBox4.Location = new System.Drawing.Point(12, 355);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(321, 92);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Caliber Connection";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.txtGENLoadModule);
            this.groupBox5.Location = new System.Drawing.Point(339, 355);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(321, 92);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "GEN Connection";
            // 
            // txtCaliberID
            // 
            this.txtCaliberID.Enabled = false;
            this.txtCaliberID.Location = new System.Drawing.Point(81, 29);
            this.txtCaliberID.Name = "txtCaliberID";
            this.txtCaliberID.ReadOnly = true;
            this.txtCaliberID.Size = new System.Drawing.Size(100, 20);
            this.txtCaliberID.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Caliber ID:";
            // 
            // txtCaliberTagID
            // 
            this.txtCaliberTagID.Enabled = false;
            this.txtCaliberTagID.Location = new System.Drawing.Point(81, 59);
            this.txtCaliberTagID.Name = "txtCaliberTagID";
            this.txtCaliberTagID.ReadOnly = true;
            this.txtCaliberTagID.Size = new System.Drawing.Size(100, 20);
            this.txtCaliberTagID.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tag ID:";
            // 
            // txtGENLoadModule
            // 
            this.txtGENLoadModule.Enabled = false;
            this.txtGENLoadModule.Location = new System.Drawing.Point(93, 29);
            this.txtGENLoadModule.Name = "txtGENLoadModule";
            this.txtGENLoadModule.ReadOnly = true;
            this.txtGENLoadModule.Size = new System.Drawing.Size(100, 20);
            this.txtGENLoadModule.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Load Module:";
            // 
            // txtElementID
            // 
            this.txtElementID.Enabled = false;
            this.txtElementID.Location = new System.Drawing.Point(81, 19);
            this.txtElementID.Name = "txtElementID";
            this.txtElementID.ReadOnly = true;
            this.txtElementID.Size = new System.Drawing.Size(100, 20);
            this.txtElementID.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Element ID:";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(12, 460);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(141, 23);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Go Ahead and Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(563, 460);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtElementName
            // 
            this.txtElementName.Enabled = false;
            this.txtElementName.Location = new System.Drawing.Point(81, 42);
            this.txtElementName.Name = "txtElementName";
            this.txtElementName.ReadOnly = true;
            this.txtElementName.Size = new System.Drawing.Size(280, 20);
            this.txtElementName.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Name:";
            // 
            // txtElementType
            // 
            this.txtElementType.Enabled = false;
            this.txtElementType.Location = new System.Drawing.Point(81, 65);
            this.txtElementType.Name = "txtElementType";
            this.txtElementType.ReadOnly = true;
            this.txtElementType.Size = new System.Drawing.Size(188, 20);
            this.txtElementType.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Type:";
            // 
            // txtElementStereotype
            // 
            this.txtElementStereotype.Enabled = false;
            this.txtElementStereotype.Location = new System.Drawing.Point(450, 19);
            this.txtElementStereotype.Name = "txtElementStereotype";
            this.txtElementStereotype.ReadOnly = true;
            this.txtElementStereotype.Size = new System.Drawing.Size(188, 20);
            this.txtElementStereotype.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(381, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Stereotype:";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(283, 460);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(97, 23);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // UIDropImpact
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 495);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "UIDropImpact";
            this.Text = "Element Drop Impact";
            this.Load += new System.EventHandler(this.UIDropImpact_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlacedInDiagrams)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLinkedToElements)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPlacedInDiagrams;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvLinkedToElements;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCaliberTagID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCaliberID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtGENLoadModule;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtElementID;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtElementName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtElementStereotype;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtElementType;
        private System.Windows.Forms.Button btnRefresh;
    }
}