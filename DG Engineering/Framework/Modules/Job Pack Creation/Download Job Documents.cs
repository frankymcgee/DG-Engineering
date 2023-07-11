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
        private async void DownloadJobDocuments(string url)
        { 
            await AssignarAPIConnect(url, Method.Get,null);
            var docsearch = Static.AssignarResponseContent;
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