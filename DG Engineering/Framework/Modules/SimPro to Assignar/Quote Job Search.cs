using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
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
            StatusLabel.Visible = true;
            StatusLabel.Text = @"Searching";
            Directory.CreateDirectory(Output + "Files");
            var di = new DirectoryInfo(Output + "Files");
            var files = di.GetFiles();
            foreach (var file in files)
            {
                file.Delete();
            }
            ProgressBar.PerformStep();
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
                    ProjectPOTextBox.Text = string.IsNullOrEmpty(quoteinfo.OrderNo) ? @"MISSING PO NUMBER" : quoteinfo.OrderNo;
                    ProgressBar.PerformStep();
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
                    ProjectPOTextBox.Text = string.IsNullOrEmpty(jobinfo.OrderNo) ?@"MISSING PO NUMBER": jobinfo.OrderNo;
                    ProgressBar.PerformStep();
                    break;
                }
            }
            ProjectPOTextBox.Font = ProjectPOTextBox.Text == @"MISSING PO NUMBER" ? new Font(ProjectPOTextBox.Font, FontStyle.Bold) : new Font(ProjectPOTextBox.Font, FontStyle.Regular);
            CompanyIdExtract(SimProClient_TextBox.Text);
            ProgressBar.PerformStep();
            var documents = SimProConnect(SimProUrl + "jobs/" + SimProQuoteText.Text + "/attachments/files/").Content;
            var result = JsonConvert.DeserializeObject<List<Documents.Root>>(documents);
            StatusLabel.Text = @"Found " + result.Count + @" Documents.";
            ProgressBar.Value = 0;
        }
    }
}