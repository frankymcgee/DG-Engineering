using System;
using System.Net;
using System.Threading.Tasks;
using DG_Engineering.Framework.Global.Assignar;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace DG_Engineering
{
    public class AssignarAuthenticator : AuthenticatorBase
    {
        private readonly string _baseurl;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public AssignarAuthenticator(string baseulrl, string clientId, string clientSecret) : base("")
        {
            _baseurl = baseulrl;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
            var token = string.IsNullOrEmpty(Token) ? await AssignarRequest() : Token;
            return new HeaderParameter(KnownHeaders.Authorization, Token);
        }
        /// <summary>
        /// Will Login to Assignar Using the Username and Password
        /// </summary>
        /// <param name="clientId">Dashboard Client ID</param>
        /// <param name="usernameLabel">Dashboard User Username</param>
        /// <param name="password">Dashboard User Password</param>
        private static async Task<HttpStatusCode> AssignarRequest(string clientId, string usernameLabel, string password)
        {
            var options = new RestClientOptions(Static.AssignarAuthUrl)
            {
                MaxTimeout = -1
            };
            var client = new RestClient(options);
            var request = new RestRequest(Static.AssignarAuthUrl,Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = "{ \"tennant_id\": " + clientId + ", \"username\": \"" + usernameLabel +
                       "\", \"password\": \"" + password + "\", \"user_type\": \"dashboard\" }";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = await client.PostAsync(request);
            if (response.StatusCode != HttpStatusCode.OK) return response.StatusCode;
            var jwtlogin = JsonConvert.DeserializeObject<Login.Root>(response.Content ?? string.Empty);
            if (jwtlogin != null) Static.JwtToken = jwtlogin.Data.Token;
            return response.StatusCode;
        }
    }
}