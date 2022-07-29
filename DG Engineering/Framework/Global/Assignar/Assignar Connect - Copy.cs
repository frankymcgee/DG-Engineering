using System;
using System.Threading.Tasks;
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
        private static async Task<string> AssignarConnect(string url, string jwtToken, Method method, string body)
        {
            var options = new RestClientOptions(url)
            {
                MaxTimeout = -1
            };
            if (method != Method.Post)
            {
                var client = new RestClient(options);
                var request = new RestRequest(url, method)
                    .AddQueryParameter("Authorization", "Bearer " + jwtToken);
                var response = await client.PostAsync(request);
                Console.WriteLine(response.Content);
                return response.Content;
            }
            else
            {
                var client = new RestClient(options);
                var request = new RestRequest(url, method)
                    .AddJsonBody(body)
                    .AddHeader("Authorization", "Bearer " + jwtToken);
                var response = await client.GetAsync(request);
                Console.WriteLine(response.Content);
                return response.Content;
            }
        }
    }
}