using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using DG_Engineering.Framework.Global.Assignar.ProjectPost;
using Newtonsoft.Json;
using RestSharp;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// POST Project from SimPro to Assignar.
        /// </summary>
        public async Task AssignarProjectPost()
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
                
                StatusLabel.Text = @"Creating Job: Mobilisation|DS";
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
                StatusLabel.Visible = false;
                await SimProDocDownload();
                MessageBox.Show(@"Project Created in Assignar. Please upload the Documents that are required into the Project.", @"Success");
                DownloadAllProjects(Static.AssignarDashboardUrl + "projects/", Static.JwtToken);
                ProjectViewer.CoreWebView2.Navigate("https://dashboard.assignar.com.au/v1/#!/projects/detail/" + ProjectId + "/documents");
                Process.Start(Output + "Files/");
            }
            else
            {
                MessageBox.Show(@"Whoops! An Error has occurred trying to create your Project. Please try again.", @"Error");
            }
        }
        
    }
}