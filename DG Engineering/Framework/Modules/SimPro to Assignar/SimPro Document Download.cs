using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using DG_Engineering.Framework.Global.Assignar;
using DG_Engineering.Framework.Global.SimPro;
using Newtonsoft.Json;
using RestSharp;
using Documents = DG_Engineering.Framework.Global.SimPro.Documents;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        private Task SimProDocDownload()
        {
            StatusLabel.Visible = true;
            StatusLabel.Text = @"Downloading Documents";
            // Download Documents
            var documents = SimProConnect(SimProUrl + "jobs/" + SimProQuoteText.Text + "/attachments/files/").Content;
            var result = JsonConvert.DeserializeObject<List<Documents.Root>>(documents);
            ProgressBar.PerformStep();
            string docbyte64Search = null;
            foreach (var a in result)
            {
                StatusLabel.Text = @"Downloading " + a.Filename;
                switch (QuoteJobSelection.Text)
                {
                    case @"Quote":
                        docbyte64Search =
                            SimProConnect(SimProUrl + "quotes/" + SimProQuoteText.Text + "/attachments/files/" + a.ID + "?display=Base64").Content;
                        break;

                    case @"Job":
                        docbyte64Search =
                            SimProConnect(SimProUrl + "jobs/" + SimProQuoteText.Text + "/attachments/files/" + a.ID + "?display=Base64").Content;
                        break;
                }
                if (docbyte64Search == null) continue;
                var docresult = JsonConvert.DeserializeObject<DocumentBase64.Root>(docbyte64Search);
                var filename = docresult.Filename;
                File.WriteAllBytes(Output + "Files/" + filename, Convert.FromBase64String(docresult.Base64Data));
            }

            StatusLabel.Visible = false;
            Process.Start(Output + "Files/");
            return Task.CompletedTask;
        }
    }
}