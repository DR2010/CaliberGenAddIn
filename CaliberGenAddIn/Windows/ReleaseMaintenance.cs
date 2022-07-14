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
    public partial class ReleaseMaintenance : Form
    {
        public ReleaseMaintenance()
        {
            InitializeComponent();
        }

        private void releaseBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.releaseBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.eACaliberCoolgenDataSet);

        }

        private void ReleaseMaintenance_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'eACaliberCoolgenDataSet.Release' table. You can move, or remove it, as needed.
            this.releaseTableAdapter.Fill(this.eACaliberCoolgenDataSet.Release);

        }
    }
}
