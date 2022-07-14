using EAAddIn.Applications.Caliber.View;


namespace EAAddIn.Windows
{
    partial class GetNewGenServiceNumber
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnGenNumber = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLoadModuleName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.EditReleasesButton = new System.Windows.Forms.Button();
            this.cbxRelease = new System.Windows.Forms.ComboBox();
            this.releaseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.eACaliberCoolgenDataSet1 = new EACaliberCoolgenDataSet();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.lblRelease = new System.Windows.Forms.Label();
            this.lblLoggedUser = new System.Windows.Forms.Label();
            this.txtCABDescription = new System.Windows.Forms.TextBox();
            this.txtLoggedUser = new System.Windows.Forms.TextBox();
            this.releaseTableAdapter = new Applications.Caliber.View.EACaliberCoolgenDataSetTableAdapters.ReleaseTableAdapter();
            this.tableAdapterManager = new Applications.Caliber.View.EACaliberCoolgenDataSetTableAdapters.TableAdapterManager();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.releaseBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eACaliberCoolgenDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.Location = new System.Drawing.Point(90, 84);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(259, 20);
            this.txtName.TabIndex = 0;
            // 
            // btnGenNumber
            // 
            this.btnGenNumber.Location = new System.Drawing.Point(90, 136);
            this.btnGenNumber.Name = "btnGenNumber";
            this.btnGenNumber.Size = new System.Drawing.Size(212, 38);
            this.btnGenNumber.TabIndex = 1;
            this.btnGenNumber.Text = "Generate Number";
            this.btnGenNumber.UseVisualStyleBackColor = true;
            this.btnGenNumber.Click += new System.EventHandler(this.btnGenNumber_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name:";
            // 
            // txtLoadModuleName
            // 
            this.txtLoadModuleName.Location = new System.Drawing.Point(90, 192);
            this.txtLoadModuleName.Name = "txtLoadModuleName";
            this.txtLoadModuleName.ReadOnly = true;
            this.txtLoadModuleName.Size = new System.Drawing.Size(104, 20);
            this.txtLoadModuleName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 195);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Load Module:";
            // 
            // txtType
            // 
            this.txtType.Enabled = false;
            this.txtType.Location = new System.Drawing.Point(90, 32);
            this.txtType.Name = "txtType";
            this.txtType.ReadOnly = true;
            this.txtType.Size = new System.Drawing.Size(172, 20);
            this.txtType.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Type:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(355, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "(27 chars)";
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(90, 218);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.ReadOnly = true;
            this.txtFullName.Size = new System.Drawing.Size(319, 20);
            this.txtFullName.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 221);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Full Name:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(415, 218);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "(32 chars)";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(586, 282);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(82, 26);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.EditReleasesButton);
            this.groupBox1.Controls.Add(this.cbxRelease);
            this.groupBox1.Controls.Add(this.txtPrefix);
            this.groupBox1.Controls.Add(this.lblRelease);
            this.groupBox1.Controls.Add(this.lblLoggedUser);
            this.groupBox1.Controls.Add(this.txtCABDescription);
            this.groupBox1.Controls.Add(this.txtLoggedUser);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtFullName);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnGenNumber);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtType);
            this.groupBox1.Controls.Add(this.txtLoadModuleName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(653, 261);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Service Number - New";
            // 
            // EditReleasesButton
            // 
            this.EditReleasesButton.Location = new System.Drawing.Point(355, 109);
            this.EditReleasesButton.Name = "EditReleasesButton";
            this.EditReleasesButton.Size = new System.Drawing.Size(27, 23);
            this.EditReleasesButton.TabIndex = 19;
            this.EditReleasesButton.Text = "...";
            this.EditReleasesButton.UseVisualStyleBackColor = true;
            this.EditReleasesButton.Click += new System.EventHandler(this.EditReleasesButton_Click);
            // 
            // cbxRelease
            // 
            this.cbxRelease.DataSource = this.releaseBindingSource;
            this.cbxRelease.DisplayMember = "description";
            this.cbxRelease.FormattingEnabled = true;
            this.cbxRelease.Location = new System.Drawing.Point(90, 109);
            this.cbxRelease.Name = "cbxRelease";
            this.cbxRelease.Size = new System.Drawing.Size(259, 21);
            this.cbxRelease.TabIndex = 18;
            this.cbxRelease.ValueMember = "code";
            // 
            // releaseBindingSource
            // 
            this.releaseBindingSource.DataMember = "Release";
            this.releaseBindingSource.DataSource = this.eACaliberCoolgenDataSet1;
            // 
            // eACaliberCoolgenDataSet1
            // 
            this.eACaliberCoolgenDataSet1.DataSetName = "EACaliberCoolgenDataSet";
            this.eACaliberCoolgenDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(268, 32);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.ReadOnly = true;
            this.txtPrefix.Size = new System.Drawing.Size(81, 20);
            this.txtPrefix.TabIndex = 17;
            // 
            // lblRelease
            // 
            this.lblRelease.AutoSize = true;
            this.lblRelease.Location = new System.Drawing.Point(35, 113);
            this.lblRelease.Name = "lblRelease";
            this.lblRelease.Size = new System.Drawing.Size(49, 13);
            this.lblRelease.TabIndex = 16;
            this.lblRelease.Text = "Release:";
            // 
            // lblLoggedUser
            // 
            this.lblLoggedUser.AutoSize = true;
            this.lblLoggedUser.Location = new System.Drawing.Point(25, 61);
            this.lblLoggedUser.Name = "lblLoggedUser";
            this.lblLoggedUser.Size = new System.Drawing.Size(59, 13);
            this.lblLoggedUser.TabIndex = 6;
            this.lblLoggedUser.Text = "Developer:";
            // 
            // txtCABDescription
            // 
            this.txtCABDescription.Enabled = false;
            this.txtCABDescription.Location = new System.Drawing.Point(415, 32);
            this.txtCABDescription.Multiline = true;
            this.txtCABDescription.Name = "txtCABDescription";
            this.txtCABDescription.ReadOnly = true;
            this.txtCABDescription.Size = new System.Drawing.Size(221, 94);
            this.txtCABDescription.TabIndex = 14;
            // 
            // txtLoggedUser
            // 
            this.txtLoggedUser.Location = new System.Drawing.Point(90, 58);
            this.txtLoggedUser.Name = "txtLoggedUser";
            this.txtLoggedUser.ReadOnly = true;
            this.txtLoggedUser.Size = new System.Drawing.Size(100, 20);
            this.txtLoggedUser.TabIndex = 5;
            // 
            // releaseTableAdapter
            // 
            this.releaseTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.ReleaseTableAdapter = this.releaseTableAdapter;
            this.tableAdapterManager.UpdateOrder = Applications.Caliber.View.EACaliberCoolgenDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 284);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 13;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // GetNewGenServiceNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 320);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Name = "GetNewGenServiceNumber";
            this.Text = "AllFusion Gen Service Number";
            this.Load += new System.EventHandler(this.GetNewGenServiceNumber_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.releaseBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eACaliberCoolgenDataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnGenNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLoadModuleName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCABDescription;
        private System.Windows.Forms.Label lblLoggedUser;
        private System.Windows.Forms.TextBox txtLoggedUser;
        private System.Windows.Forms.Label lblRelease;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.ComboBox cbxRelease;
        private System.Windows.Forms.Button EditReleasesButton;
        private EACaliberCoolgenDataSet eACaliberCoolgenDataSet1;
        private System.Windows.Forms.BindingSource releaseBindingSource;
        private Applications.Caliber.View.EACaliberCoolgenDataSetTableAdapters.ReleaseTableAdapter releaseTableAdapter;
        private Applications.Caliber.View.EACaliberCoolgenDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.Button btnRefresh;
    }
}