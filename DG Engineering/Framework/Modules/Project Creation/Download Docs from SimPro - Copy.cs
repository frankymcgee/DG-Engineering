using DG_Engineering.Framework.Global.SimPro;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Will download Document from SimPRO and upload them to the project.
        /// </summary>
        public async void DownloadDocument()
        {
            if (!string.IsNullOrEmpty(QuoteNo_TextBox.Text))
            {
                await SimProConnect("/quotes/ " + QuoteNo_TextBox.Text + "/attachments/files/", Method.Get, null);
                var fileList = JsonConvert.DeserializeObject<List<SPDocumentList.Root>>(Static.SimProResponseContent);
                foreach (var file in fileList)
                {
                    await SimProConnect("/quotes/ " + QuoteNo_TextBox.Text + "/attachments/files/" + file.ID + "?display=Base64", Method.Get, null);
                    var jsonObject = JsonConvert.DeserializeObject<SPDocuments.Root>(Static.SimProResponseContent).Base64Data;
                    string modifiedname = "";
                    foreach (char c in ProjectName.Text)
                    {
                        if (c == '\\' || c == '/')
                        {
                            modifiedname += "-";
                        }
                        else
                        {
                            modifiedname += c;
                        }
                    }
                    File.WriteAllBytes(Path.Combine(DGEngineering, "DG Engineering HUB - Operations", "Jobs", ProjectJobNumber.Text + " - " + modifiedname, "SimPro Extracted Documents", file.Filename), Convert.FromBase64String(jsonObject));
                }
            }
        }
    }
}
