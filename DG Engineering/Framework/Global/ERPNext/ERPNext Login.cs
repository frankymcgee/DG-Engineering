using System;
using System.Threading.Tasks;
using RestSharp;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        private async Task ERPNextLogin()
        {
            var options = new RestClientOptions("https://dgengineering.com.au")
            {
                MaxTimeout = -1,
            };
            Static.ERPNext = new RestClient(options);
            var request = new RestRequest("/api/method/login?usr=Administrator&pwd=FUYS5oYo9hnNd6criyhF", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("b544d47f25a916a", "079d58b09f6cd01");
            RestResponse response = await Static.ERPNext.ExecuteAsync(request);
            Static.Cookie = response.Cookies[0].Value;

        }
    }

}