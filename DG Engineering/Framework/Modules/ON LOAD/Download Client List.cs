using System.Linq;
using System.Threading.Tasks;
using DG_Engineering.Framework.Global.Assignar;
using Newtonsoft.Json;
using RestSharp;

// ReSharper disable once CheckNamespace
namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Downloads all Clients in Assignar.
        /// </summary>
        /// <param name=""></param>
        /// <returns>The List of Clients from Assignar in the Clients ComboBox.</returns>
        private async Task DownloadClientList()
        {
            await AssignarAPIConnect("/clients", Method.Get, null);
            var tasksearch = Static.AssignarResponseContent;
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