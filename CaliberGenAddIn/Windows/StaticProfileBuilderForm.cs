using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EAAddIn.Windows.Interfaces;

namespace EAAddIn.Windows
{
    public partial class StaticProfileBuilderForm : Form, IStaticProfileBuilder
    {
        public StaticProfileBuilderForm()
        {
            InitializeComponent();
        }

        public string Diagram
        {
            get { return textBoxStaticProfileDiagram.Text; }
            set { textBoxStaticProfileDiagram.Text = value; }
        }

        private void buttonBuildXML_Click(object sender, EventArgs e)
        {

        }
    }
}
