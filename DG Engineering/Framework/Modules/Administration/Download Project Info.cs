using System;
using DG_Engineering.Framework.Global.Assignar;
using Newtonsoft.Json;
using RestSharp;

namespace DG_Engineering
{
    public partial class MainWindow
    {
       /// <summary>
        /// Downloads Specific Project Information
        /// </summary>
        /// <param name="url">The URL of Assignar i.e., https://api.assignar.com.au/v2/projects and the Project Number.</param>
        /// <param name="token">The JWT retrieved for the Auth API.</param>
        private async void AdminProjectInformation(string url, string token)
        {
            var jobsearch = await AssignarConnect(url, token, Method.GET, null);
            if (jobsearch.Contains("something went wrong."))
            {
                jobsearch = ( await AssignarConnect(Static.AssignarDashboardUrl + "projects?external_id=" + AdminProjectNumber.Text, token, Method.GET, null));
            }
            string order = null;
            try
            {
                var project = JsonConvert.DeserializeObject<Projectinfo.Root>(jobsearch);
                AdminProjName.Text = project.Data.Name;
                order = project.Data.Id.ToString();
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
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
            ListJobs(order);
        }
        /// <summary>
        /// List all References under a Project in the Administration Tab.
        /// </summary>
        /// <param name="projectnumber">The Project Number being referenced.</param>
       private async void ListJobs(string projectnumber)
       {
           var request = await AssignarConnect(Static.AssignarDashboardUrl + "orders?project_id=" + projectnumber,
               Static.JwtToken, Method.GET, null);
           var response = JsonConvert.DeserializeObject<Jobs.Root>(request);
           foreach (var a in response.Data)
           {
               AdminJobComboBox.Items.Add(a.Id + " | " + a.JobDescription);
           }
       }
    }
}