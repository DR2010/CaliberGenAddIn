namespace EAAddIn.Windows
{
    partial class BPMNReport
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
            this.BPMNdataGridView = new System.Windows.Forms.DataGridView();
            this.btnShowBPMNUsers = new System.Windows.Forms.Button();
            this.btnDiagramReport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.BPMNdataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // BPMNdataGridView
            // 
            this.BPMNdataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BPMNdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BPMNdataGridView.Location = new System.Drawing.Point(22, 55);
            this.BPMNdataGridView.Name = "BPMNdataGridView";
            this.BPMNdataGridView.Size = new System.Drawing.Size(675, 401);
            this.BPMNdataGridView.TabIndex = 0;
            // 
            // btnShowBPMNUsers
            // 
            this.btnShowBPMNUsers.Location = new System.Drawing.Point(22, 12);
            this.btnShowBPMNUsers.Name = "btnShowBPMNUsers";
            this.btnShowBPMNUsers.Size = new System.Drawing.Size(142, 23);
            this.btnShowBPMNUsers.TabIndex = 1;
            this.btnShowBPMNUsers.Text = "Show BPMN Users";
            this.btnShowBPMNUsers.UseVisualStyleBackColor = true;
            this.btnShowBPMNUsers.Click += new System.EventHandler(this.btnShowBPMNUsers_Click);
            // 
            // btnDiagramReport
            // 
            this.btnDiagramReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDiagramReport.Location = new System.Drawing.Point(576, 12);
            this.btnDiagramReport.Name = "btnDiagramReport";
            this.btnDiagramReport.Size = new System.Drawing.Size(121, 23);
            this.btnDiagramReport.TabIndex = 2;
            this.btnDiagramReport.Text = "Diagram Report";
            this.btnDiagramReport.UseVisualStyleBackColor = true;
            this.btnDiagramReport.Click += new System.EventHandler(this.btnDiagramReport_Click);
            // 
            // BPMNReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 483);
            this.Controls.Add(this.btnDiagramReport);
            this.Controls.Add(this.btnShowBPMNUsers);
            this.Controls.Add(this.BPMNdataGridView);
            this.Name = "BPMNReport";
            this.Text = "BPMNReport";
            ((System.ComponentModel.ISupportInitialize)(this.BPMNdataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView BPMNdataGridView;
        private System.Windows.Forms.Button btnShowBPMNUsers;
        private System.Windows.Forms.Button btnDiagramReport;
    }
}