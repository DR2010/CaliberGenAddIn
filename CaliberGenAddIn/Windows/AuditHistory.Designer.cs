namespace EAAddIn.Windows
{
    partial class AuditHistory
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtFromDate = new System.Windows.Forms.TextBox();
            this.txtToDate = new System.Windows.Forms.TextBox();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.cbxDeleteOnly = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSaveNotes = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtElementID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSync = new System.Windows.Forms.Button();
            this.dgvGENCabLoad = new System.Windows.Forms.DataGridView();
            this.btnLoadXML = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnNewGuid = new System.Windows.Forms.Button();
            this.txtObjectID = new System.Windows.Forms.TextBox();
            this.txtProperty = new System.Windows.Forms.TextBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.txtea_guid = new System.Windows.Forms.TextBox();
            this.xRefresh = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtGlobalAuthor = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnShowLastGlobal = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCABName = new System.Windows.Forms.TextBox();
            this.txtLoadModule = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnCleanUp = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGENCabLoad)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "From Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "To Date:";
            // 
            // txtFromDate
            // 
            this.txtFromDate.Location = new System.Drawing.Point(95, 45);
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.Size = new System.Drawing.Size(127, 20);
            this.txtFromDate.TabIndex = 4;
            // 
            // txtToDate
            // 
            this.txtToDate.Location = new System.Drawing.Point(95, 76);
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.Size = new System.Drawing.Size(127, 20);
            this.txtToDate.TabIndex = 5;
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(95, 116);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(127, 44);
            this.btnClearLog.TabIndex = 6;
            this.btnClearLog.Text = "Clear Log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // cbxDeleteOnly
            // 
            this.cbxDeleteOnly.AutoSize = true;
            this.cbxDeleteOnly.Checked = true;
            this.cbxDeleteOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxDeleteOnly.Location = new System.Drawing.Point(6, 154);
            this.cbxDeleteOnly.Name = "cbxDeleteOnly";
            this.cbxDeleteOnly.Size = new System.Drawing.Size(81, 17);
            this.cbxDeleteOnly.TabIndex = 7;
            this.cbxDeleteOnly.Text = "Delete Only";
            this.cbxDeleteOnly.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cbxDeleteOnly);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(238, 172);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Delete Log";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnSaveNotes);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtElementID);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtNotes);
            this.groupBox2.Location = new System.Drawing.Point(12, 213);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(486, 150);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Test EA SQL Update";
            // 
            // btnSaveNotes
            // 
            this.btnSaveNotes.Location = new System.Drawing.Point(353, 88);
            this.btnSaveNotes.Name = "btnSaveNotes";
            this.btnSaveNotes.Size = new System.Drawing.Size(100, 46);
            this.btnSaveNotes.TabIndex = 4;
            this.btnSaveNotes.Text = "Save Notes";
            this.btnSaveNotes.UseVisualStyleBackColor = true;
            this.btnSaveNotes.Click += new System.EventHandler(this.btnSaveNotes_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Element ID:";
            // 
            // txtElementID
            // 
            this.txtElementID.Enabled = false;
            this.txtElementID.Location = new System.Drawing.Point(83, 28);
            this.txtElementID.Name = "txtElementID";
            this.txtElementID.ReadOnly = true;
            this.txtElementID.Size = new System.Drawing.Size(216, 20);
            this.txtElementID.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Notes";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(8, 88);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(339, 44);
            this.txtNotes.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnSync);
            this.groupBox3.Controls.Add(this.dgvGENCabLoad);
            this.groupBox3.Controls.Add(this.btnLoadXML);
            this.groupBox3.Location = new System.Drawing.Point(12, 369);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(800, 171);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Load CAB dependencies";
            // 
            // btnSync
            // 
            this.btnSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSync.Location = new System.Drawing.Point(743, 95);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(42, 70);
            this.btnSync.TabIndex = 2;
            this.btnSync.Text = "Sync";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // dgvGENCabLoad
            // 
            this.dgvGENCabLoad.AllowUserToDeleteRows = false;
            this.dgvGENCabLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvGENCabLoad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGENCabLoad.Location = new System.Drawing.Point(12, 19);
            this.dgvGENCabLoad.Name = "dgvGENCabLoad";
            this.dgvGENCabLoad.Size = new System.Drawing.Size(725, 146);
            this.dgvGENCabLoad.TabIndex = 1;
            // 
            // btnLoadXML
            // 
            this.btnLoadXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadXML.Location = new System.Drawing.Point(743, 19);
            this.btnLoadXML.Name = "btnLoadXML";
            this.btnLoadXML.Size = new System.Drawing.Size(42, 70);
            this.btnLoadXML.TabIndex = 0;
            this.btnLoadXML.Text = "Load";
            this.btnLoadXML.UseVisualStyleBackColor = true;
            this.btnLoadXML.Click += new System.EventHandler(this.btnLoadXML_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // btnNewGuid
            // 
            this.btnNewGuid.Location = new System.Drawing.Point(222, 45);
            this.btnNewGuid.Name = "btnNewGuid";
            this.btnNewGuid.Size = new System.Drawing.Size(75, 23);
            this.btnNewGuid.TabIndex = 1;
            this.btnNewGuid.Text = "Save";
            this.btnNewGuid.UseVisualStyleBackColor = true;
            this.btnNewGuid.Click += new System.EventHandler(this.btnNewGuid_Click);
            // 
            // txtObjectID
            // 
            this.txtObjectID.Location = new System.Drawing.Point(22, 18);
            this.txtObjectID.Name = "txtObjectID";
            this.txtObjectID.ReadOnly = true;
            this.txtObjectID.Size = new System.Drawing.Size(100, 20);
            this.txtObjectID.TabIndex = 2;
            // 
            // txtProperty
            // 
            this.txtProperty.Location = new System.Drawing.Point(22, 44);
            this.txtProperty.Name = "txtProperty";
            this.txtProperty.Size = new System.Drawing.Size(178, 20);
            this.txtProperty.TabIndex = 3;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(22, 72);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(178, 20);
            this.txtValue.TabIndex = 4;
            // 
            // txtea_guid
            // 
            this.txtea_guid.Location = new System.Drawing.Point(22, 98);
            this.txtea_guid.Name = "txtea_guid";
            this.txtea_guid.ReadOnly = true;
            this.txtea_guid.Size = new System.Drawing.Size(266, 20);
            this.txtea_guid.TabIndex = 5;
            // 
            // xRefresh
            // 
            this.xRefresh.Location = new System.Drawing.Point(222, 16);
            this.xRefresh.Name = "xRefresh";
            this.xRefresh.Size = new System.Drawing.Size(75, 23);
            this.xRefresh.TabIndex = 6;
            this.xRefresh.Text = "Refresh";
            this.xRefresh.UseVisualStyleBackColor = true;
            this.xRefresh.Click += new System.EventHandler(this.xRefresh_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.xRefresh);
            this.groupBox4.Controls.Add(this.txtea_guid);
            this.groupBox4.Controls.Add(this.txtValue);
            this.groupBox4.Controls.Add(this.txtProperty);
            this.groupBox4.Controls.Add(this.txtObjectID);
            this.groupBox4.Controls.Add(this.btnNewGuid);
            this.groupBox4.Location = new System.Drawing.Point(509, 213);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(303, 150);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "GUID";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.txtGlobalAuthor);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.btnShowLastGlobal);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.txtCABName);
            this.groupBox5.Controls.Add(this.txtLoadModule);
            this.groupBox5.Location = new System.Drawing.Point(509, 21);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(303, 163);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Last Global";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 91);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Author: ";
            // 
            // txtGlobalAuthor
            // 
            this.txtGlobalAuthor.Location = new System.Drawing.Point(100, 84);
            this.txtGlobalAuthor.Name = "txtGlobalAuthor";
            this.txtGlobalAuthor.Size = new System.Drawing.Size(180, 20);
            this.txtGlobalAuthor.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Name: ";
            // 
            // btnShowLastGlobal
            // 
            this.btnShowLastGlobal.Location = new System.Drawing.Point(100, 113);
            this.btnShowLastGlobal.Name = "btnShowLastGlobal";
            this.btnShowLastGlobal.Size = new System.Drawing.Size(140, 26);
            this.btnShowLastGlobal.TabIndex = 3;
            this.btnShowLastGlobal.Text = "Show Last Global";
            this.btnShowLastGlobal.UseVisualStyleBackColor = true;
            this.btnShowLastGlobal.Click += new System.EventHandler(this.btnShowLastGlobal_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Load Module:";
            // 
            // txtCABName
            // 
            this.txtCABName.Location = new System.Drawing.Point(100, 55);
            this.txtCABName.Name = "txtCABName";
            this.txtCABName.Size = new System.Drawing.Size(180, 20);
            this.txtCABName.TabIndex = 1;
            // 
            // txtLoadModule
            // 
            this.txtLoadModule.Location = new System.Drawing.Point(100, 27);
            this.txtLoadModule.Name = "txtLoadModule";
            this.txtLoadModule.Size = new System.Drawing.Size(100, 20);
            this.txtLoadModule.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnCleanUp);
            this.groupBox6.Location = new System.Drawing.Point(256, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(247, 172);
            this.groupBox6.TabIndex = 13;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Clean-up Snapshot";
            // 
            // btnCleanUp
            // 
            this.btnCleanUp.Location = new System.Drawing.Point(6, 104);
            this.btnCleanUp.Name = "btnCleanUp";
            this.btnCleanUp.Size = new System.Drawing.Size(132, 44);
            this.btnCleanUp.TabIndex = 0;
            this.btnCleanUp.Text = "Clean-up";
            this.btnCleanUp.UseVisualStyleBackColor = true;
            this.btnCleanUp.Click += new System.EventHandler(this.btnCleanUp_Click);
            // 
            // AuditHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 554);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.txtToDate);
            this.Controls.Add(this.txtFromDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "AuditHistory";
            this.Text = "AuditHistory";
            this.Load += new System.EventHandler(this.AuditHistory_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGENCabLoad)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFromDate;
        private System.Windows.Forms.TextBox txtToDate;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.CheckBox cbxDeleteOnly;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtElementID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnSaveNotes;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnLoadXML;
        private System.Windows.Forms.DataGridView dgvGENCabLoad;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Button btnNewGuid;
        private System.Windows.Forms.TextBox txtObjectID;
        private System.Windows.Forms.TextBox txtProperty;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.TextBox txtea_guid;
        private System.Windows.Forms.Button xRefresh;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCABName;
        private System.Windows.Forms.TextBox txtLoadModule;
        private System.Windows.Forms.Button btnShowLastGlobal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtGlobalAuthor;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnCleanUp;
    }
}