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

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// POST Project to Assignar.
        /// </summary>
        private void AssignarProjectPost()
        {
            StatusLabel.Visible = true;
            StatusLabel.Text = @"Creating Project";
            ProjectStartDate.Format = DateTimePickerFormat.Custom;
            ProjectStartDate.CustomFormat = @"yyyy-MM-dd";
            ProjectEndDate.Format = DateTimePickerFormat.Custom;
            ProjectEndDate.CustomFormat = @"yyyy-MM-dd";
            var restClient = new RestClient(Static.AssignarDashboardUrl + "projects");
            var restRequest = new RestRequest(Static.AssignarDashboardUrl + "projects",Method.POST);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", Static.JwtToken);
            var value =@"{" +
                        "\n  \"active\": true" +
                        ",\n  \"id\": " + ProjectJobNumber.Text + 
                        ",\n  \"client_id\": " + _companyId +
                        ",\n  \"name\": \"" + ProjectName.Text + @" - Job Number: " + ProjectJobNumber.Text +  "\"" + 
                        ",\n  \"address\": \"" + ProjectAddress.Text + "\"" + 
                        ",\n  \"external_id\": \"" + ProjectPONumber.Text +  "\"" + 
                        ",\n  \"start_date\": " + ProjectStartDate.Text + 
                        ",\n  \"end_date\": " + ProjectEndDate.Text + 
                        "}";
            restRequest.AddParameter("application/json", value, ParameterType.RequestBody);
            var restResponse = restClient.Execute(restRequest);
            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                _projectId = JsonConvert.DeserializeObject<ProjectPost.Root>(restResponse.Content).Data.Id;
                ClientAddToProject(_projectId.ToString());

                //Thread.Sleep(500);
                //AssignarJobPost("Mobilisation |DS",ProjectJobNumber.Text + "001");
                //Thread.Sleep(500);
                //AssignarJobPost("Mobilisation |NS",ProjectJobNumber.Text + "002");

                Thread.Sleep(500);
                AssignarJobPost("Work | DS",ProjectJobNumber.Text + "001");
                Thread.Sleep(500);
                AssignarJobPost("Work | NS",ProjectJobNumber.Text + "002");
                Thread.Sleep(500);

                //AssignarJobPost("DeMobilisation |DS",ProjectJobNumber.Text + "005");
                //Thread.Sleep(500);
                //AssignarJobPost("DeMobilisation |NS",ProjectJobNumber.Text + "006");
                //Thread.Sleep(500);
                //SimProDocDownload();
                CreateFolderStructure();
                MessageBox.Show(@"Project Created in Assignar. Complete the Details Tab, then add the Documents as Necessary under the Documents Tab.", @"Success");
                try
                {
                    ProjectViewer.CoreWebView2.Navigate("https://dashboard.assignar.com.au/v1/#!/projects/detail/" + ProjectJobNumber.Text + "/edit");
                }
                catch (Exception e)
                {
                    MessageBox.Show(@"Whoops! An Error has occurred trying to navigate to your Project. The error is as follows:" +@"

" + e, @"Error");
                    
                }
                StatusLabel.Visible = false;
                ProgressBar.Value = 0;
            }
            else
            {
                MessageBox.Show(@"Whoops! An Error has occurred trying to create your Project. The error is as follows:" +@"

" + restResponse.Content, @"Error");
                StatusLabel.Visible = false;
                ProgressBar.Value = 0;
            }
        }

        private async void ClientAddToProject(string projectid)
        {
            var contactId = 0;
            var contactsquery =await AssignarConnect(Static.AssignarDashboardUrl + "contacts?company=" + ProjectClient.Text, Static.JwtToken, Method.GET, null);
            var contactsresult = JsonConvert.DeserializeObject<Contacts.Root>(contactsquery);
            foreach (var a in contactsresult.Data.Where(a => a.FirstName == ClientContact.Text.Split(" ".ToCharArray())[0] && ClientContact.Text.Split(" ".ToCharArray())[1].Contains(a.LastName)))
            {
                contactId = a.Id;
            }

            var body = "{\n \"project_id\":" + projectid + ",\n  \"contact_id\":" + contactId + "\n}";
            _ = AssignarConnect(Static.AssignarDashboardUrl + "projects/" + projectid + "/contacts", Static.JwtToken, Method.POST, body);
        }

        private void CreateFolderStructure()
        {
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs",
                ProjectJobNumber.Text,"Administration (TS-Correspondence-Quotes-PO's)"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs",
                ProjectJobNumber.Text,"QA (Material Certs-MDR-NDT)"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs",
                ProjectJobNumber.Text,"Scope (SOW-WMS-SWP's-Photos-Drawings)"));

            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs",
                ProjectJobNumber.Text,"Administration (TS-Correspondence-Quotes-PO's)","Time Sheets"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs",
                ProjectJobNumber.Text,"Administration (TS-Correspondence-Quotes-PO's)","Correspondence"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs",
                ProjectJobNumber.Text,"Administration (TS-Correspondence-Quotes-PO's)","Quotes"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs",
                ProjectJobNumber.Text,"Administration (TS-Correspondence-Quotes-PO's)","Purchase Orders"));

            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs",
                ProjectJobNumber.Text,"QA (Material Certs-MDR-NDT)","Material Certifications"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs",
                ProjectJobNumber.Text,"QA (Material Certs-MDR-NDT)","MDR's"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs",
                ProjectJobNumber.Text,"QA (Material Certs-MDR-NDT)","NDT's"));

            
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs",
                ProjectJobNumber.Text,"Scope (SOW-WMS-SWP's-Photos-Drawings)","Scope of Works"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs",
                ProjectJobNumber.Text,"Scope (SOW-WMS-SWP's-Photos-Drawings)","WMS's"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs",
                ProjectJobNumber.Text,"Scope (SOW-WMS-SWP's-Photos-Drawings)","SWP's"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs",
                ProjectJobNumber.Text,"Scope (SOW-WMS-SWP's-Photos-Drawings)","Photos"));
            Directory.CreateDirectory(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs",
                ProjectJobNumber.Text,"Scope (SOW-WMS-SWP's-Photos-Drawings)","Drawings"));
        }
    }
}