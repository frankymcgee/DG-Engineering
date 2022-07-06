using System.Linq;
using DG_Engineering.Framework.Global.Assignar;
using Newtonsoft.Json;
using RestSharp;

// ReSharper disable once CheckNamespace
namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Downloads all Role Descriptions in Assignar.
        /// </summary>
        /// <param name="url">LoginForm.AssignarDashboardUrl + "tasks/"</param>
        /// <param name="token">LoginForm.JwtToken</param>
        private void DownloadClientList(string url, string token)
        {
            var tasksearch = AssignarConnect(url, token, Method.GET,null);
            var tasks = JsonConvert.DeserializeObject<Clients.Root>(tasksearch);
            if (tasks == null) return;
            var sortList = (from a in tasks.Data where a.Active select a.Name).ToList();
            sortList.Sort();
            foreach (var num in sortList)
            {
                ProjectClient.Items.Add(num);
            }
        }
    }
}