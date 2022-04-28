using System;
using System.Collections.Generic;
using System.IO;
using DG_Engineering.Framework.Global.SimPro;
using Newtonsoft.Json;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Search SimPro for Quote/Job from Number.
        /// </summary>
        public void SimProSearch()
        {
            Directory.CreateDirectory(Output + "Files");
            var di = new DirectoryInfo(Output + "Files");
            var files = di.GetFiles();
            foreach (var file in files)
            {
                file.Delete();
            }
            MessageLabel.Visible = true;
            MessageLabel.Text = @"Searching";
            switch (QuoteJobSelection.Text)
            {
                case @"Quote":
                {
                    var quotes = SimProConnect(SimProUrl + "quotes/" + SimProQuoteText.Text).Content;
                    var quoteinfo = JsonConvert.DeserializeObject<Quotes.Root>(quotes);
                    if (quoteinfo == null) return;
                    SimProClient_TextBox.Text = quoteinfo.Customer.CompanyName;
                    ProjectNameTextBox.Text = quoteinfo.Name;
                    ProjectAddress_TextBox.Text = quoteinfo.Site.Name;
                    ProjectPOTextBox.Text = quoteinfo.OrderNo;
                    break;
                }
                case @"Job":
                {
                    var jobs = SimProConnect(SimProUrl + "jobs/" + SimProQuoteText.Text).Content;
                    var jobinfo = JsonConvert.DeserializeObject<Jobs.Root>(jobs);
                    if (jobinfo == null) return;
                    SimProClient_TextBox.Text = jobinfo.Customer.CompanyName;
                    ProjectNameTextBox.Text = jobinfo.Name;
                    ProjectAddress_TextBox.Text = jobinfo.Site.Name;
                    ProjectPOTextBox.Text = jobinfo.OrderNo;
                    break;
                }
            }

            MessageLabel.Text = @"Downloading Documents";
            // Download Documents
            var documents = SimProConnect(SimProUrl + "jobs/" + SimProQuoteText.Text + "/attachments/files/").Content;
            var result = JsonConvert.DeserializeObject<List<Documents.Root>>(documents);
            string docbyte64Search = null;
            foreach (var a in result)
            {
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

            MessageLabel.Visible = false;
            CompanyIdExtract(SimProClient_TextBox.Text);
        }
    }
}