using System;
using System.IO;
using DG_Engineering.Framework.Global.Assignar;
using DG_Engineering.Framework.Global.MYOB;
using Newtonsoft.Json;
using RestSharp;

// ReSharper disable once CheckNamespace
namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Search MYOB for Order from Number.
        /// </summary>
        private void MyobSearch()
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

            string fixedjobnumber;
            ProgressBar.PerformStep();
            if (ProjectJobNumber.Text.Contains("'"))
            {
                var index = ProjectJobNumber.Text.IndexOf("'", StringComparison.Ordinal);
                fixedjobnumber = ProjectJobNumber.Text.Insert(index, "'");
            }
            else
            {
                fixedjobnumber = ProjectJobNumber.Text;
            }
            var jobsearch = MyobConnect(Static.Companyfileuri + "/"+ Static.Companyfileguid + "/GeneralLedger/Job?$filter=Number eq \'" + fixedjobnumber + "\'", Method.GET).Content;
            Console.WriteLine(jobsearch);
            var jobsearchresult = JsonConvert.DeserializeObject<Job.Root>(jobsearch);
            foreach (var a in jobsearchresult.Items)
            {
                ProjectClient.Text = a.LinkedCustomer.Name;
                ProjectName.Text = a.Name;
                ProjectAddress.Text = a.Contact;
                ProjectPONumber.Text = a.Manager.ToString();
                
                
            }
            CompanyIdExtract(ProjectClient.Text);
            ProgressBar.PerformStep();
            ClientContact.Items.Clear();
            var contactsquery = AssignarConnect(Static.AssignarDashboardUrl + "contacts?company=" + ProjectClient.Text,Static.JwtToken,Method.GET,null);
            var contactsresult = JsonConvert.DeserializeObject<Contacts.Root>(contactsquery);
            if (contactsresult?.Data != null)
                foreach (var a in contactsresult.Data)
                {
                    ClientContact.Items.Add(a.FirstName + " " + a.LastName + "-" + a.JobTitle);
                }

            //var documents = SimProConnect(SimProUrl + "jobs/" + ProjectJobNumber.Text + "/attachments/files/").Content;
            //var result = JsonConvert.DeserializeObject<List<Framework.Global.SimPro.Documents.Root>>(documents);
            //StatusLabel.Text = @"Found " + result?.Count + @" Documents.";
            StatusLabel.Visible = false;
            ProgressBar.Value = 0;
        }
    }
}