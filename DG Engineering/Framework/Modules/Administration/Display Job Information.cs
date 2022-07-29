using System;
using DG_Engineering.Framework.Global.Assignar;
using Newtonsoft.Json;
using RestSharp;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        
        /// <summary>
        /// Displays Job Information
        /// </summary>
        /// <param name="url">URL to be searched. i.e., "Static.AssignarDashboardUrl + "orders/" + AdminJobNo.Text"</param>
        /// <param name="token">The Bearer Token Supplied</param>
        private async void AdminDownloadJobInformation(string url, string token)
        {
            var jobsearch =  await AssignarConnect(url, token, Method.GET, null);
            var jobinfo = JsonConvert.DeserializeObject<AdminJobInfo.Root>(jobsearch);
            AdminJobDesc.Text = jobinfo.Data.JobDescription;
            AdminJobLoc.Text = jobinfo.Data.Location;
            AdminJobStart.Text = DateTime.Parse(jobinfo.Data.StartDate).ToString("dd-MM-yyyy");
            AdminJobEnd.Text = DateTime.Parse(jobinfo.Data.EndDate).ToString("dd-MM-yyyy");
            AdminJobPO.Text = jobinfo.Data.PoNumber;
            AdminViewer.CoreWebView2.Navigate("https://dashboard.assignar.com.au/v1/#!/orders/detail/" + AdminJobNo.Text + "/general");
        }
    }
}