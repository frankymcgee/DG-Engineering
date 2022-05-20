using System.Linq;
using DG_Engineering.Framework.Global.Assignar;
using Newtonsoft.Json;
using RestSharp;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Downloads and exports a List of Job Documents.
        /// </summary>
        /// <param name="url">LoginForm.AssignarDashboardUrl + "projects/" + JobPackNo_TextBox.Text + "/documents/"</param>
        /// <param name="token">LoginForm.JwtToken</param>
        private void DownloadJobDocuments(string url, string token)
        {
            var docsearch = AssignarConnect(url, token, Method.GET,null);
            var project = JsonConvert.DeserializeObject<Documents.Root>(docsearch);
            if (project == null) return;
            foreach (var a in project.Data)
            {
                StatusLabel.Text = @"Adding " + a.Label + @" to the List.";
                JobDocuments_ComboBox.Items.Add(a.Label);
            }
        }
    }
}