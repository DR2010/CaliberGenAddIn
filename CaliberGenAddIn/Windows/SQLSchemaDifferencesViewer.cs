using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EAAddIn.Applications.SQLServerScriptGenerator;

namespace EAAddIn.Windows
{
    public partial class SQLSchemaDifferencesViewer : Form
    {
        List<DbDifference> differences = new List<DbDifference>();

        public SQLSchemaDifferencesViewer(List<DbDifference> data)
        {
            InitializeComponent();
            differences = data;

            ViewerDataGrid.DataSource = differences;
        }

        public DataGridViewColumn GetColumn(int col)
        {
            return ViewerDataGrid.Columns[col];
        }

       private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
