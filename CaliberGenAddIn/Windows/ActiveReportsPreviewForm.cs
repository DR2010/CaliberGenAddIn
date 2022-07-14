using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EAAddIn.Windows
{
    public partial class ActiveReportsPreviewForm : Form, IPreviewActiveReport
    {
        public ActiveReportsPreviewForm(DataDynamics.ActiveReports.ActiveReport3 report)
        {
            InitializeComponent();

            viewer1.Document = report.Document;
        }

        public event EventHandler<EventArgs> PreviewFormClosed;

        private void ActiveReportsPreviewForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (PreviewFormClosed != null)
            {
                PreviewFormClosed(this, EventArgs.Empty);
            }
        }
    }
}
