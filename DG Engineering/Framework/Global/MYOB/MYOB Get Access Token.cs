using System.Web;
using DG_Engineering.Framework.Global.MYOB;
using Newtonsoft.Json;
using RestSharp;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        public static void MyobGetAccessToken()
        {
            var client = new RestClient("https://secure.myob.com/oauth2/v1/authorize/")
            {
                Timeout = -1
            };
            
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("client_id", "43112a28-1d90-4a9e-97a2-0c5f40f25aef");
            request.AddParameter("client_secret", "oOO87NlUi6aYxB35VkqeG8om");
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("code", HttpUtility.UrlDecode(Static.UrlCoded) ?? string.Empty);
            request.AddParameter("redirect_uri", "https://dgengineering.com.au/authenticated/");
            var response = client.Execute(request);
            var requestjson = JsonConvert.DeserializeObject<RefreshTokenJson.Root>(response.Content);
            Static.RefreshToken = requestjson?.RefreshToken;
            RefreshMyob();
        }
    }
}