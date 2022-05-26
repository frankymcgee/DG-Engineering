using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
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
        /// POST Project from SimPro to Assignar.
        /// </summary>
        public void AssignarProjectPost()
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
            var value = "{\n  \"active\": true,\n  \"client_id\": " + CompanyId + ",\n  \"name\": " + ProjectNameTextBox.Text + ",\n  \"address\": " + ProjectAddress_TextBox.Text + ",\n  \"external_id\": " + SimProQuoteText.Text + ",\n  \"start_date\": " + ProjectStartDate.Text + ",\n  \"end_date\": " + ProjectEndDate.Text + ",\n  \"tags\": [\n            {\n                \"tag_id\": 7,\n                \"name\": \"DGE\",\n                \"description\": \"Project for DGE\",\n                \"color\": \"#f44336\"\n            }\n        ]\n}";
            restRequest.AddParameter("application/json", value, ParameterType.RequestBody);
            var restResponse = restClient.Execute(restRequest);
            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                ProjectId = JsonConvert.DeserializeObject<ProjectPost.Root>(restResponse.Content).Data.Id;
                ClientAddToProject(ProjectId.ToString());
                AssignarJobPost("Mobilisation|DS");
                StatusLabel.Text = @"Creating Job: Mobilisation|NS";
                AssignarJobPost("Mobilisation|NS");
                StatusLabel.Text = @"Creating Job: Work|DS";
                AssignarJobPost("Work|DS");
                StatusLabel.Text = @"Creating Job: Work|NS";
                AssignarJobPost("Work|NS");
                StatusLabel.Text = @"Creating Job: DeMobilisation|DS";
                AssignarJobPost("DeMobilisation|DS");
                StatusLabel.Text = @"Creating Job: DeMobilisation|NS";
                AssignarJobPost("DeMobilisation|NS");
                SimProDocDownload();
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
        public void ClientAddToProject(string projectid)
        {
            var contact_id = 0;
            var contactsquery = AssignarConnect(Static.AssignarDashboardUrl + "contacts?company=" + SimProClient_TextBox.Text, Static.JwtToken, RestSharp.Method.GET, null);
            var contactsresult = JsonConvert.DeserializeObject<Contacts.Root>(contactsquery);
            foreach (var a in contactsresult.Data)
            {
                if (a.FirstName == ClientContact_ComboBox.Text.Split(" ".ToCharArray())[0] && ClientContact_ComboBox.Text.Split(" ".ToCharArray())[1].Contains(a.LastName))
                {
                    contact_id = a.Id;
                }
            }
            var body = "{\n \"project_id\":" + projectid + ",\n  \"contact_id\":" + contact_id + "\n}";
            _ = AssignarConnect(Static.AssignarDashboardUrl + "projects/" + projectid + "/contacts", Static.JwtToken, Method.POST, body);
        }
    }
}