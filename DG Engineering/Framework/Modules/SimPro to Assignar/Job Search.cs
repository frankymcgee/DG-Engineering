using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using DG_Engineering.Framework.Global.Assignar;
using DG_Engineering.Framework.Global.MYOB;
using Newtonsoft.Json;
using RestSharp;
using Jobs = DG_Engineering.Framework.Global.MYOB.Jobs;

// ReSharper disable once CheckNamespace
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
            var jobsearch = MyobConnect(Static.Companyfileuri + "/"+ Static.Companyfileguid + "/GeneralLedger/Job", Method.GET).Content;
            var jobsearchresult = JsonConvert.DeserializeObject<Jobs.Item>(jobsearch);
            if (jobsearchresult != null)
                foreach (var dummy in jobsearchresult.Number.Where(a => jobsearchresult.Number == SimProQuoteText.Text))
                {
                    var customerId = jobsearchresult.LinkedCustomer.DisplayId;
                    var customersearch = MyobConnect(Static.Companyfileuri + "/"+ Static.Companyfileguid + "/Contact/Customer?$filter=DisplayID eq \'" + customerId + "\'", Method.GET).Content;
                    var customersearchresult = JsonConvert.DeserializeObject <Customer.Item>(customersearch);
                    if (customersearchresult?.Addresses != null)
                    {
                        foreach (var a in customersearchresult.Addresses)
                        {
                            ProjectAddress_TextBox.Items.Add(a.Street + ", " + a.City + ", " + a.State);
                        }
                    }
                    SimProClient_TextBox.Text = jobsearchresult.LinkedCustomer.Name;
                    ProjectNameTextBox.Text = jobsearchresult.Name;
                    ProjectPOTextBox.Text = @"MISSING PO NUMBER";
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