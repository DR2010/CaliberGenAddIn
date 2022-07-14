namespace EAAddIn.Windows
{
    partial class CR0370AddIn
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
            this.GetChangedElementsButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.fromDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.ToDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.changedElementsDataGridView = new System.Windows.Forms.DataGridView();
            this.generateProcessSpecificationButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.changedElementsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // GetChangedElementsButton
            // 
            this.GetChangedElementsButton.Location = new System.Drawing.Point(347, 6);
            this.GetChangedElementsButton.Name = "GetChangedElementsButton";
            this.GetChangedElementsButton.Size = new System.Drawing.Size(150, 23);
            this.GetChangedElementsButton.TabIndex = 0;
            this.GetChangedElementsButton.Text = "Get Changed Elements";
            this.GetChangedElementsButton.UseVisualStyleBackColor = true;
            this.GetChangedElementsButton.Click += new System.EventHandler(this.GetChangedElementsButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "From:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(193, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "To:";
            // 
            // fromDateTimePicker
            // 
            this.fromDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fromDateTimePicker.Location = new System.Drawing.Point(51, 8);
            this.fromDateTimePicker.Name = "fromDateTimePicker";
            this.fromDateTimePicker.Size = new System.Drawing.Size(101, 20);
            this.fromDateTimePicker.TabIndex = 3;
            // 
            // ToDateTimePicker
            // 
            this.ToDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ToDateTimePicker.Location = new System.Drawing.Point(222, 8);
            this.ToDateTimePicker.Name = "ToDateTimePicker";
            this.ToDateTimePicker.Size = new System.Drawing.Size(101, 20);
            this.ToDateTimePicker.TabIndex = 4;
            // 
            // changedElementsDataGridView
            // 
            this.changedElementsDataGridView.AllowDrop = true;
            this.changedElementsDataGridView.AllowUserToAddRows = false;
            this.changedElementsDataGridView.AllowUserToDeleteRows = false;
            this.changedElementsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.changedElementsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.changedElementsDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.changedElementsDataGridView.Location = new System.Drawing.Point(12, 34);
            this.changedElementsDataGridView.MultiSelect = false;
            this.changedElementsDataGridView.Name = "changedElementsDataGridView";
            this.changedElementsDataGridView.Size = new System.Drawing.Size(689, 220);
            this.changedElementsDataGridView.TabIndex = 5;
            this.changedElementsDataGridView.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.changedElementsDataGridView_RowHeaderMouseDoubleClick);
            // 
            // generateProcessSpecificationButton
            // 
            this.generateProcessSpecificationButton.Location = new System.Drawing.Point(533, 6);
            this.generateProcessSpecificationButton.Name = "generateProcessSpecificationButton";
            this.generateProcessSpecificationButton.Size = new System.Drawing.Size(168, 23);
            this.generateProcessSpecificationButton.TabIndex = 6;
            this.generateProcessSpecificationButton.Text = "Generate Spec";
            this.generateProcessSpecificationButton.UseVisualStyleBackColor = true;
            this.generateProcessSpecificationButton.Click += new System.EventHandler(this.grabImageForDiagramButton_Click);
            // 
            // CR0370AddIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 266);
            this.Controls.Add(this.generateProcessSpecificationButton);
            this.Controls.Add(this.changedElementsDataGridView);
            this.Controls.Add(this.ToDateTimePicker);
            this.Controls.Add(this.fromDateTimePicker);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GetChangedElementsButton);
            this.Name = "CR0370AddIn";
            this.Text = "CR0370 AddIn";
            this.Load += new System.EventHandler(this.CR0370AddIn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.changedElementsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GetChangedElementsButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker fromDateTimePicker;
        private System.Windows.Forms.DateTimePicker ToDateTimePicker;
        private System.Windows.Forms.DataGridView changedElementsDataGridView;
        private System.Windows.Forms.Button generateProcessSpecificationButton;
    }
}