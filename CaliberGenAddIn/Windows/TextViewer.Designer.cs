namespace EAAddIn.Windows
{
    partial class TextViewer
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
            this.ViewerRichTextBox = new System.Windows.Forms.RichTextBox();
            this.SaveAsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ViewerRichTextBox
            // 
            this.ViewerRichTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ViewerRichTextBox.Location = new System.Drawing.Point(12, 12);
            this.ViewerRichTextBox.Name = "ViewerRichTextBox";
            this.ViewerRichTextBox.Size = new System.Drawing.Size(609, 353);
            this.ViewerRichTextBox.TabIndex = 0;
            this.ViewerRichTextBox.Text = "";
            // 
            // SaveAsButton
            // 
            this.SaveAsButton.Location = new System.Drawing.Point(546, 371);
            this.SaveAsButton.Name = "SaveAsButton";
            this.SaveAsButton.Size = new System.Drawing.Size(75, 23);
            this.SaveAsButton.TabIndex = 1;
            this.SaveAsButton.Text = "Save As...";
            this.SaveAsButton.UseVisualStyleBackColor = true;
            this.SaveAsButton.Click += new System.EventHandler(this.SaveAsButton_Click);
            // 
            // TextViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 404);
            this.Controls.Add(this.SaveAsButton);
            this.Controls.Add(this.ViewerRichTextBox);
            this.Name = "TextViewer";
            this.Text = "TextViewer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SaveAsButton;
        internal System.Windows.Forms.RichTextBox ViewerRichTextBox;
    }
}