using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using DG_Engineering.Framework.Global.Assignar;
using Newtonsoft.Json;
using RestSharp;
using Quotes = DG_Engineering.Framework.Global.MYOB.Quotes;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Search MYOB for Order from Number.
        /// </summary>
        public void MyobSearch()
        {
            StatusLabel.Visible = true;
            StatusLabel.Text = @"Searching";
            Directory.CreateDirectory(Output + "Files");
            var di = new DirectoryInfo(Output + "Files");
            var files = di.GetFiles();
            foreach (var file in files)
            {
                file.Delete();
            }
            ProgressBar.PerformStep();
            var quotes = MyobConnect(Static.companyfileuri + "/"+ Static.companyfileguid + "/Sale/Order", Method.GET).Content;
            var quoteinfo = JsonConvert.DeserializeObject<Quotes.Item>(quotes);
            if (quoteinfo?.Number != null)
                foreach (var dummy in quoteinfo.Number.Where(a => quoteinfo.Number == SimProQuoteText.Text))
                {
                    SimProClient_TextBox.Text = quoteinfo.Customer.Name;
                    ProjectNameTextBox.Text = quoteinfo.JournalMemo;
                    ProjectAddress_TextBox.Text = quoteinfo.Customer.Name;
                    ProjectPOTextBox.Text = string.IsNullOrEmpty(quoteinfo.CustomerPurchaseOrderNumber)
                        ? @"MISSING PO NUMBER"
                        : quoteinfo.CustomerPurchaseOrderNumber;
                    ProjectPOTextBox.Font = ProjectPOTextBox.Text == @"MISSING PO NUMBER"
                        ? new Font(ProjectPOTextBox.Font, FontStyle.Bold)
                        : new Font(ProjectPOTextBox.Font, FontStyle.Regular);
                    ProgressBar.PerformStep();
                }

            CompanyIdExtract(SimProClient_TextBox.Text);
            ProgressBar.PerformStep();
            ClientContact_ComboBox.Items.Clear();
            var contactsquery = AssignarConnect(Static.AssignarDashboardUrl + "contacts?company=" + SimProClient_TextBox.Text,Static.JwtToken,Method.GET,null);
            var contactsresult = JsonConvert.DeserializeObject<Contacts.Root>(contactsquery);
            if (contactsresult?.Data != null)
                foreach (var a in contactsresult.Data)
                {
                    ClientContact_ComboBox.Items.Add(a.FirstName + " " + a.LastName + "-" + a.JobTitle);
                }

            var documents = SimProConnect(SimProUrl + "jobs/" + SimProQuoteText.Text + "/attachments/files/").Content;
            var result = JsonConvert.DeserializeObject<List<Framework.Global.SimPro.Documents.Root>>(documents);
            StatusLabel.Text = @"Found " + result?.Count + @" Documents.";
            ProgressBar.Value = 0;
        }
    }
}