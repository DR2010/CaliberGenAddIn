using System;
using System.Collections;
using System.Windows.Forms;

namespace EAAddIn.Windows
{
    public partial class SelectStereotype : Form
    {

        public SelectStereotype()
        {
            InitializeComponent();
        }

        private void SelectStereotype_Load(object sender, EventArgs e)
        {
            var eaSQL = new EaAccess();
            ArrayList stereotypeList = eaSQL.GetStereotypeList("Class");
            cboStereotype.DataSource = stereotypeList;
        }

        public string SelectedStereotype
        {
            get
            {
                return cboStereotype.Text;
            }
        }

        public bool AppendExisitngStereotypes
        {
            get
            {
                return cbAppendStereotype.Checked;
            }
        }


    }
}
