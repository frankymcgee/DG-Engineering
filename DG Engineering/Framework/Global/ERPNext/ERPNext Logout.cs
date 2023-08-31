using System;
using System.Threading.Tasks;
using RestSharp;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        private async Task ERPNextLogout(string cookie, RestClient client)
        {
            var request = new RestRequest("/api/method/logout", Method.Get);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("b544d47f25a916a", "079d58b09f6cd01");
            request.AddHeader("Cookie", "full_name=Administrator; sid=" + cookie + "; system_user=yes; user_id=Administrator;");
            RestResponse response = await Static.ERPNext.ExecuteAsync(request);
            Console.WriteLine(response.Content);
        }
    }

}