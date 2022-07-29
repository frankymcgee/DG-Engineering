using System;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using DG_Engineering.Framework.Global.Assignar.ProjectPost;
using Newtonsoft.Json;
using RestSharp;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// POST Project to Assignar.
        /// </summary>
        private void AssignarProjectPostfromGantt()
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
            StatusLabel.Text = @"Creating Jobs";
            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                _projectId = JsonConvert.DeserializeObject<ProjectPost.Root>(restResponse.Content).Data.Id;

                OpenProjectFile(_projectId);

                ClientAddToProject(_projectId.ToString());
                MessageBox.Show(@"Project Created in Assignar. Complete the Details Tab, then add the Documents as Necessary under the Documents Tab.", @"Success");
                ProjectViewer.CoreWebView2.Navigate("https://dashboard.assignar.com.au/v1/#!/projects/detail/" + _projectId + "/edit");
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
        private void OpenProjectFile(int projectnumber)
        {
            var projectApp = new Microsoft.Office.Interop.MSProject.Application();
            // Open the file. There are a number of options in this constructor as you can see
            projectApp.FileOpen("C:\\test.mpp", true, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Microsoft.Office.Interop.MSProject.PjPoolOpen.pjDoNotOpenPool, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            // Get the active project
            var proj = projectApp.ActiveProject;
            // Enumerate the tasks
            var idnumber = Convert.ToInt32(projectnumber + "001");
            foreach (Microsoft.Office.Interop.MSProject.Task a in proj.Tasks)
            {
                if (a.ResourceGroup.Contains("CABS"))
                {
                    idnumber++;
                    var comment = @"WO Number: " + a.Text2 + " |  OP Number: " + a.Number1 + "\n\n | Summary: " +
                                  a.Text3 + " |  Job: "+ a.Name;
                    AssignarJobPostFromGantt(a.Name,Convert.ToString(idnumber),a.Start,a.Finish,comment);
                }
            }

        }
    }
}