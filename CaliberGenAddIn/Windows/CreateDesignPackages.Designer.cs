namespace EAAddIn.Windows
{
    partial class CreateDesignPackages
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
            this.clonePackageButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ProjectIDTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ComponentsComboBox = new System.Windows.Forms.ComboBox();
            this.applyDesignPlanButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // clonePackageButton
            // 
            this.clonePackageButton.Location = new System.Drawing.Point(350, 39);
            this.clonePackageButton.Name = "clonePackageButton";
            this.clonePackageButton.Size = new System.Drawing.Size(106, 23);
            this.clonePackageButton.TabIndex = 0;
            this.clonePackageButton.Text = "Create";
            this.clonePackageButton.UseVisualStyleBackColor = true;
            this.clonePackageButton.Click += new System.EventHandler(this.clonePackageButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "New Component Design Area:";
            // 
            // ProjectIDTextBox
            // 
            this.ProjectIDTextBox.Location = new System.Drawing.Point(169, 41);
            this.ProjectIDTextBox.Name = "ProjectIDTextBox";
            this.ProjectIDTextBox.Size = new System.Drawing.Size(175, 20);
            this.ProjectIDTextBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "New Project ID:";
            // 
            // ComponentsComboBox
            // 
            this.ComponentsComboBox.FormattingEnabled = true;
            this.ComponentsComboBox.Location = new System.Drawing.Point(169, 12);
            this.ComponentsComboBox.Name = "ComponentsComboBox";
            this.ComponentsComboBox.Size = new System.Drawing.Size(281, 21);
            this.ComponentsComboBox.TabIndex = 5;
            // 
            // applyDesignPlanButton
            // 
            this.applyDesignPlanButton.Location = new System.Drawing.Point(350, 67);
            this.applyDesignPlanButton.Name = "applyDesignPlanButton";
            this.applyDesignPlanButton.Size = new System.Drawing.Size(106, 23);
            this.applyDesignPlanButton.TabIndex = 6;
            this.applyDesignPlanButton.Text = "Apply Design Plan";
            this.applyDesignPlanButton.UseVisualStyleBackColor = true;
            this.applyDesignPlanButton.Click += new System.EventHandler(this.applyDesignPlanButton_Click);
            // 
            // CreateDesignPackages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 102);
            this.Controls.Add(this.applyDesignPlanButton);
            this.Controls.Add(this.ComponentsComboBox);
            this.Controls.Add(this.ProjectIDTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.clonePackageButton);
            this.Name = "CreateDesignPackages";
            this.Text = "Create Design Packages";
            this.Load += new System.EventHandler(this.CR0370AddIn_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button clonePackageButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ProjectIDTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ComponentsComboBox;
        private System.Windows.Forms.Button applyDesignPlanButton;
    }
}