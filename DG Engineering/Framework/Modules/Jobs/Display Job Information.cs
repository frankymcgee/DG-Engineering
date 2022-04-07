using System;
using System.Linq;
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
        /// <param name="url">LoginForm.AssignarDashboardUrl + "orders/" + Job_List_ComboBox.Text.Split(Convert.ToChar("-"))[0]</param>
        /// <param name="token">LoginForm.JwtToken</param>
        private void DownloadJobInformation(string url, string token)
        {
            var jobsearch = AssignarConnect(url, token, Method.GET);
            var jobinfo = JsonConvert.DeserializeObject<JobInfo.Root>(jobsearch);
            if (jobinfo == null) return;
            Job_Number_TextBox.Text = jobinfo.Data.Id.ToString();
            Job_Description_TextBox.Text = jobinfo.Data.JobDescription;
            Job_Location_TextBox.Text = jobinfo.Data.Location;
            Job_Start_TextBox.Text = DateTime.Parse(jobinfo.Data.StartDate).ToString("dd-MM-yyyy");
            Job_End_TextBox.Text = DateTime.Parse(jobinfo.Data.EndDate).ToString("dd-MM-yyyy");
            Job_Po_TextBox.Text = jobinfo.Data.PoNumber;
            if (!jobinfo.Data.Geolocation.Coordinates.Any()) return;
            var coords = jobinfo.Data.Geolocation.Coordinates.ToArray();
            var lat = coords[0];
            var lng = coords[1];
            DisplayJobLocation(lat, lng);
        }
    }
}