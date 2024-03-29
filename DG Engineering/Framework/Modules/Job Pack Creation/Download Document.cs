﻿using System;
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
        private async void DownloadDocumentFromUrl(string url)
        {
             await AssignarAPIConnect(url, Method.Get, null);
            var docsearch = Static.AssignarResponseContent;
            var project = JsonConvert.DeserializeObject<Documents.Root>(docsearch);
            if (project == null) return;
            foreach (var a in project.Data.Where(a => a.Label == JobDocuments_ComboBox.Text))
            {
                using (var client = new WebClient())
                {
                    _filestep++;
                    var filetype = a.Attachment.Split(Convert.ToChar("."));
                    var output = Path.Combine(Path.GetTempPath(), "Job Pack Generator\\");
                    client.DownloadFile(a.AttachmentUrl.Url, output + _filestep + "." + a.Label + "." + filetype.Last());
                    MessageBox.Show(@"Added", @"Document Added");
                    JobDocuments_ListBox.Items.Add(_filestep + "." + a.Label);
                }
            }
        }
    }
}