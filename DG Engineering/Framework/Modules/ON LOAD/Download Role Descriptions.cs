﻿using DG_Engineering.Framework.Global.Assignar;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

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
            var sort_list = new List<string>();
            var tasksearch = AssignarConnect(url, token, Method.GET,null);
            var tasks = JsonConvert.DeserializeObject<Roles.Root>(tasksearch);
            if (tasks == null) return;
            foreach (var a in tasks.Data)
            {
                if (a.Active)
                {
                    sort_list.Add(a.Name);
                }
                //Job_Position_ComboBox.Items.Add(a.Name);
            }
            sort_list.Sort();
            foreach (var num in sort_list)
            {
                Job_Position_ComboBox.Items.Add(num);
            }
        }
    }
}