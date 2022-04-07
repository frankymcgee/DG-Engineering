using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using DG_Engineering.Framework.Global.SimPro;
using Newtonsoft.Json;
using RestSharp;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Search SimPro for Quote/Job from Number.
        /// </summary>
        public void SimProSearch()
        {
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
            CompanyIdExtract(SimProClient_TextBox.Text);
        }

        
    }
}