using System;
using RestSharp;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Method to Connect to Assignar
        /// </summary>
        /// <param name="url">LoginForm.AssignarDashboardUrl plus any other required settings.</param>
        /// <param name="jwtToken">LoginForm.JwtToken</param>
        /// <param name="method">Method.GET</param>
        /// <param name="body">If POST, must not be null</param>
        /// <returns>Content of the Request.</returns>
        private static string AssignarConnect(string url, string jwtToken, Method method, string body)
        {
            if (method != Method.POST)
            {
                var client = new RestClient(url)
                {
                    Timeout = -1
                };
                var request = new RestRequest(method);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + jwtToken);
                var response = client.Execute(request);
                return response.Content;
            }
            else
            {
                var client = new RestClient(url)
                {
                    Timeout = -1
                };
                var request = new RestRequest(method);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + jwtToken);
                request.AddParameter("application/json", body,  ParameterType.RequestBody);
                var response = client.Execute(request);
                Console.WriteLine(response.Content);
                return response.Content;
            }
        }
    }
}