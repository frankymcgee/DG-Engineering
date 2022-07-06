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
            var restClient = new RestClient(Static.AssignarDashboardUrl + "projects")
            {
                Timeout = -1
            };
            var restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", Static.JwtToken);
            var value =@"{" +
                        "\n  \"active\": true" +
                        ",\n  \"client_id\": " + CompanyId +
                        ",\n  \"name\": \"" + ProjectName.Text + "\"" + 
                        ",\n  \"address\": \"" + ProjectAddress.Text + "\"" + 
                        ",\n  \"external_id\": \"" + ProjectJobNumber.Text +  "\"" + 
                        ",\n  \"start_date\": " + ProjectStartDate.Text + 
                        ",\n  \"end_date\": " + ProjectEndDate.Text + 
                        ",\n  \"tags\": [" +
                        "\n            {" +
                        "\n                \"tag_id\": 7" +
                        ",\n                \"name\": \"DGE\"," +
                        "\n                \"description\": \"Project for DGE\"," +
                        "\n                \"color\": \"#f44336\"" +
                        "\n            }\n" +
                        "        ]\n}";
            restRequest.AddParameter("application/json", value, ParameterType.RequestBody);
            var restResponse = restClient.Execute(restRequest);
            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                ProjectId = JsonConvert.DeserializeObject<ProjectPost.Root>(restResponse.Content).Data.Id;
                ClientAddToProject(ProjectId.ToString());
                Thread.Sleep(500);
                AssignarJobPost("Mobilisation |DS");
                Thread.Sleep(500);
                AssignarJobPost("Mobilisation |NS");
                Thread.Sleep(500);
                AssignarJobPost("Work |DS");
                Thread.Sleep(500);
                AssignarJobPost("Work |NS");
                Thread.Sleep(500);
                AssignarJobPost("DeMobilisation |DS");
                Thread.Sleep(500);
                AssignarJobPost("DeMobilisation |NS");
                Thread.Sleep(500);
                //SimProDocDownload();
                MessageBox.Show(@"Project Created in Assignar. Documents have been automatically uploaded. Please add any more as necessary", @"Success");
                DownloadAllProjects(Static.AssignarDashboardUrl + "projects/", Static.JwtToken);
                ProjectViewer.CoreWebView2.Navigate("https://dashboard.assignar.com.au/v1/#!/projects/detail/" + ProjectId + "/documents");
                StatusLabel.Visible = false;
                ProgressBar.Value = 0;
            }
            else
            {
                MessageBox.Show(@"Whoops! An Error has occurred trying to create your Project. It either already exists or has incorrect information. Please try again.", @"Error");
            }
        }

        private void ClientAddToProject(string projectid)
        {
            var contactId = 0;
            var contactsquery = AssignarConnect(Static.AssignarDashboardUrl + "contacts?company=" + ProjectClient.Text, Static.JwtToken, Method.GET, null);
            var contactsresult = JsonConvert.DeserializeObject<Contacts.Root>(contactsquery);
            foreach (var a in contactsresult.Data.Where(a => a.FirstName == ClientContact.Text.Split(" ".ToCharArray())[0] && ClientContact.Text.Split(" ".ToCharArray())[1].Contains(a.LastName)))
            {
                contactId = a.Id;
            }

            var body = "{\n \"project_id\":" + projectid + ",\n  \"contact_id\":" + contactId + "\n}";
            _ = AssignarConnect(Static.AssignarDashboardUrl + "projects/" + projectid + "/contacts", Static.JwtToken, Method.POST, body);
        }
    }
}