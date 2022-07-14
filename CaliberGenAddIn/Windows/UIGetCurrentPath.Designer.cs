namespace EAAddIn.Windows
{
    partial class UIGetCurrentPath
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
            this.txtGetCurrentPath = new System.Windows.Forms.TextBox();
            this.btnGetCurrent = new System.Windows.Forms.Button();
            this.btnLocate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtGetCurrentPath
            // 
            this.txtGetCurrentPath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGetCurrentPath.Location = new System.Drawing.Point(12, 12);
            this.txtGetCurrentPath.Multiline = true;
            this.txtGetCurrentPath.Name = "txtGetCurrentPath";
            this.txtGetCurrentPath.Size = new System.Drawing.Size(480, 117);
            this.txtGetCurrentPath.TabIndex = 0;
            // 
            // btnGetCurrent
            // 
            this.btnGetCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGetCurrent.Location = new System.Drawing.Point(12, 135);
            this.btnGetCurrent.Name = "btnGetCurrent";
            this.btnGetCurrent.Size = new System.Drawing.Size(113, 23);
            this.btnGetCurrent.TabIndex = 1;
            this.btnGetCurrent.Text = "Get Current Path";
            this.btnGetCurrent.UseVisualStyleBackColor = true;
            this.btnGetCurrent.Click += new System.EventHandler(this.btnGetCurrent_Click);
            // 
            // btnLocate
            // 
            this.btnLocate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLocate.Location = new System.Drawing.Point(406, 135);
            this.btnLocate.Name = "btnLocate";
            this.btnLocate.Size = new System.Drawing.Size(86, 23);
            this.btnLocate.TabIndex = 2;
            this.btnLocate.Text = "Find";
            this.btnLocate.UseVisualStyleBackColor = true;
            this.btnLocate.Click += new System.EventHandler(this.btnLocate_Click);
            // 
            // UIGetCurrentPath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 170);
            this.Controls.Add(this.btnLocate);
            this.Controls.Add(this.btnGetCurrent);
            this.Controls.Add(this.txtGetCurrentPath);
            this.Name = "UIGetCurrentPath";
            this.Text = "Find by Path";
            this.Load += new System.EventHandler(this.GetCurrentPath_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtGetCurrentPath;
        private System.Windows.Forms.Button btnGetCurrent;
        private System.Windows.Forms.Button btnLocate;
    }
}