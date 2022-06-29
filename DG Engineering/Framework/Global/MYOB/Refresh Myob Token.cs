using DG_Engineering.Framework.Global.MYOB;
using Newtonsoft.Json;
using RestSharp;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        public static void RefreshMyob()
        {
            var client = new RestClient("https://secure.myob.com/oauth2/v1/authorize/")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("client_id", Static.MyobClientID);
            request.AddParameter("client_secret", Static.MyobSecretKey);
            request.AddParameter("grant_type", "refresh_token");
            request.AddParameter("refresh_token", Static.refreshtoken);
            var response = client.Execute(request);
            var accesstoken = JsonConvert.DeserializeObject<RefreshTokenJson.Root>(response.Content);
            Static.AccessToken = accesstoken?.AccessToken;
        }
    }
}