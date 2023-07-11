using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;

namespace DG_Engineering
{
    public partial class MainWindow
    {
       /// <summary>
       /// Assignar API Connection and Utilisation
       /// </summary>
       /// <param name="url">The API Endpoint you want to Connect to: i.e.: /clients</param>
       /// <param name="jwtToken">DEPRECATED: can be null</param>
       /// <param name="method">The Request Type: i.e: Method.Get</param>
       /// <param name="body">If using Method.Post, a body MUST be used</param>
       /// <returns>The Content of the response in a JSON Format.</returns>
        private static async Task AssignarAPIConnect(string url, Method method, string body)
        {
            var options = new RestClientOptions(Static.AssignarDashboardUrl)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest(url, method);
            if (method != Method.Post)
            {                
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Authorization", "Bearer " + Static.JwtToken);
            }            
            else
            {
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Static.JwtToken);
                request.AddStringBody(body, DataFormat.Json);
            }
            try
            {
                RestResponse response = await client.ExecuteAsync(request);
                Static.AssignarResponseStatusCode = response.StatusCode.ToString();
                Static.AssignarResponseDescription = response.StatusDescription;
                Static.AssignarResponseContent = response.Content;
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Whoops! An Error has occurred. The error is as follows:" + @"

" + ex, @"Error");
            }
           
        }
    }
}