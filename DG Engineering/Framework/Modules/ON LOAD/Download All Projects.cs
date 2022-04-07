using System.Linq;
using DG_Engineering.Framework.Global.Assignar;
using Newtonsoft.Json;
using RestSharp;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Downloads a List of All Active Projects in Assignar
        /// </summary>
        /// <param name="url">LoginForm.AssignarDashboardUrl + "projects/"</param>
        /// <param name="token">LoginForm.JwtToken</param>
        private void DownloadAllProjects(string url, string token)
        {
            var projectsearch = AssignarConnect(url, token, Method.GET);
            var project = JsonConvert.DeserializeObject<Projects.Project>(projectsearch);
            if (project == null) return;
            foreach (var a in project.Data.Where(a => a.Active))
            {
                All_Projects_ComboBox.Items.Add(a.Id + " - " + a.Name);
            }
        }
    }
}