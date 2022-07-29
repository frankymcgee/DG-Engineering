using System;
using System.Net;
using System.Threading.Tasks;
using DG_Engineering.Framework.Global.Assignar;
using Newtonsoft.Json;
using RestSharp;

namespace DG_Engineering
{
    public partial class LoginWindow
    {
        /// <summary>
        /// Will Login to Assignar Using the Username and Password
        /// </summary>
        /// <param name="clientId">Dashboard Client ID</param>
        /// <param name="usernameLabel">Dashboard User Username</param>
        /// <param name="password">Dashboard User Password</param>
        private static async Task<HttpStatusCode> Request(string clientId, string usernameLabel, string password)
        {
            var client = new RestClient(Static.AssignarAuthUrl)
            {
                Timeout = -1
            };
            var request = new RestRequest(Static.AssignarAuthUrl,Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = "{ \"tennant_id\": " + clientId + ", \"username\": \"" + usernameLabel +
                       "\", \"password\": \"" + password + "\", \"user_type\": \"dashboard\" }";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode != HttpStatusCode.OK) return response.StatusCode;
            var jwtlogin = JsonConvert.DeserializeObject<Login.Root>(response.Content);
            if (jwtlogin != null) Static.JwtToken = jwtlogin.Data.Token;
            return response.StatusCode;
        }
    }
}