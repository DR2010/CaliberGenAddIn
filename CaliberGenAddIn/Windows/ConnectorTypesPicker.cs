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
    public partial class ConnectorTypesPicker : Form
    {
        public ConnectorTypesPicker()
        {
            InitializeComponent();
        }

        public List<string> ConnectorTypes
        {
            get;set;
        }

        private void ConnectorTypesPicker_Load(object sender, EventArgs e)
        {
            t_connectortypesTableAdapter.Fill(eAReleaseDataSet.t_connectortypes);

            foreach (DataGridViewRow row in ConnectorTypesDataGridView.Rows)
            {
                row.Selected = ConnectorTypes.Contains(row.Cells[0].Value.ToString());
            }
        }

        private void OKbutton_Click(object sender, EventArgs e)
        {
            ConnectorTypes = new List<string>();

            if (ConnectorTypesDataGridView.SelectedRows.Count > 0
                && ConnectorTypesDataGridView.SelectedRows.Count == ConnectorTypesDataGridView.Rows.Count)
            {
                ConnectorTypes.Add("All");
            }
            else
            {
                foreach (DataGridViewRow row in ConnectorTypesDataGridView.SelectedRows)
                {
                    ConnectorTypes.Add(row.Cells[0].Value.ToString());
                }
            }

        }
    }
}
