namespace EAAddIn.Windows
{
    partial class ConnectorTypesPicker
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
            this.ConnectorTypesDataGridView = new System.Windows.Forms.DataGridView();
            this.connectorTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tconnectortypesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.eAReleaseDataSet = new EAReleaseDataSet();
            this.OKbutton = new System.Windows.Forms.Button();
            this.CancelPickerButton = new System.Windows.Forms.Button();
            this.t_connectortypesTableAdapter = new EAReleaseDataSetTableAdapters.t_connectortypesTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ConnectorTypesDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tconnectortypesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eAReleaseDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // ConnectorTypesDataGridView
            // 
            this.ConnectorTypesDataGridView.AllowUserToAddRows = false;
            this.ConnectorTypesDataGridView.AllowUserToDeleteRows = false;
            this.ConnectorTypesDataGridView.AutoGenerateColumns = false;
            this.ConnectorTypesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ConnectorTypesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.connectorTypeDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn});
            this.ConnectorTypesDataGridView.DataSource = this.tconnectortypesBindingSource;
            this.ConnectorTypesDataGridView.Location = new System.Drawing.Point(13, 13);
            this.ConnectorTypesDataGridView.Name = "ConnectorTypesDataGridView";
            this.ConnectorTypesDataGridView.ReadOnly = true;
            this.ConnectorTypesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ConnectorTypesDataGridView.Size = new System.Drawing.Size(271, 488);
            this.ConnectorTypesDataGridView.TabIndex = 0;
            // 
            // connectorTypeDataGridViewTextBoxColumn
            // 
            this.connectorTypeDataGridViewTextBoxColumn.DataPropertyName = "Connector_Type";
            this.connectorTypeDataGridViewTextBoxColumn.HeaderText = "Connector_Type";
            this.connectorTypeDataGridViewTextBoxColumn.Name = "connectorTypeDataGridViewTextBoxColumn";
            this.connectorTypeDataGridViewTextBoxColumn.ReadOnly = true;
            this.connectorTypeDataGridViewTextBoxColumn.Visible = false;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.FillWeight = 200F;
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            this.descriptionDataGridViewTextBoxColumn.Width = 200;
            // 
            // tconnectortypesBindingSource
            // 
            this.tconnectortypesBindingSource.DataMember = "t_connectortypes";
            this.tconnectortypesBindingSource.DataSource = this.eAReleaseDataSet;
            // 
            // eAReleaseDataSet
            // 
            this.eAReleaseDataSet.DataSetName = "EAReleaseDataSet";
            this.eAReleaseDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // OKbutton
            // 
            this.OKbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKbutton.Location = new System.Drawing.Point(209, 507);
            this.OKbutton.Name = "OKbutton";
            this.OKbutton.Size = new System.Drawing.Size(75, 23);
            this.OKbutton.TabIndex = 1;
            this.OKbutton.Text = "OK";
            this.OKbutton.UseVisualStyleBackColor = true;
            this.OKbutton.Click += new System.EventHandler(this.OKbutton_Click);
            // 
            // CancelPickerButton
            // 
            this.CancelPickerButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelPickerButton.Location = new System.Drawing.Point(128, 507);
            this.CancelPickerButton.Name = "CancelPickerButton";
            this.CancelPickerButton.Size = new System.Drawing.Size(75, 23);
            this.CancelPickerButton.TabIndex = 2;
            this.CancelPickerButton.Text = "Cancel";
            this.CancelPickerButton.UseVisualStyleBackColor = true;
            // 
            // t_connectortypesTableAdapter
            // 
            this.t_connectortypesTableAdapter.ClearBeforeFill = true;
            // 
            // ConnectorTypesPicker
            // 
            this.AcceptButton = this.OKbutton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 534);
            this.Controls.Add(this.CancelPickerButton);
            this.Controls.Add(this.OKbutton);
            this.Controls.Add(this.ConnectorTypesDataGridView);
            this.Name = "ConnectorTypesPicker";
            this.Text = "Connector Types Picker";
            this.Load += new System.EventHandler(this.ConnectorTypesPicker_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ConnectorTypesDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tconnectortypesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eAReleaseDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ConnectorTypesDataGridView;
        private EAReleaseDataSet eAReleaseDataSet;
        private System.Windows.Forms.BindingSource tconnectortypesBindingSource;
        private global::EAAddIn.EAReleaseDataSetTableAdapters.t_connectortypesTableAdapter t_connectortypesTableAdapter;
        private System.Windows.Forms.Button OKbutton;
        private System.Windows.Forms.Button CancelPickerButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn connectorTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
    }
}