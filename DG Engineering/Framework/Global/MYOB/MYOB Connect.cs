using System.Net;
using RestSharp;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        private static IRestResponse MyobConnect(string url, Method method)
        {
            while (true)
            {
                var client = new RestClient(url)
                {
                    Timeout = -1
                };
                var request = new RestRequest(method);
                request.AddHeader("x-myobapi-key", Static.MyobClientID);
                request.AddHeader("x-myobapi-version", "v2");
                request.AddHeader("Accept-Encoding", "gzip,deflate");
                request.AddHeader("Authorization", "Bearer " + Static.AccessToken);
                if (client.Execute(request).StatusCode == HttpStatusCode.Forbidden)
                {
                    RefreshMyob();
                }
                else
                {
                    return client.Execute(request);
                }
            }
        }
    }
}