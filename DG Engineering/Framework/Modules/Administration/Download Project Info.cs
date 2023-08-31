using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using DG_Engineering.Framework.Global.Assignar;
using Newtonsoft.Json;
using RestSharp;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Search Assignar for Job Information
        /// </summary>
        /// <param name="url">/projects?external_id=" + the Assignar Job Number</param>
        /// <returns></returns>
        private async Task AdminJobInformation( string url)
        {
            await AssignarAPIConnect(url, Method.Get, null);
            var jobsearch = Static.AssignarResponseContent;
            if (jobsearch.Contains("something went wrong."))
            {
                await AssignarAPIConnect("/projects?external_id=" + AdminProjectNumber.Text, Method.Get, null);
                jobsearch = Static.AssignarResponseContent;
            }
            string order = null;
            try
            {
                var project = JsonConvert.DeserializeObject<Projectinfo.Root>(jobsearch);
                AdminProjName.Text = project.Data.Name;
                order = project.Data.Id.ToString();
            }
            catch (Exception)
            {
                var projectold = JsonConvert.DeserializeObject<Projectinfoold.Root>(jobsearch);
                foreach (var a in projectold.Data)
                {
                    AdminProjName.Text = a.Name;
                    order = a.Id.ToString();
                }
            }
            Static.AssignarInternalNumber = order;
            AdminJobNo.Clear();
            AdminJobDesc.Clear();
            AdminJobLoc.Clear();
            AdminJobStart.Clear();
            AdminJobEnd.Clear();
            AdminJobPO.Clear();
            AdminJobComboBox.Items.Clear();
            AssignarListShifts(order);
        }
       /// <summary>
       /// List Assignar Shifts within an Assignar Job
       /// </summary>
       /// <param name="projectnumber">Assignar Job Number</param>
        private async void AssignarListShifts(string projectnumber)
        {
            await AssignarAPIConnect("/orders?project_id=" + projectnumber,Method.Get, null);
            var request = Static.AssignarResponseContent;
            var response = JsonConvert.DeserializeObject<Jobs.Root>(request);
            foreach (var a in response.Data)
            {
                AdminJobComboBox.Items.Add(a.Id + " | " + a.JobDescription);
                _companyId = a.ClientId;
                _projectId= a.ProjectId;
            }
        }
       /// <summary>
       /// Creates a new Manual Shift under the Assignar Job.
       /// </summary>
       /// <returns>The newly Generated Shift.</returns>
        private async Task AssignarNewShift()
        {
            string value;
            StatusLabel.Text = @"Creating Job:  " + NewJobDesc.Text;
            if (Debugger.IsAttached)
            {
                value = "{" +
                    "\n  \"id\": " + NewJobNumber.Text +
                    ",\n  \"active\": true" +
                    ",\n  \"job_number\": \"" + _projectId.ToString() + "\"" +
                    ",\n  \"po_number\": \"" + NewJobPO.Text + "\"" +
                    ",\n  \"client_id\": " + _companyId +
                    ",\n  \"order_owner\": 48" +
                    ",\n  \"project_id\": " + _projectId +
                    ",\n  \"location\": \"" + NewJobLoc.Text + "\"" +
                    ",\n  \"job_description\": \"" + NewJobDesc.Text + "\"" +
                    ",\n  \"start_time\": \"\"" +
                    ",\n  \"shift_duration\": \"\"" +
                    ",\n  \"start_date\": \"" + NewJobStart.Text + "\"" +
                    ",\n  \"end_date\": \"" + NewJobEnd.Text + "\"" +
                    ",\n  \"comments\": \"" + NewJobDesc.Text + "\"" +
                    ",\n  \"status_id\": 5" +
                    ",\n  \"type_id\": 1" +
                    ",\n  \"supplier_id\": null" +
                    "\n}";
            }
            else
            {
                value = "{" +
                    ",\n  \"id\": " + NewJobNumber.Text +
                    ",\n  \"active\": true" +
                    ",\n  \"job_number\": \"" + _projectId.ToString() + "\"" +
                    ",\n  \"po_number\": " + NewJobPO.Text +
                    ",\n  \"client_id\": " + _companyId +
                    ",\n  \"order_owner\": 186" +
                    ",\n  \"project_id\": " + _projectId +
                    ",\n  \"location\": \"" + NewJobLoc.Text + "\"" +
                    ",\n  \"job_description\": \"" + NewJobDesc.Text + "\"" +
                    ",\n  \"start_time\": \"\"" +
                    ",\n  \"shift_duration\": \"\"" +
                    ",\n  \"start_date\": " + NewJobStart.Text +
                    ",\n  \"end_date\": " + NewJobEnd.Text +
                    ",\n  \"comments\": \"" + NewJobDesc.Text + "\"" +
                    ",\n  \"status_id\": 5" +
                    ",\n  \"type_id\": 1" +
                    ",\n  \"supplier_id\": null" +
                    "\n}";
            }
            await AssignarAPIConnect("/orders", Method.Post, value);
            StatusLabel.Text = null;
            MessageBox.Show(@"Job Creation Status: " + Static.AssignarResponseDescription, @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            AdminViewer.CoreWebView2.Navigate("https://dashboard.assignar.com.au/v1/#!/orders/detail/" + NewJobNumber.Text + "/general");
        }
    }
}