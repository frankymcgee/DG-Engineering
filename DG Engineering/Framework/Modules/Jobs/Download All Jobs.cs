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
        /// Downloads a List of All Active Jobs in a Project in Assignar
        /// </summary>
        /// <param name="url">LoginForm.AssignarDashboardUrl + "projects/" + Manual_Search_TextBox.Text</param>
        /// <param name="token">LoginForm.JwtToken</param>
        public void DownloadAllJobs(string url, string token)
        {
            if (string.IsNullOrEmpty(All_Projects_ComboBox.Text))
            {
                var jobsearch = AssignarConnect(url, token, Method.GET,null);
                var jobs = JsonConvert.DeserializeObject<Jobs.Root>(jobsearch);
                if (jobs == null) return;
                Job_List_ComboBox.Items.Clear();
                foreach (var a in jobs.Data.Where(a => a.ProjectId == Convert.ToInt32(Manual_Search_TextBox.Text)))
                {
                    if (!string.IsNullOrEmpty(a.PoNumber))
                    {
                        Project_Po_TextBox.Text = a.PoNumber;
                    }

                    Job_List_ComboBox.Items.Add(a.Id + " - " + a.JobDescription);
                }
            }
            else
            {
                //Manual_Search_TextBox.Text = All_Projects_ComboBox.Text.Split(Convert.ToChar("-"))[0];
                var jobsearch = AssignarConnect(url, token, Method.GET,null);
                var jobs = JsonConvert.DeserializeObject<Jobs.Root>(jobsearch);
                if (jobs == null) return;
                Job_List_ComboBox.Items.Clear();
                foreach (var a in jobs.Data.Where(a =>
                             a.ProjectId == Convert.ToInt32(All_Projects_ComboBox.Text.Split(Convert.ToChar("-"))[0])))
                {
                    if (!string.IsNullOrEmpty(a.PoNumber))
                    {
                        Project_Po_TextBox.Text = a.PoNumber;
                    }

                    Job_List_ComboBox.Items.Add(a.Id + " - " + a.JobDescription);
                }
            }
        }
        /// <summary>
        /// Downloads a List of All Active Jobs in a Project in Assignar
        /// </summary>
        /// <param name="url">LoginForm.AssignarDashboardUrl + "projects/" + Manual_Search_TextBox.Text</param>
        /// <param name="token">LoginForm.JwtToken</param>
        public void AdminDownloadAllJobs(string url, string token)
        {
                var jobsearch = AssignarConnect(url, token, Method.GET, null);
                var jobs = JsonConvert.DeserializeObject<Jobs.Root>(jobsearch);
                if (jobs == null) return;
                AdminJobComboBox.Items.Clear();
                foreach (var a in jobs.Data)
                {                
                    AdminJobComboBox.Items.Add(a.Id + " | " +a.JobDescription);
                }            
        }
    }
}