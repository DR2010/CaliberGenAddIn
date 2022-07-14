namespace EAAddIn.Windows
{
    partial class DatabaseReleaseManagerFormUsingControl
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
            this.databaseReleaseManagerControl1 = new Windows.Controls.DatabaseReleaseManagerControl();
            this.SuspendLayout();
            // 
            // databaseReleaseManagerControl1
            // 
            this.databaseReleaseManagerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.databaseReleaseManagerControl1.Location = new System.Drawing.Point(0, 0);
            this.databaseReleaseManagerControl1.Name = "databaseReleaseManagerControl1";
            this.databaseReleaseManagerControl1.Size = new System.Drawing.Size(1113, 400);
            this.databaseReleaseManagerControl1.TabIndex = 0;
            // 
            // DatabaseReleaseManagerForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 400);
            this.Controls.Add(this.databaseReleaseManagerControl1);
            this.Name = "DatabaseReleaseManagerForm2";
            this.Text = "Database Release Manager Form";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.DatabaseReleaseManagerControl databaseReleaseManagerControl1;
    }
}