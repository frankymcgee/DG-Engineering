using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using DG_Engineering.Framework.Global.Assignar;
using Newtonsoft.Json;
using RestSharp;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Will Download the Specified Document to the Job Pack.
        /// </summary>
        /// <param name="url">LoginForm.AssignarDashboardUrl + "projects/" + JobPackNo_TextBox.Text + "/documents/"</param>
        /// <param name="token">LoginForm.JwtToken</param>
        private void DownloadDocumentFromUrl(string url, string token)
        {
            var docsearch = AssignarConnect(url, token, Method.GET);
            var project = JsonConvert.DeserializeObject<Documents.Root>(docsearch);
            if (project == null) return;
            foreach (var a in project.Data.Where(a => a.Label == JobDocuments_ComboBox.Text))
            {
                using (var client = new WebClient())
                {
                    Filestep++;
                    var filetype = a.Attachment.Split(Convert.ToChar("."));
                    var output = Path.Combine(Path.GetTempPath(), "Job Pack Generator\\");
                    client.DownloadFile(a.AttachmentUrl.Url, output + Filestep + "." + a.Label + "." + filetype.Last());
                    MessageBox.Show(@"Added", @"Document Added");
                    JobDocuments_ListBox.Items.Add(Filestep + "." + a.Label);
                }
            }
        }
    }
}