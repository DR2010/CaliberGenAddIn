using System;
using System.Data;
using System.Windows.Forms;

namespace EAAddIn.Windows
{
    public partial class DataSourceViewer : Form
    {
        public DataSourceViewer(object data)
        {
            InitializeComponent();
            ViewerDataGrid.DataSource = data;
            
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