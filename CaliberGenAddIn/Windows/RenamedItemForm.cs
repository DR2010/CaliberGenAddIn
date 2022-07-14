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
    public partial class RenamedItemForm : Form, IRename
    {
        public RenamedItemForm()
        {
            InitializeComponent();
        }

        #region IRename Members

        public string NewName
        {
            get
            {
                return NewNameTextBox.Text;
            }
        }
        public string OldName
        {
            set
            {
                Text = string.Format(Text, value);
            }
        }

        #endregion
    }
}
