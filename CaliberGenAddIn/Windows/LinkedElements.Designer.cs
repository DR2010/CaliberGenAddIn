namespace EAAddIn.Windows
{
    partial class LinkedElements
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
            this.txtStereotype = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnListLinked = new System.Windows.Forms.Button();
            this.dgvLinkedElements = new System.Windows.Forms.DataGridView();
            this.btnPlace = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtObjectType = new System.Windows.Forms.TextBox();
            this.txtElementName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtElementType = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLinkedElements)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtStereotype
            // 
            this.txtStereotype.Location = new System.Drawing.Point(84, 19);
            this.txtStereotype.Name = "txtStereotype";
            this.txtStereotype.Size = new System.Drawing.Size(208, 20);
            this.txtStereotype.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Stereotype:";
            // 
            // btnListLinked
            // 
            this.btnListLinked.Location = new System.Drawing.Point(298, 19);
            this.btnListLinked.Name = "btnListLinked";
            this.btnListLinked.Size = new System.Drawing.Size(75, 48);
            this.btnListLinked.TabIndex = 2;
            this.btnListLinked.Text = "List linked";
            this.btnListLinked.UseVisualStyleBackColor = true;
            this.btnListLinked.Click += new System.EventHandler(this.btnListLinked_Click);
            // 
            // dgvLinkedElements
            // 
            this.dgvLinkedElements.AllowUserToAddRows = false;
            this.dgvLinkedElements.AllowUserToOrderColumns = true;
            this.dgvLinkedElements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLinkedElements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLinkedElements.Location = new System.Drawing.Point(15, 95);
            this.dgvLinkedElements.Name = "dgvLinkedElements";
            this.dgvLinkedElements.ReadOnly = true;
            this.dgvLinkedElements.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLinkedElements.Size = new System.Drawing.Size(702, 394);
            this.dgvLinkedElements.TabIndex = 3;
            this.toolTip1.SetToolTip(this.dgvLinkedElements, "Double-click to view linked elements for a row");
            this.dgvLinkedElements.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvLinkedElements_MouseDoubleClick);
            // 
            // btnPlace
            // 
            this.btnPlace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPlace.Location = new System.Drawing.Point(15, 498);
            this.btnPlace.Name = "btnPlace";
            this.btnPlace.Size = new System.Drawing.Size(133, 32);
            this.btnPlace.TabIndex = 4;
            this.btnPlace.Text = "Place Selected Elements";
            this.btnPlace.UseVisualStyleBackColor = true;
            this.btnPlace.Click += new System.EventHandler(this.btnPlace_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Type:";
            // 
            // txtObjectType
            // 
            this.txtObjectType.Location = new System.Drawing.Point(84, 47);
            this.txtObjectType.Name = "txtObjectType";
            this.txtObjectType.Size = new System.Drawing.Size(208, 20);
            this.txtObjectType.TabIndex = 6;
            // 
            // txtElementName
            // 
            this.txtElementName.Location = new System.Drawing.Point(55, 49);
            this.txtElementName.Name = "txtElementName";
            this.txtElementName.ReadOnly = true;
            this.txtElementName.Size = new System.Drawing.Size(240, 20);
            this.txtElementName.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtElementType);
            this.groupBox1.Controls.Add(this.txtElementName);
            this.groupBox1.Location = new System.Drawing.Point(401, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(313, 75);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Element Details";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Type:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Name:";
            // 
            // txtElementType
            // 
            this.txtElementType.Location = new System.Drawing.Point(55, 23);
            this.txtElementType.Name = "txtElementType";
            this.txtElementType.ReadOnly = true;
            this.txtElementType.Size = new System.Drawing.Size(169, 20);
            this.txtElementType.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtStereotype);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtObjectType);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnListLinked);
            this.groupBox2.Location = new System.Drawing.Point(4, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(391, 77);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Criteria";
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.Location = new System.Drawing.Point(636, 498);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(78, 32);
            this.btnBack.TabIndex = 10;
            this.btnBack.Text = "< Back";
            this.toolTip1.SetToolTip(this.btnBack, "Click to view the previous element you displayed linked elements for");
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // LinkedElements
            // 
            this.AcceptButton = this.btnListLinked;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 534);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnPlace);
            this.Controls.Add(this.dgvLinkedElements);
            this.Name = "LinkedElements";
            this.Text = "Linked Elements";
            this.Load += new System.EventHandler(this.LinkedElements_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLinkedElements)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtStereotype;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnListLinked;
        private System.Windows.Forms.DataGridView dgvLinkedElements;
        private System.Windows.Forms.Button btnPlace;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtObjectType;
        private System.Windows.Forms.TextBox txtElementName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtElementType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}