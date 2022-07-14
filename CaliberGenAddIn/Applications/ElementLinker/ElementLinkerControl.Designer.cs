namespace EAAddIn.Applications.ElementLinker
{
    partial class ElementLinkerControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonSelectCurrentAsFromElement = new System.Windows.Forms.Button();
            this.textBoxFromElementName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSelectCurrentAsToElement = new System.Windows.Forms.Button();
            this.textBoxToElementName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxLinkType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonCreateLink = new System.Windows.Forms.Button();
            this.radioButtonAttributes = new System.Windows.Forms.RadioButton();
            this.radioButtonMethods = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridViewFeatures = new System.Windows.Forms.DataGridView();
            this.buttonSwapFromAndTo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFeatures)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSelectCurrentAsFromElement
            // 
            this.buttonSelectCurrentAsFromElement.Location = new System.Drawing.Point(280, 13);
            this.buttonSelectCurrentAsFromElement.Name = "buttonSelectCurrentAsFromElement";
            this.buttonSelectCurrentAsFromElement.Size = new System.Drawing.Size(28, 23);
            this.buttonSelectCurrentAsFromElement.TabIndex = 7;
            this.buttonSelectCurrentAsFromElement.Text = "...";
            this.buttonSelectCurrentAsFromElement.UseVisualStyleBackColor = true;
            this.buttonSelectCurrentAsFromElement.Click += new System.EventHandler(this.buttonSelectCurrentAsFromElement_Click);
            // 
            // textBoxFromElementName
            // 
            this.textBoxFromElementName.Location = new System.Drawing.Point(56, 15);
            this.textBoxFromElementName.Name = "textBoxFromElementName";
            this.textBoxFromElementName.ReadOnly = true;
            this.textBoxFromElementName.Size = new System.Drawing.Size(220, 20);
            this.textBoxFromElementName.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "From:";
            // 
            // buttonSelectCurrentAsToElement
            // 
            this.buttonSelectCurrentAsToElement.Location = new System.Drawing.Point(282, 68);
            this.buttonSelectCurrentAsToElement.Name = "buttonSelectCurrentAsToElement";
            this.buttonSelectCurrentAsToElement.Size = new System.Drawing.Size(28, 23);
            this.buttonSelectCurrentAsToElement.TabIndex = 10;
            this.buttonSelectCurrentAsToElement.Text = "...";
            this.buttonSelectCurrentAsToElement.UseVisualStyleBackColor = true;
            this.buttonSelectCurrentAsToElement.Click += new System.EventHandler(this.buttonSelectCurrentAsToElement_Click);
            // 
            // textBoxToElementName
            // 
            this.textBoxToElementName.Location = new System.Drawing.Point(56, 70);
            this.textBoxToElementName.Name = "textBoxToElementName";
            this.textBoxToElementName.ReadOnly = true;
            this.textBoxToElementName.Size = new System.Drawing.Size(220, 20);
            this.textBoxToElementName.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "To:";
            // 
            // comboBoxLinkType
            // 
            this.comboBoxLinkType.FormattingEnabled = true;
            this.comboBoxLinkType.Items.AddRange(new object[] {
            "Realisation"});
            this.comboBoxLinkType.Location = new System.Drawing.Point(25, 367);
            this.comboBoxLinkType.Name = "comboBoxLinkType";
            this.comboBoxLinkType.Size = new System.Drawing.Size(189, 21);
            this.comboBoxLinkType.TabIndex = 19;
            this.comboBoxLinkType.Text = "Realisation";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 351);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Link Type:";
            // 
            // buttonCreateLink
            // 
            this.buttonCreateLink.Location = new System.Drawing.Point(220, 364);
            this.buttonCreateLink.Name = "buttonCreateLink";
            this.buttonCreateLink.Size = new System.Drawing.Size(88, 26);
            this.buttonCreateLink.TabIndex = 17;
            this.buttonCreateLink.Text = "Link!";
            this.buttonCreateLink.UseVisualStyleBackColor = true;
            this.buttonCreateLink.Click += new System.EventHandler(this.buttonCreateLink_Click);
            // 
            // radioButtonAttributes
            // 
            this.radioButtonAttributes.AutoSize = true;
            this.radioButtonAttributes.Location = new System.Drawing.Point(152, 115);
            this.radioButtonAttributes.Name = "radioButtonAttributes";
            this.radioButtonAttributes.Size = new System.Drawing.Size(69, 17);
            this.radioButtonAttributes.TabIndex = 16;
            this.radioButtonAttributes.Text = "Attributes";
            this.radioButtonAttributes.UseVisualStyleBackColor = true;
            this.radioButtonAttributes.CheckedChanged += new System.EventHandler(this.radioButtonAttributes_CheckedChanged);
            // 
            // radioButtonMethods
            // 
            this.radioButtonMethods.AutoSize = true;
            this.radioButtonMethods.Checked = true;
            this.radioButtonMethods.Location = new System.Drawing.Point(80, 115);
            this.radioButtonMethods.Name = "radioButtonMethods";
            this.radioButtonMethods.Size = new System.Drawing.Size(66, 17);
            this.radioButtonMethods.TabIndex = 15;
            this.radioButtonMethods.TabStop = true;
            this.radioButtonMethods.Text = "Methods";
            this.radioButtonMethods.UseVisualStyleBackColor = true;
            this.radioButtonMethods.CheckedChanged += new System.EventHandler(this.radioButtonMethods_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Features:";
            // 
            // dataGridViewFeatures
            // 
            this.dataGridViewFeatures.AllowUserToAddRows = false;
            this.dataGridViewFeatures.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFeatures.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewFeatures.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewFeatures.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewFeatures.Location = new System.Drawing.Point(25, 134);
            this.dataGridViewFeatures.Name = "dataGridViewFeatures";
            this.dataGridViewFeatures.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFeatures.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewFeatures.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFeatures.Size = new System.Drawing.Size(283, 204);
            this.dataGridViewFeatures.TabIndex = 13;
            this.dataGridViewFeatures.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewFeatures_RowHeaderMouseDoubleClick);
            // 
            // buttonSwapFromAndTo
            // 
            this.buttonSwapFromAndTo.Location = new System.Drawing.Point(131, 41);
            this.buttonSwapFromAndTo.Name = "buttonSwapFromAndTo";
            this.buttonSwapFromAndTo.Size = new System.Drawing.Size(56, 23);
            this.buttonSwapFromAndTo.TabIndex = 20;
            this.buttonSwapFromAndTo.Text = "Swap";
            this.buttonSwapFromAndTo.UseVisualStyleBackColor = true;
            this.buttonSwapFromAndTo.Visible = false;
            this.buttonSwapFromAndTo.Click += new System.EventHandler(this.buttonSwapFromAndTo_Click);
            // 
            // ElementLinkerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSwapFromAndTo);
            this.Controls.Add(this.comboBoxLinkType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonCreateLink);
            this.Controls.Add(this.radioButtonAttributes);
            this.Controls.Add(this.radioButtonMethods);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridViewFeatures);
            this.Controls.Add(this.buttonSelectCurrentAsToElement);
            this.Controls.Add(this.textBoxToElementName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonSelectCurrentAsFromElement);
            this.Controls.Add(this.textBoxFromElementName);
            this.Controls.Add(this.label1);
            this.Name = "ElementLinkerControl";
            this.Size = new System.Drawing.Size(331, 408);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFeatures)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSelectCurrentAsFromElement;
        private System.Windows.Forms.TextBox textBoxFromElementName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSelectCurrentAsToElement;
        private System.Windows.Forms.TextBox textBoxToElementName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxLinkType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonCreateLink;
        private System.Windows.Forms.RadioButton radioButtonAttributes;
        private System.Windows.Forms.RadioButton radioButtonMethods;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridViewFeatures;
        private System.Windows.Forms.Button buttonSwapFromAndTo;
    }
}
