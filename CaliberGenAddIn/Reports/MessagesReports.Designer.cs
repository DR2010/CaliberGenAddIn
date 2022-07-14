namespace EAAddIn.Reports
{
    /// <summary>
    /// Summary description for MessagesReports.
    /// </summary>
    partial class MessagesReports
    {
        private DataDynamics.ActiveReports.PageHeader pageHeader;
        private DataDynamics.ActiveReports.Detail detail;
        private DataDynamics.ActiveReports.PageFooter pageFooter;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }

        #region ActiveReport Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MessagesReports));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.label1 = new DataDynamics.ActiveReports.Label();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.TextTextBox = new DataDynamics.ActiveReports.TextBox();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            this.TypeTextBox = new DataDynamics.ActiveReports.TextBox();
            this.groupHeader1 = new DataDynamics.ActiveReports.GroupHeader();
            this.groupFooter1 = new DataDynamics.ActiveReports.GroupFooter();
            this.line1 = new DataDynamics.ActiveReports.Line();
            this.HeaderTextBox = new DataDynamics.ActiveReports.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.label1,
            this.HeaderTextBox});
            this.pageHeader.Height = 0.7708333F;
            this.pageHeader.Name = "pageHeader";
            // 
            // label1
            // 
            this.label1.Border.BottomColor = System.Drawing.Color.Black;
            this.label1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.label1.Border.LeftColor = System.Drawing.Color.Black;
            this.label1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.label1.Border.RightColor = System.Drawing.Color.Black;
            this.label1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.label1.Border.TopColor = System.Drawing.Color.Black;
            this.label1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.label1.Height = 0.625F;
            this.label1.HyperLink = null;
            this.label1.Left = 0.125F;
            this.label1.Name = "label1";
            this.label1.Style = "ddo-char-set: 0; font-weight: bold; font-style: italic; font-size: 24pt; font-fam" +
                "ily: Comic Sans MS; ";
            this.label1.Text = "Messages Report";
            this.label1.Top = 0.125F;
            this.label1.Width = 2.875F;
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.TextTextBox});
            this.detail.Height = 0.2083333F;
            this.detail.Name = "detail";
            // 
            // TextTextBox
            // 
            this.TextTextBox.Border.BottomColor = System.Drawing.Color.Black;
            this.TextTextBox.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Dot;
            this.TextTextBox.Border.LeftColor = System.Drawing.Color.Black;
            this.TextTextBox.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextTextBox.Border.RightColor = System.Drawing.Color.Black;
            this.TextTextBox.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextTextBox.Border.TopColor = System.Drawing.Color.Black;
            this.TextTextBox.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextTextBox.DataField = "Description";
            this.TextTextBox.Height = 0.1875F;
            this.TextTextBox.Left = 0.375F;
            this.TextTextBox.Name = "TextTextBox";
            this.TextTextBox.Style = "text-align: left; ";
            this.TextTextBox.Text = "TextTextBox";
            this.TextTextBox.Top = 0F;
            this.TextTextBox.Width = 6.0625F;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0.25F;
            this.pageFooter.Name = "pageFooter";
            // 
            // TypeTextBox
            // 
            this.TypeTextBox.Border.BottomColor = System.Drawing.Color.Black;
            this.TypeTextBox.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TypeTextBox.Border.LeftColor = System.Drawing.Color.Black;
            this.TypeTextBox.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TypeTextBox.Border.RightColor = System.Drawing.Color.Black;
            this.TypeTextBox.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TypeTextBox.Border.TopColor = System.Drawing.Color.Black;
            this.TypeTextBox.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TypeTextBox.DataField = "Type";
            this.TypeTextBox.Height = 0.1979167F;
            this.TypeTextBox.Left = 0F;
            this.TypeTextBox.Name = "TypeTextBox";
            this.TypeTextBox.Style = "text-align: left; font-weight: bold; ";
            this.TypeTextBox.Text = "TypeTextBox";
            this.TypeTextBox.Top = 0F;
            this.TypeTextBox.Width = 1F;
            // 
            // groupHeader1
            // 
            this.groupHeader1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.TypeTextBox});
            this.groupHeader1.DataField = "Type";
            this.groupHeader1.Height = 0.2291667F;
            this.groupHeader1.Name = "groupHeader1";
            // 
            // groupFooter1
            // 
            this.groupFooter1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.line1});
            this.groupFooter1.Height = 0.1458333F;
            this.groupFooter1.Name = "groupFooter1";
            // 
            // line1
            // 
            this.line1.Border.BottomColor = System.Drawing.Color.Black;
            this.line1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.line1.Border.LeftColor = System.Drawing.Color.Black;
            this.line1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.line1.Border.RightColor = System.Drawing.Color.Black;
            this.line1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.line1.Border.TopColor = System.Drawing.Color.Black;
            this.line1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.line1.Height = 0F;
            this.line1.Left = 0F;
            this.line1.LineWeight = 1F;
            this.line1.Name = "line1";
            this.line1.Top = 0.0625F;
            this.line1.Width = 6.5625F;
            this.line1.X1 = 0F;
            this.line1.X2 = 6.5625F;
            this.line1.Y1 = 0.0625F;
            this.line1.Y2 = 0.0625F;
            // 
            // HeaderTextBox
            // 
            this.HeaderTextBox.Border.BottomColor = System.Drawing.Color.Black;
            this.HeaderTextBox.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.HeaderTextBox.Border.LeftColor = System.Drawing.Color.Black;
            this.HeaderTextBox.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.HeaderTextBox.Border.RightColor = System.Drawing.Color.Black;
            this.HeaderTextBox.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.HeaderTextBox.Border.TopColor = System.Drawing.Color.Black;
            this.HeaderTextBox.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.HeaderTextBox.DataField = "Header";
            this.HeaderTextBox.Height = 0.5625F;
            this.HeaderTextBox.Left = 4.125F;
            this.HeaderTextBox.Name = "HeaderTextBox";
            this.HeaderTextBox.Style = "vertical-align: bottom; ";
            this.HeaderTextBox.Top = 0F;
            this.HeaderTextBox.Width = 2.3125F;
            // 
            // MessagesReports
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11.69F;
            this.PageSettings.PaperWidth = 8.27F;
            this.PrintWidth = 6.5625F;
            this.Sections.Add(this.pageHeader);
            this.Sections.Add(this.groupHeader1);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.groupFooter1);
            this.Sections.Add(this.pageFooter);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
                        "l; font-size: 10pt; color: Black; ", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold; ", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
                        "lic; ", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold; ", "Heading3", "Normal"));
            ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.Label label1;
        private DataDynamics.ActiveReports.TextBox TypeTextBox;
        private DataDynamics.ActiveReports.TextBox TextTextBox;
        private DataDynamics.ActiveReports.GroupHeader groupHeader1;
        private DataDynamics.ActiveReports.GroupFooter groupFooter1;
        private DataDynamics.ActiveReports.Line line1;
        private DataDynamics.ActiveReports.TextBox HeaderTextBox;
    }
}
