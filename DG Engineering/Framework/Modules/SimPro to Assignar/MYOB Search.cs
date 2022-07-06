using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using DG_Engineering.Framework.Global.SimPro;
using DG_Engineering.Framework.Global.Assignar;
using Newtonsoft.Json;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Search SimPro for Quote/Job from Number.
        /// </summary>
        public void SimProSearch()
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
            var jobs = SimProConnect(SimProUrl + "jobs/" + ProjectJobNumber.Text).Content;
            var jobinfo = JsonConvert.DeserializeObject<Framework.Global.SimPro.Jobs.Root>(jobs);
            if (jobinfo == null) return;
            ProjectClient.Text = jobinfo.Customer.CompanyName;
            ProjectName.Text = jobinfo.Name;
            ProjectAddress.Text = jobinfo.Site.Name;
            ProjectPONumber.Text = string.IsNullOrEmpty(jobinfo.OrderNo) ?@"MISSING PO NUMBER": jobinfo.OrderNo;
            ProgressBar.PerformStep();
            ProjectPONumber.Font = ProjectPONumber.Text == @"MISSING PO NUMBER" ? new Font(ProjectPONumber.Font, FontStyle.Bold) : new Font(ProjectPONumber.Font, FontStyle.Regular);
            CompanyIdExtract(ProjectClient.Text);
            ProgressBar.PerformStep();
            ClientContact.Items.Clear();
            var contactsquery = AssignarConnect(Static.AssignarDashboardUrl + "contacts?company=" + ProjectClient.Text,Static.JwtToken,RestSharp.Method.GET,null);
            var contactsresult = JsonConvert.DeserializeObject<Contacts.Root>(contactsquery);
            foreach (var a in contactsresult.Data)
            {
                ClientContact.Items.Add(a.FirstName + " " + a.LastName + "-" + a.JobTitle);
            }
            var documents = SimProConnect(SimProUrl + "jobs/" + ProjectJobNumber.Text + "/attachments/files/").Content;
            var result = JsonConvert.DeserializeObject<List<Framework.Global.SimPro.Documents.Root>>(documents);
            StatusLabel.Text = @"Found " + result.Count + @" Documents.";
            ProgressBar.Value = 0;
        }
    }
}