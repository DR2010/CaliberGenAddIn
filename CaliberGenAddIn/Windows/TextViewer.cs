using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EAAddIn.Windows
{
    public partial class TextViewer : Form
    {
        public TextViewer()
        {
            InitializeComponent();
        }

        public string ViewerText
        {
            set { ViewerRichTextBox.Text = value; }
        }

        private void SaveAsButton_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
                                     {
                                         Filter = "Text Files (*.txt)| *.txt",
                                         Title = "Save",
                                         RestoreDirectory = true
                                     };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var streamWriter = File.CreateText(saveFileDialog.FileName.Trim());
                streamWriter.WriteLine(ViewerRichTextBox.Text);
                streamWriter.Close();
            }
        } 
    }
}
