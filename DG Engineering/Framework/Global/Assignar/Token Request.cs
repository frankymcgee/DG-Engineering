using System;
using System.Net;
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
        private static HttpStatusCode Request(string clientId, string usernameLabel, string password)
        {
            var client = new RestClient(Static.AssignarAuthUrl)
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = "{ \"tennant_id\": " + clientId + ", \"username\": \"" + usernameLabel +
                       "\", \"password\": \"" + password + "\", \"user_type\": \"dashboard\" }";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK) return response.StatusCode;
            var jwtlogin = JsonConvert.DeserializeObject<Login.Root>(response.Content);
            if (jwtlogin != null) Static.JwtToken = jwtlogin.Data.Token;
            Console.WriteLine(response.Content);
            return response.StatusCode;
        }
    }
}