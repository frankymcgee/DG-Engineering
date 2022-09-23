using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace DG_Engineering
{
    public partial class LoginWindow : Form
    {
       
        public LoginWindow()
        {
            InitializeComponent();
            if (!Debugger.IsAttached)
            {
                DebugMode.Visible = false;
            }
            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)) +
                                  @"\DGE"))
            {
                Directory.CreateDirectory(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),@"DGE"));
            }
            if (!File.Exists(Static.Cache))
            {
                File.Create(Static.Cache).Close();
            }
            if (new FileInfo(Static.Cache).Length == 0) return;
            Username_TextBox.Text = ReadFile(Static.Cache, 3);
            Password_TextBox.Text = ReadFile(Static.Cache, 4);
        }
        private void Login_Button_Click(object sender, EventArgs e)
        {
            Process();
        }

        private void Password_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Process();
            }
        }
        private void ResetPasswordLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://dashboard.assignar.com.au/auth/reset-password");
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
