namespace EAAddIn.Windows
{
    partial class ProcessDocumentation
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
            this.btnCopyTemplate = new System.Windows.Forms.Button();
            this.tbProcessDocName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbTemplateList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCopyTemplate
            // 
            this.btnCopyTemplate.Location = new System.Drawing.Point(130, 90);
            this.btnCopyTemplate.Name = "btnCopyTemplate";
            this.btnCopyTemplate.Size = new System.Drawing.Size(136, 23);
            this.btnCopyTemplate.TabIndex = 0;
            this.btnCopyTemplate.Text = "Copy template";
            this.btnCopyTemplate.UseVisualStyleBackColor = true;
            this.btnCopyTemplate.Click += new System.EventHandler(this.btnCopyTemplate_Click);
            // 
            // tbProcessDocName
            // 
            this.tbProcessDocName.Location = new System.Drawing.Point(105, 64);
            this.tbProcessDocName.Name = "tbProcessDocName";
            this.tbProcessDocName.Size = new System.Drawing.Size(265, 20);
            this.tbProcessDocName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Template Name";
            // 
            // cbTemplateList
            // 
            this.cbTemplateList.FormattingEnabled = true;
            this.cbTemplateList.Location = new System.Drawing.Point(105, 27);
            this.cbTemplateList.Name = "cbTemplateList";
            this.cbTemplateList.Size = new System.Drawing.Size(265, 21);
            this.cbTemplateList.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Template Type";
            // 
            // ProcessDocumentation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 133);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbTemplateList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbProcessDocName);
            this.Controls.Add(this.btnCopyTemplate);
            this.Name = "ProcessDocumentation";
            this.Text = "ProcessDocumentation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCopyTemplate;
        private System.Windows.Forms.TextBox tbProcessDocName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbTemplateList;
        private System.Windows.Forms.Label label2;
    }
}