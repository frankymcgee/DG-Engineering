using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using DG_Engineering.Framework.Global.Assignar.ProjectPost;
using DG_Engineering.Framework.Global.Assignar;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// POST Project to Assignar.
        /// </summary>
        private async Task AssignarProjectPost()
        {
            StatusLabel.Visible = true;
            StatusLabel.Text = @"Creating Project";
            ProjectStartDate.Format = DateTimePickerFormat.Custom;
            ProjectStartDate.CustomFormat = @"yyyy-MM-dd";
            ProjectEndDate.Format = DateTimePickerFormat.Custom;
            ProjectEndDate.CustomFormat = @"yyyy-MM-dd";
            string modifiedname = "";
            foreach (char c in ProjectName.Text)
            {
                if (c == '\\' || c == '/')
                {
                    modifiedname += "-";
                }
                else
                {
                    modifiedname += c;
                }
            }
            var body = @"{" +
                        "\n  \"active\": true" +
                        ",\n  \"client_id\": " + _companyId +
                        ",\n  \"name\": \"" + ProjectName.Text + @" - Job Number: " + ProjectJobNumber.Text + "\"" +
                        ",\n  \"address\": \"" + Predictions.Text + "\"" +
                        ",\n  \"external_id\": \"" + ProjectJobNumber.Text + "\"" +
                        ",\n  \"start_date\": " + ProjectStartDate.Text +
                        ",\n  \"end_date\": " + ProjectEndDate.Text +
                        "}";
            await AssignarAPIConnect("/projects/", Method.Post, body);
            var project_id = JsonConvert.DeserializeObject<ProjectPost.Root>(Static.AssignarResponseContent).Data.Id;
            if (project_id.ToString().Length > 0)
            {
                try
                {
                    ClientAddToProject(project_id.ToString());
                    Thread.Sleep(500);
                    await AssignarJobPost(project_id,"Work | DS", ProjectJobNumber.Text + "001");
                    Thread.Sleep(500);
                    await AssignarJobPost(project_id,"Work | NS", ProjectJobNumber.Text + "002");
                    Thread.Sleep(500);
                    //CreateFolderStructure();
                    //DownloadDocument();
                    MessageBox.Show(@"Project Created in Assignar. Complete the Details Tab, then add the Documents as Necessary under the Documents Tab.", @"Success");
                    try
                    {
                        ProjectViewer.CoreWebView2.Navigate("https://dashboard.assignar.com.au/v1/#!/projects/detail/" + ProjectJobNumber.Text + "/edit");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(@"Whoops! An Error has occurred trying to navigate to your Project. The error is as follows:" + @"

" + e, @"Error");

                    }
                    StatusLabel.Visible = false;
                }
                catch (Exception e)
                {
                    MessageBox.Show(@"Whoops! An Error has occurred trying to create your Project. The error is as follows:" + @"

" + e, @"Error");
                    StatusLabel.Visible = false;
                }
            }
        }
            public async void ClientAddToProject(string projectid)
        {
            var contactId = 0;
            await AssignarAPIConnect("/contacts?company=" + ProjectClient.Text, Method.Get, null);
            var contactsquery =Static.AssignarResponseContent;
            var contactsresult = JsonConvert.DeserializeObject<Contacts.Root>(contactsquery);
            foreach (var a in contactsresult.Data.Where(a => a.FirstName == ClientContact.Text.Split(" ".ToCharArray())[0] && ClientContact.Text.Split(" ".ToCharArray())[1].Contains(a.LastName)))
            {
                contactId = a.Id;
            }

            var body = "{\n \"project_id\":" + projectid + ",\n  \"contact_id\":" + contactId + "\n}";
            _ = AssignarAPIConnect("/projects/" + projectid + "/contacts", Method.Post, body);
        }

        private void CreateFolderStructure()
        {

            string modifiedname = "";
            foreach (char c in ProjectName.Text)
            {
                if (c == '\\' || c == '/')
                {
                    modifiedname += "\\" + c;
                }
                else
                {
                    modifiedname += c;
                }
            }
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs", ProjectJobNumber.Text + " - " + modifiedname,"Administration (TS-Correspondence-Quotes-PO's)"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs", ProjectJobNumber.Text + " - " + modifiedname, "QA-QC (Material Certs-MDR-NDT)"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs", ProjectJobNumber.Text + " - " + modifiedname, "Scopes (SOW-WMS-SWP's-Photos-Drawings)"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs", ProjectJobNumber.Text + " - " + modifiedname, "WHS (JHA-Risk Assessments-Incidents-Investigations)"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs", ProjectJobNumber.Text + " - " + modifiedname, "SimPro Extracted Documents"));

            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs", ProjectJobNumber.Text + " - " + modifiedname, "Administration (TS-Correspondence-Quotes-PO's)","Correspondence"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs", ProjectJobNumber.Text + " - " + modifiedname, "Administration (TS-Correspondence-Quotes-PO's)", "Purchase Orders - Redacted Info only"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs", ProjectJobNumber.Text + " - " + modifiedname, "Administration (TS-Correspondence-Quotes-PO's)", "Quotes - Redacted Info Only"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs", ProjectJobNumber.Text + " - " + modifiedname, "Administration (TS-Correspondence-Quotes-PO's)", "Timesheets - Sub-Contractors Timesheets"));

            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs", ProjectJobNumber.Text + " - " + modifiedname, "QA-QC (Material Certs-MDR-NDT)", "Material Certs"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs", ProjectJobNumber.Text + " - " + modifiedname, "QA-QC (Material Certs-MDR-NDT)", "Tool Certs"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs", ProjectJobNumber.Text + " - " + modifiedname, "QA-QC (Material Certs-MDR-NDT)", "ITP's"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs", ProjectJobNumber.Text + " - " + modifiedname, "QA-QC (Material Certs-MDR-NDT)", "MDR's"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs", ProjectJobNumber.Text + " - " + modifiedname, "QA-QC (Material Certs-MDR-NDT)", "NDT's"));

            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs", ProjectJobNumber.Text + " - " + modifiedname, "Scopes (SOW-WMS-SWP's-Photos-Drawings)", "Scope of Works"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs", ProjectJobNumber.Text + " - " + modifiedname, "Scopes (SOW-WMS-SWP's-Photos-Drawings)", "WMS's & Methodologies"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs", ProjectJobNumber.Text + " - " + modifiedname, "Scopes (SOW-WMS-SWP's-Photos-Drawings)", "SWP's"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs", ProjectJobNumber.Text + " - " + modifiedname, "Scopes (SOW-WMS-SWP's-Photos-Drawings)", "WIN's"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs", ProjectJobNumber.Text + " - " + modifiedname, "Scopes (SOW-WMS-SWP's-Photos-Drawings)", "Photo's"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs", ProjectJobNumber.Text + " - " + modifiedname, "Scopes (SOW-WMS-SWP's-Photos-Drawings)", "Drawings"));

            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs", ProjectJobNumber.Text + " - " + modifiedname, "WHS (JHA-Risk Assessments-Incidents-Investigations)", "JHA Records"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs", ProjectJobNumber.Text + " - " + modifiedname, "WHS (JHA-Risk Assessments-Incidents-Investigations)", "Risk Assessments"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs", ProjectJobNumber.Text + " - " + modifiedname, "WHS (JHA-Risk Assessments-Incidents-Investigations)", "Incidents"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs", ProjectJobNumber.Text + " - " + modifiedname, "WHS (JHA-Risk Assessments-Incidents-Investigations)", "Investigations"));
            DownloadDocument();
        }
    }
}