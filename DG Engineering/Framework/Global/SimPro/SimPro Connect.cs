using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        public static async Task SimProConnect(string url, Method method, string body)
        {
            var options = new RestClientOptions(Static.SimProUrl)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest(url, method);
            if (method != Method.Post)
            {                
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Authorization", "Bearer " + Static.SimProToken);
            }            
            else
            {
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Static.SimProToken);
                request.AddStringBody(body, DataFormat.Json);
            }
            RestResponse response = await client.ExecuteAsync(request);
            Static.SimProResponseStatusCode = response.StatusCode.ToString();
            Static.SimProResponseDescription = response.StatusDescription;
            Static.SimProResponseContent = response.Content;            
        }
    }
}