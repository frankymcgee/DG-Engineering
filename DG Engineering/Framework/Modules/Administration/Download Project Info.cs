using System.Diagnostics;
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
            var project = JsonConvert.DeserializeObject<Projectinfo.Root>(jobsearch);
            Debug.Assert(project != null, nameof(project) + " != null");
            foreach (var a in project.Data)
            {
                AdminProjName.Text = a.Name;
                var order = a.Id.ToString();
                Static.AssignarInternalNumber = order;
            }
            AdminJobNo.Clear();
            AdminJobDesc.Clear();
            AdminJobLoc.Clear();
            AdminJobStart.Clear();
            AdminJobEnd.Clear();
            AdminJobPO.Clear();            
        }
    }
}