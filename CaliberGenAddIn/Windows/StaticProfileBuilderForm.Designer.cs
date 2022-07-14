namespace EAAddIn.Windows
{
    partial class StaticProfileBuilderForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxStaticProfileDiagram = new System.Windows.Forms.TextBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonBuildXML = new System.Windows.Forms.Button();
            this.webBrowserStaticProfileXML = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Profile Diagram:";
            // 
            // textBoxStaticProfileDiagram
            // 
            this.textBoxStaticProfileDiagram.AcceptsReturn = true;
            this.textBoxStaticProfileDiagram.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxStaticProfileDiagram.Location = new System.Drawing.Point(103, 9);
            this.textBoxStaticProfileDiagram.Name = "textBoxStaticProfileDiagram";
            this.textBoxStaticProfileDiagram.Size = new System.Drawing.Size(352, 20);
            this.textBoxStaticProfileDiagram.TabIndex = 1;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefresh.Location = new System.Drawing.Point(461, 7);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(75, 23);
            this.buttonRefresh.TabIndex = 2;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            // 
            // buttonBuildXML
            // 
            this.buttonBuildXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBuildXML.Location = new System.Drawing.Point(461, 363);
            this.buttonBuildXML.Name = "buttonBuildXML";
            this.buttonBuildXML.Size = new System.Drawing.Size(75, 23);
            this.buttonBuildXML.TabIndex = 3;
            this.buttonBuildXML.Text = "Build XML";
            this.buttonBuildXML.UseVisualStyleBackColor = true;
            this.buttonBuildXML.Click += new System.EventHandler(this.buttonBuildXML_Click);
            // 
            // webBrowserStaticProfileXML
            // 
            this.webBrowserStaticProfileXML.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowserStaticProfileXML.Location = new System.Drawing.Point(12, 36);
            this.webBrowserStaticProfileXML.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserStaticProfileXML.Name = "webBrowserStaticProfileXML";
            this.webBrowserStaticProfileXML.Size = new System.Drawing.Size(524, 321);
            this.webBrowserStaticProfileXML.TabIndex = 5;
            // 
            // StaticProfileBuilderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 398);
            this.Controls.Add(this.webBrowserStaticProfileXML);
            this.Controls.Add(this.buttonBuildXML);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.textBoxStaticProfileDiagram);
            this.Controls.Add(this.label1);
            this.Name = "StaticProfileBuilderForm";
            this.Text = "Static Profile Builder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxStaticProfileDiagram;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonBuildXML;
        private System.Windows.Forms.WebBrowser webBrowserStaticProfileXML;
    }
}