using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using EA;

namespace EAAddIn.Windows.Dialog
{
    public partial class PleaseWaitDialog : Form
    {
        private Thread Worker;
        private Action<string> updateStatus;

        public PleaseWaitDialog()
        {
            InitializeComponent();

            updateStatus = UpdateStatusText;
        }



        private void timerBlink_Tick(object sender, EventArgs e)
        {
            label2.Visible = !label2.Visible;

            //_blinkCount++;

            //if (_blinkCount == _maxNumberOfBlinks * 2)
            //{
            //    timerBlink.Stop();
            //    label2.Visible = true;
            //}

            if (!Worker.IsAlive)
                Close();
        }

        public void RunProcess(Thread worker)
        {
            Worker = worker;

            Worker.IsBackground = true;
            Worker.SetApartmentState(ApartmentState.STA);
            Worker.Start();

            timerBlink.Start();

            ShowDialog();
            
        }

        public void UpdateStatus(string text)
        {
            BeginInvoke(updateStatus, text);
            
        }

        public void UpdateStatusText(string text)
        {
                textBoxStatus.Text = text;
        }


  


        //internal void RunBackgroundWorker(BackgroundWorker worker)
        //{
        //    worker.RunWorkerAsync();
        //}
    }
}
