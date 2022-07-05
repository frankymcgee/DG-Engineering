using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

// ReSharper disable once CheckNamespace
namespace DG_Engineering
{
    public partial class LoginWindow
    {
        /// <summary>
        /// Initiates the Login Process in Assignar for Audit Logging.
        /// </summary>
        public void Process()
        {
            if (!string.IsNullOrEmpty(Username_TextBox.Text) || !string.IsNullOrEmpty(Password_TextBox.Text))
            {
                if (Request(ClientID_TextBox.Text,Username_TextBox.Text, Password_TextBox.Text) == HttpStatusCode.OK)
                {
                    Static.ClientId = ClientID_TextBox.Text;
                    Static.UserName = Username_TextBox.Text;
                    Static.Password = Password_TextBox.Text;
                    if (SaveLoginInfo.CheckState == CheckState.Checked)
                    {
                        if (new FileInfo(Static.Cache).Length == 0)
                            using (var sw = new StreamWriter(Static.Cache))
                            {
                                sw.WriteLineAsync(
                                    Static.ClientId + Environment.NewLine + Static.UserName + Environment.NewLine +
                                    Static.Password);
                                sw.Flush();
                                sw.Close();
                            }
                    }
                    // Hide This Login
                    //Set the Property to true while closing the Login Form. This Property will be checked before running
                    //the Assignar Form in the Main Method
                    Static.IsLoggedIn = true;
                    Hide();
                    var myobform = new MyobAccess();
                    var assignarForm = new MainWindow();
                    myobform.FormClosed += (s, args) =>
                    {
                        MainWindow.MyobGetAccessToken();
                        assignarForm.Show();
                    };
                    myobform.Show();
                    assignarForm.Closed += (s, args) => Close();
                    //assignarForm.Show();
                }
                else
                {
                    const string message = "Whoops!" + "\n\n Looks like you have a login error! \n Please try again.";
                    const string title = "Login Error";
                    MessageBox.Show(message, title);
                }
            }
            else
            {
                const string message = "Whoops!" +
                                       "\n\n Looks like you haven't put in your Username or Password \n Please try again.";
                const string title = "Login Error";
                MessageBox.Show(message, title);
            }
        }
    }
}