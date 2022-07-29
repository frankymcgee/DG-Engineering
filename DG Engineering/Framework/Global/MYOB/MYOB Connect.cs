using System.Net;
using RestSharp;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        private static RestResponse MyobConnect(string url, Method method)
        {
            while (true)
            {
                var client = new RestClient(url);
                var request = new RestRequest(url,method);
                request.AddHeader("x-myobapi-key", Static.MyobClientId);
                request.AddHeader("x-myobapi-version", "v2");
                request.AddHeader("Accept-Encoding", "gzip,deflate");
                request.AddHeader("Authorization", "Bearer " + Static.AccessToken);
                if (client.Execute(request).StatusCode == HttpStatusCode.Forbidden)
                {
                    MyobGetAccessToken();
                }
                else
                {
                    return (RestResponse) client.Execute(request);
                }
            }
        }
    }
}