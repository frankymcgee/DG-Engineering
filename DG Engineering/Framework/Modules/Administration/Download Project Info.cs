using System;
using System.Collections.Generic;
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
        private void AdminProjectInformation(string url, string token)
        {
            var jobsearch = AssignarConnect(url, token, Method.GET, null);
            if (jobsearch.Contains("something went wrong."))
            {
                jobsearch = (AssignarConnect(Static.AssignarDashboardUrl + "projects?external_id=" + AdminProjectNumber.Text, token, Method.GET, null));
            }
            Console.WriteLine(jobsearch);
            var project = JsonConvert.DeserializeObject<Projectinfo.Root>(jobsearch);
            AdminProjName.Text = project.Data.Name;
            var order = project.Data.Id.ToString();
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
       private void ListJobs(string projectnumber)
       {
           var request = AssignarConnect(Static.AssignarDashboardUrl + "orders?project_id=" + projectnumber,
               Static.JwtToken, Method.GET, null);
           var response = JsonConvert.DeserializeObject<Jobs.Root>(request);
           foreach (var a in response.Data)
           {
               AdminJobComboBox.Items.Add(a.Id + " | " + a.JobDescription);
           }
       }
    }
}