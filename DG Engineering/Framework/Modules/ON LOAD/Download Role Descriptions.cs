using DG_Engineering.Framework.Global.Assignar;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Downloads all Discipliness in Assignar.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>The List of the Disciplines in the Job Position ComboBox</returns>
        public async Task DownloadRoleDescriptions()
        {
            await AssignarAPIConnect("/tasks", Method.Get, null);
            string tasksearch = Static.AssignarResponseContent;
                var tasks =  JsonConvert.DeserializeObject<Roles.Root>(tasksearch);
                if (tasks == null) return;
                var sortList = (from a in tasks.Data where a.Active select a.Name).ToList();
                sortList.Sort();
                foreach (var num in sortList)
                {
                Job_Position_ComboBox.Items.Add(num);
                fwPosHeld.Items.Add(num);
                 }
            
            
        }
    }
}