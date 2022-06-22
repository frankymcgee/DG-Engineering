using System;
using System.Net.Mail;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        private void SendEmail()
        {
            try
            {
                var mySmtpClient = new SmtpClient("smtp.office365.com", 587) {UseDefaultCredentials = false};

                // set smtp-client with basicAuthentication
                var basicAuthenticationInfo = new System.Net.NetworkCredential("info@dgengineering.com.au", "Huy23335");
                mySmtpClient.EnableSsl = true;
                mySmtpClient.TargetName = "STARTTLS/smtp.office365.com";
                mySmtpClient.Credentials = basicAuthenticationInfo;


                // add from,to mail addresses
                var from = new MailAddress("info@dgengineering.com.au", "DG Engineering | Info");
                var to = new MailAddress("ap@dgengineering.com.au", "Accounts Payable");
                var myMail = new System.Net.Mail.MailMessage(@from, to);

                // add ReplyTo
                var replyTo = new MailAddress("info@dgengineering.com.au");
                myMail.ReplyToList.Add(replyTo);

                // set subject and encoding
                myMail.Subject = "Test message";
                myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                // set body-message and encoding
                myMail.Body = "<p><span style=\"font-family: Helvetica;\">Hey there!,</span></p>" +
                              "<p><span style=\"font-family: Helvetica;\">A Project has just been created in Assignar:</span></p>" +
                              "<p><span style=\"font-family: Helvetica;\"><br></span></p>" +
                              "<p><span style=\"font-family: Helvetica;\">Project Name: " + ProjectNameTextBox.Text +
                              "</span></p>" +
                              "<p><span style=\"font-family: Helvetica;\">Job Number: " + SimProQuoteText.Text +
                              "</span></p>" +
                              "<p><span style=\"font-family: Helvetica;\"><br></span></p>" +
                              "<p><span style=\"font-family: Helvetica;\">Please create a Job Folder for this when you can.</span></p>" +
                              "<table style=\"border: none;width:100.0%;\">" +
                              "<tbody>" +
                              "<tr>" +
                              "<td colspan=\"2\" style=\"width: 101%;padding: 0cm;vertical-align: top;\">" +
                              "<p style='margin:0cm;font-size:15px;font-family:\"Calibri\",sans-serif;margin-bottom:12.0pt;'><span style='font-size:15px;font-family:\"Tahoma\",sans-serif;color:black;'>Kind Regards,</span></p>" +
                              "</td>" +
                              "</tr>" +
                              "<tr>" +
                              "<td style=\"width: 37%;padding: 0cm;vertical-align: top;\">" +
                              "<p style='margin:0cm;font-size:15px;font-family:\"Calibri\",sans-serif;'><strong><span style='font-size:19px;font-family:\"Tahoma\",sans-serif;color:#9B1A1E;'>DGE Automation</span></strong><span style='font-size:15px;font-family:\"Tahoma\",sans-serif;color:black;'><br>Automation Robot<br></span><span style='font-size:15px;font-family:\"Tahoma\",sans-serif;color:#9B1A1E;'>De Wet &amp; Green Engineering&nbsp;</span></p>" +
                              "</td>" +
                              "<td style=\"width:64.0%;padding:0cm 0cm 0cm 0cm;\">" +
                              "<p style='margin:0cm;font-size:15px;font-family:\"Calibri\",sans-serif;'><br></p>" +
                              "</td>" +
                              "</tr>" +
                              "</tbody>" +
                              "</table>";
                myMail.BodyEncoding = System.Text.Encoding.UTF8;
                // text or html
                myMail.IsBodyHtml = true;

                mySmtpClient.Send(myMail);
            }

            catch (SmtpException ex)
            {
                throw new ApplicationException
                    ("SmtpException has occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}