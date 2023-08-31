using System;
using System.Threading.Tasks;
using DG_Engineering.Framework.Global.Assignar;
using Newtonsoft.Json;
using RestSharp;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        
        /// <summary>
        /// Displays Shift Information
        /// </summary>
        /// <param name="url">URL to be searched. i.e.: "/orders/" + Shift Number"</param>
        private async Task AssignarShiftInformation( string url)
        {
            await AssignarAPIConnect(url, Method.Get, null);
            var jobsearch = Static.AssignarResponseContent;
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