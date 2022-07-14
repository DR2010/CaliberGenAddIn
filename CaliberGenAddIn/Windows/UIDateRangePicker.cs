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
    public partial class UIDateRangePicker : Form
    {

        public DateTime DateFrom;
        public DateTime DateTo;

        public UIDateRangePicker( DateTime _dateFrom, DateTime _dateTo)
        {
            InitializeComponent();

            datePickerStart.Value = _dateFrom;
            datePickerEnd.Value = _dateTo;
           
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateFrom = datePickerStart.Value;
            DateTo = datePickerEnd.Value;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UIDateRangePicker_Load(object sender, EventArgs e)
        {

        }

    }
}
