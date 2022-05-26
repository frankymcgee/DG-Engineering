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
        private void DownloadClientList(string url, string token)
        {
            var tasksearch = AssignarConnect(url, token, Method.GET,null);
            var tasks = JsonConvert.DeserializeObject<Clients.Root>(tasksearch);
            if (tasks == null) return;
            foreach (var a in tasks.Data)
            {
                if (a.Active)
                {
                    SimProClient_TextBox.Items.Add(a.Name);                   
                }                
            }
        }
    }
}