using DG_Engineering.Framework.Global.Assignar;
using Newtonsoft.Json;
using RestSharp;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Downloads all Role Descriptions in Assignar.
        /// </summary>
        /// <param name="url">LoginForm.AssignarDashboardUrl + "tasks/"</param>
        /// <param name="token">LoginForm.JwtToken</param>
        private void DownloadRoleDescriptions(string url, string token)
        {
            var tasksearch = AssignarConnect(url, token, Method.GET);
            var tasks = JsonConvert.DeserializeObject<Roles.Root>(tasksearch);
            if (tasks == null) return;
            foreach (var a in tasks.Data)
            {
                Job_Position_ComboBox.Items.Add(a.Name);
            }
        }
    }
}