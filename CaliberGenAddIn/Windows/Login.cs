using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;
using Starbase.CaliberRM.Interop;

namespace EAAddIn.Windows
{
    public partial class Login : Form
    {
        public Login(String server, String username)
            : this()
        {
            if (String.IsNullOrEmpty(username))
            {
                username = WindowsIdentity.GetCurrent().Name.Split('\\')[1];
            }
            txtServer.Text = server;
            txtUserID.Text = username;

            mskPassword.Focus();
            //this.AcceptButton = btnLogin;
        }

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            ISession sess;
            try
            {
                sess = SessionManager.Object.CreateCaliberSession(txtServer.Text, txtUserID.Text, mskPassword.Text);
            }
            catch (COMException ex)
            {
                if ( ex.Message == "com.starbase.caliber.server.InvalidLoginException")
                {
                    MessageBox.Show("Invalid user name or password.", "Login Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(String.Format("Login failed with error {0}", ex.Message), "Login Failure");
                }

                DialogResult = DialogResult.None;
                return;
            }

            //Hide();

            Cursor.Current = Cursors.Arrow;
        }
    }
}