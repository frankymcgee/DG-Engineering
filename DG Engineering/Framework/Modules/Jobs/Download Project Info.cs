using System;
using System.Diagnostics;
using System.Windows.Forms;
using DG_Engineering.Framework.Global.Assignar;
using Newtonsoft.Json;
using RestSharp;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Downloads Specific Project Information
        /// </summary>
        /// <param name="url">The URL of Assignar i.e., https://api.assignar.com.au/v2/projects and the Project Number.</param>
        /// <param name="token">The JWT retrieved for the Auth API.</param>
        private void DownloadProjectInformation(string url, string token)
        {
            var jobsearch = AssignarConnect(url, token, Method.GET,null);
            var project = JsonConvert.DeserializeObject<ProjectNumber.Project>(jobsearch);
            Debug.Assert(project != null, nameof(project) + " != null");
            var coords = project.Data.AddressGeo.Coordinates.ToArray();
            {
                var lat = coords[0];
                var lng = coords[1];
                Project_Name_TextBox.Text = project.Data.Name;
                Start_Date_TextBox.Text = DateTime.Parse(project.Data.StartDate).ToShortDateString();
                End_Date_TextBox.Text = DateTime.Parse(project.Data.EndDate).ToShortDateString();
                Client_Name_TextBox.Text = project.Data.Client.Name;
                Site_Address_TextBox.Text = project.Data.Address;
                Suburb_TextBox.Text = project.Data.Suburb;
                State_TextBox.Text = project.Data.State;
                Postcode_TextBox.Text = project.Data.Postcode;
                Project_External_ID_TextBox.Text =
                    project.Data.ExternalId == null ? "NA" : project.Data.ExternalId.ToString();
                if (project.Data.Active) Project_Active_CheckBox.CheckState = CheckState.Checked;
                DisplayJobLocation(lat, lng);
            }

            Manual_Search_TextBox.Text = project.Data.Id.ToString();
            DownloadAllJobs(Static.AssignarDashboardUrl + "orders/", token);
            Job_Number_TextBox.Clear();
            Job_Description_TextBox.Clear();
            Job_Location_TextBox.Clear();
            Job_Start_TextBox.Clear();
            Job_End_TextBox.Clear();
            Job_Po_TextBox.Clear();
        }
    }
}