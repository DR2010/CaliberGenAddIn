﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace WrapperTool.WindowsForm
{
    internal partial class NumberedTextBox : UserControl
    {
        public NumberedTextBox()
        {
            InitializeComponent();

            numberLabel.Font = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size);
        }


        private void updateNumberLabel()
        {
            //we get index of first visible char and number of first visible line
            Point pos = new Point(0, 0);
            int firstIndex = richTextBox1.GetCharIndexFromPosition(pos);
            int firstLine = richTextBox1.GetLineFromCharIndex(firstIndex);

            //now we get index of last visible char and number of last visible line
            pos.X = ClientRectangle.Width;
            pos.Y = ClientRectangle.Height;
            int lastIndex = richTextBox1.GetCharIndexFromPosition(pos);
            int lastLine = richTextBox1.GetLineFromCharIndex(lastIndex);

            //this is point position of last visible char, we'll use its Y value for calculating numberLabel size
            pos = richTextBox1.GetPositionFromCharIndex(lastIndex);


            //finally, renumber label
            numberLabel.Text = "";
            for (int i = firstLine; i < lastLine +1 ; i++)
            {
                numberLabel.Text += i + 1 + "\n";
            }

        }


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            updateNumberLabel();
        }

        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            //move location of numberLabel for amount of pixels caused by scrollbar
            int d = richTextBox1.GetPositionFromCharIndex(0).Y % (richTextBox1.Font.Height + 1);
            numberLabel.Location = new Point(0, d);

            updateNumberLabel();
        }
        public RichTextBoxScrollBars ScrollBars
        {
            get { return richTextBox1.ScrollBars; }
            set { richTextBox1.ScrollBars = value; }
        }
        public override string Text
        {
            get { return richTextBox1.Text; }
            set { richTextBox1.Text = value; }
        }
        public bool ReadOnly 
        {
            get { return richTextBox1.ReadOnly; }
            set { 
                richTextBox1.ReadOnly = value;
                if (richTextBox1.ReadOnly)
                    richTextBox1.BackColor = System.Drawing.SystemColors.Window;
                else
                    richTextBox1.BackColor = System.Drawing.SystemColors.Control;
            }
        }
        private void richTextBox1_Resize(object sender, EventArgs e)
        {
            richTextBox1_VScroll(null, null);
        }

        private void richTextBox1_FontChanged(object sender, EventArgs e)
        {
            updateNumberLabel();
            richTextBox1_VScroll(null, null);
        }




    }
}
