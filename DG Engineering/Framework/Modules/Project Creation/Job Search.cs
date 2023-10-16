using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        private async Task MyobSearch(string projectnumber)
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
            if (projectnumber.Contains("'"))
            {
                var index = projectnumber.IndexOf("'", StringComparison.Ordinal);
                fixedjobnumber = projectnumber.Insert(index, "'");
            }
            else
            {
                fixedjobnumber = projectnumber;
            }
            var joburl = "/GeneralLedger/Job?$filter=substringof(\'" + fixedjobnumber + "\'" + ",Number) eq true";
            var jobsearch = MyobConnect(Static.Companyfileuri + "/"+ Static.Companyfileguid,joburl, Method.Get).Content;
            Console.WriteLine(jobsearch);
            var jobsearchresult = JsonConvert.DeserializeObject<Job.Root>(jobsearch);
            int quotenumber;
            foreach (var a in jobsearchresult.Items)
            {
                Match match = Regex.Match(a.Description, @"\d+$");
                if (match.Success)
                {
                    quotenumber = int.Parse(match.Value);
                }
                else
                {
                    quotenumber = 0;
                }
                ProjectClient.Text = a.LinkedCustomer.Name;
                ProjectName.Text = a.Name;
                ProjectAddress.Text = a.Contact;
                ProjectPONumber.Text = a.Manager.ToString();
                QuoteNo_TextBox.Text = quotenumber.ToString();
                Jobs_Client.Text = a.LinkedCustomer.Name;
                Jobs_JobName.Text = a.Description;
                Jobs_Site.Text = a.Contact;
                Jobs_PoNo.Text = a.Manager.ToString();
            }
            await CompanyIdExtract(ProjectClient.Text);
            ClientContact.Items.Clear();
            await AssignarAPIConnect("/contacts?company=" + ProjectClient.Text, Method.Get, null);
            var contactsquery = Static.AssignarResponseContent;
            var contactsresult = JsonConvert.DeserializeObject<Contacts.Root>(contactsquery);
            if (contactsresult?.Data != null)
                foreach (var a in contactsresult.Data)
                {
                    ClientContact.Items.Add(a.FirstName + " " + a.LastName + "  -  " + a.JobTitle);
                }
            StatusLabel.Visible = false;
        }
    }
}