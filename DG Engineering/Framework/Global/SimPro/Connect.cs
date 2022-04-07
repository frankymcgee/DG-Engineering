using RestSharp;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Connects to SimPro to retrieve relevant information
        /// </summary>
        /// <param name="url">URL for access.</param>
        /// <returns>IRestResponse</returns>
        private static IRestResponse SimProConnect(string url)
        {
            var client = new RestClient(url)
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer 73acb409ba2e09ff6485665e309d1be1f59490ee");
            return client.Execute(request);
        }
    }
}