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
            var jobs = SimProConnect(SimProUrl + "jobs/" + SimProQuoteText.Text).Content;
            var jobinfo = JsonConvert.DeserializeObject<Framework.Global.SimPro.Jobs.Root>(jobs);
            if (jobinfo == null) return;
            SimProClient_TextBox.Text = jobinfo.Customer.CompanyName;
            ProjectNameTextBox.Text = jobinfo.Name;
            ProjectAddress_TextBox.Text = jobinfo.Site.Name;
            ProjectPOTextBox.Text = string.IsNullOrEmpty(jobinfo.OrderNo) ?@"MISSING PO NUMBER": jobinfo.OrderNo;
            ProgressBar.PerformStep();
            ProjectPOTextBox.Font = ProjectPOTextBox.Text == @"MISSING PO NUMBER" ? new Font(ProjectPOTextBox.Font, FontStyle.Bold) : new Font(ProjectPOTextBox.Font, FontStyle.Regular);
            CompanyIdExtract(SimProClient_TextBox.Text);
            ProgressBar.PerformStep();
            ClientContact_ComboBox.Items.Clear();
            var contactsquery = AssignarConnect(Static.AssignarDashboardUrl + "contacts?company=" + SimProClient_TextBox.Text,Static.JwtToken,RestSharp.Method.GET,null);
            var contactsresult = JsonConvert.DeserializeObject<Contacts.Root>(contactsquery);
            foreach (var a in contactsresult.Data)
            {
                ClientContact_ComboBox.Items.Add(a.FirstName + " " + a.LastName + "-" + a.JobTitle);
            }
            var documents = SimProConnect(SimProUrl + "jobs/" + SimProQuoteText.Text + "/attachments/files/").Content;
            var result = JsonConvert.DeserializeObject<List<Framework.Global.SimPro.Documents.Root>>(documents);
            StatusLabel.Text = @"Found " + result.Count + @" Documents.";
            ProgressBar.Value = 0;
        }
    }
}