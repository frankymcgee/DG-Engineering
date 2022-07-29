using DG_Engineering.Framework.Global.Assignar;
using Newtonsoft.Json;
using RestSharp;
using System.Linq;

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
        private async void DownloadRoleDescriptions(string url, string token)
        {
            var tasksearch = await AssignarConnect(url, token, Method.GET,null);
            var tasks = JsonConvert.DeserializeObject<Roles.Root>(tasksearch);
            if (tasks == null) return;
            var sortList = (from a in tasks.Data where a.Active select a.Name).ToList();
            sortList.Sort();
            foreach (var num in sortList)
            {
                Job_Position_ComboBox.Items.Add(num);
            }
        }
    }
}