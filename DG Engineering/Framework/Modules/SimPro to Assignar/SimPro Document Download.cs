using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using DG_Engineering.Framework.Global.Assignar;
using DG_Engineering.Framework.Global.SimPro;
using Newtonsoft.Json;
using RestSharp;
using Documents = DG_Engineering.Framework.Global.SimPro.Documents;

// ReSharper disable once CheckNamespace
namespace DG_Engineering
{
    public partial class MainWindow
    {
        private void SimProDocDownload()
        {
            StatusLabel.Visible = true;
            StatusLabel.Text = @"Downloading Documents";
            // Download Documents
            var documents = SimProConnect(SimProUrl + "jobs/" + ProjectJobNumber.Text + "/attachments/files/").Content;
            var result = JsonConvert.DeserializeObject<List<Documents.Root>>(documents);
            ProgressBar.PerformStep();
            string docbyte64Search;
            if (result != null)
                foreach (var a in result)
                {
                    StatusLabel.Text = @"Downloading " + a.Filename;
                    docbyte64Search = SimProConnect(SimProUrl + "jobs/" + ProjectJobNumber.Text + "/attachments/files/" + a.ID + "?display=Base64").Content;
                    var docresult = JsonConvert.DeserializeObject<DocumentBase64.Root>(docbyte64Search);
                    var filename = docresult?.Filename;
                    File.WriteAllBytes(Output + "Files/" + filename, Convert.FromBase64String(docresult?.Base64Data ?? string.Empty));
                    StatusLabel.Text = @"Uploading " + docresult?.Filename + @" to the Project.";
                    AssignarDocUploadPost(Path.Combine(Output, "files", filename ?? string.Empty), filename);
                }

            StatusLabel.Visible = false;
        }       

        private void AssignarDocUploadPost(string filename,string file)
        {
            var filerequest = "{\r\n  \"filename\": \"" + file + "\"\r\n}";
            var request = AssignarConnect(Static.AssignarDashboardUrl + "upload-urls/project-document", Static.JwtToken,Method.POST,filerequest);
            var response = JsonConvert.DeserializeObject<ProjectDocument.Root>(request);
            var uploadpath = response?.Path;
            var bytes = File.ReadAllBytes(filename);
            var wc = new WebClient();
            wc.UploadData(response?.Url ?? string.Empty, "PUT", bytes);
            //AssignarPut(uploadurl,filename);
            var body = @"{
" + "\n" +
                       @"""document_id"": 10,
" + "\n" +
                       @"""label"": """ + file +  @""",
" + "\n" +
                       @"""comments"": ""Uploaded from SimPro"",
" + "\n" +
                       @"""active"": true,
" + "\n" +
                       @"""attachment"": """ + uploadpath + @"""
" + "\n" +
                       @"}";
            var projectrequest = AssignarConnect(Static.AssignarDashboardUrl + "projects?external_id=" + ProjectJobNumber.Text, Static.JwtToken, Method.GET, null);
            var projectresponse = JsonConvert.DeserializeObject<ProjectSearch.Root>(projectrequest);
            Debug.Assert(projectresponse?.Data != null, "projectresponse?.Data != null");
            foreach (var a in projectresponse.Data)
            {
                ProjectId = a.Id;
            }
            AssignarConnect(Static.AssignarDashboardUrl + "projects/" + ProjectId + "/documents", Static.JwtToken, Method.POST, body);
        }
    }
}