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
        /// Searches MYOB with the Supplied Project Number
        /// </summary>
        /// <param name="projectnumber">Project Number to Search. Has the option to search if there is a ' in the Job Number.</param>
        private void MyobSearch(string projectnumber)
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
            if (projectnumber.Contains("'"))
            {
                var index = projectnumber.IndexOf("'", StringComparison.Ordinal);
                fixedjobnumber = projectnumber.Insert(index, "'");
            }
            else
            {
                fixedjobnumber = projectnumber;
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

                Jobs_Client.Text = a.LinkedCustomer.Name;
                Jobs_JobName.Text = a.Description;
                Jobs_Site.Text = a.Contact;
                Jobs_PoNo.Text = a.Manager.ToString();
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
            StatusLabel.Visible = false;
            ProgressBar.Value = 0;
        }
    }
}