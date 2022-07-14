namespace EAAddIn.Windows
{
    partial class CsvClassImporter
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
            this.CSVFileTextBox = new System.Windows.Forms.TextBox();
            this.CSVImportDataGridView = new System.Windows.Forms.DataGridView();
            this.ImportButton = new System.Windows.Forms.Button();
            this.CSVFilePickerButton = new System.Windows.Forms.Button();
            this.SetPackageButton = new System.Windows.Forms.Button();
            this.PackageTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.openCSVFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.CreateElementsButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CSVImportDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "CSV File:";
            // 
            // CSVFileTextBox
            // 
            this.CSVFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CSVFileTextBox.Location = new System.Drawing.Point(68, 9);
            this.CSVFileTextBox.Name = "CSVFileTextBox";
            this.CSVFileTextBox.Size = new System.Drawing.Size(369, 20);
            this.CSVFileTextBox.TabIndex = 2;
            // 
            // CSVImportDataGridView
            // 
            this.CSVImportDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CSVImportDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CSVImportDataGridView.Location = new System.Drawing.Point(12, 110);
            this.CSVImportDataGridView.Name = "CSVImportDataGridView";
            this.CSVImportDataGridView.Size = new System.Drawing.Size(532, 277);
            this.CSVImportDataGridView.TabIndex = 3;
            // 
            // ImportButton
            // 
            this.ImportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ImportButton.Location = new System.Drawing.Point(472, 7);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(75, 23);
            this.ImportButton.TabIndex = 4;
            this.ImportButton.Text = "Import CSV";
            this.ImportButton.UseVisualStyleBackColor = true;
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click);
            // 
            // CSVFilePickerButton
            // 
            this.CSVFilePickerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CSVFilePickerButton.Location = new System.Drawing.Point(443, 7);
            this.CSVFilePickerButton.Name = "CSVFilePickerButton";
            this.CSVFilePickerButton.Size = new System.Drawing.Size(24, 23);
            this.CSVFilePickerButton.TabIndex = 6;
            this.CSVFilePickerButton.Text = "...";
            this.CSVFilePickerButton.UseVisualStyleBackColor = true;
            this.CSVFilePickerButton.Click += new System.EventHandler(this.CSVFilePickerButton_Click);
            // 
            // SetPackageButton
            // 
            this.SetPackageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SetPackageButton.Location = new System.Drawing.Point(293, 393);
            this.SetPackageButton.Name = "SetPackageButton";
            this.SetPackageButton.Size = new System.Drawing.Size(144, 23);
            this.SetPackageButton.TabIndex = 7;
            this.SetPackageButton.Text = "Set to Current Package";
            this.SetPackageButton.UseVisualStyleBackColor = true;
            this.SetPackageButton.Click += new System.EventHandler(this.SetPackageButton_Click);
            // 
            // PackageTextBox
            // 
            this.PackageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PackageTextBox.Location = new System.Drawing.Point(68, 395);
            this.PackageTextBox.Name = "PackageTextBox";
            this.PackageTextBox.Size = new System.Drawing.Size(219, 20);
            this.PackageTextBox.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 398);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Package:";
            // 
            // openCSVFileDialog
            // 
            this.openCSVFileDialog.DefaultExt = "csv";
            this.openCSVFileDialog.FileName = "openFileDialog1";
            this.openCSVFileDialog.Filter = "CSV Files|*.csv";
            this.openCSVFileDialog.Title = "Open CSV File";
            // 
            // CreateElementsButton
            // 
            this.CreateElementsButton.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.CreateElementsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateElementsButton.Location = new System.Drawing.Point(443, 393);
            this.CreateElementsButton.Name = "CreateElementsButton";
            this.CreateElementsButton.Size = new System.Drawing.Size(104, 23);
            this.CreateElementsButton.TabIndex = 10;
            this.CreateElementsButton.Text = "Create Elements";
            this.CreateElementsButton.UseVisualStyleBackColor = true;
            this.CreateElementsButton.Click += new System.EventHandler(this.CreateClassesButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(375, 60);
            this.label3.TabIndex = 11;
            this.label3.Text = "CSV file can have up to 3 columns, with 1 heading row. Valid headings are:\r\n\r\nNam" +
                "e, Type, Stereotype\r\n\r\nName is the only mandatory column, with the type defaulti" +
                "ng to Class.\r\n";
            // 
            // CsvClassImporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 433);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CreateElementsButton);
            this.Controls.Add(this.PackageTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SetPackageButton);
            this.Controls.Add(this.CSVFilePickerButton);
            this.Controls.Add(this.ImportButton);
            this.Controls.Add(this.CSVImportDataGridView);
            this.Controls.Add(this.CSVFileTextBox);
            this.Controls.Add(this.label1);
            this.Name = "CsvClassImporter";
            this.Text = "CSV Element Import";
            ((System.ComponentModel.ISupportInitialize)(this.CSVImportDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CSVFileTextBox;
        private System.Windows.Forms.DataGridView CSVImportDataGridView;
        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.Button CSVFilePickerButton;
        private System.Windows.Forms.Button SetPackageButton;
        private System.Windows.Forms.TextBox PackageTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openCSVFileDialog;
        private System.Windows.Forms.Button CreateElementsButton;
        private System.Windows.Forms.Label label3;
    }
}

