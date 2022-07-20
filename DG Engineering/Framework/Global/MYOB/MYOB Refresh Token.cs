﻿using DG_Engineering.Framework.Global.MYOB;
using Newtonsoft.Json;
using RestSharp;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        private static void RefreshMyob()
        {
            var client = new RestClient("https://secure.myob.com/oauth2/v1/authorize/")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("client_id", Static.MyobClientId);
            request.AddParameter("client_secret", Static.MyobSecretKey);
            request.AddParameter("grant_type", "refresh_token");
            request.AddParameter("refresh_token", Static.RefreshToken);
            var response = client.Execute(request);
            var accesstoken = JsonConvert.DeserializeObject<RefreshTokenJson.Root>(response.Content);
            Static.AccessToken = accesstoken?.AccessToken;
            Static.RefreshToken = accesstoken?.RefreshToken;
            MyobRetrieveCompanyList();
        }

        private static void MyobRetrieveCompanyList()
        {
            var client = new RestClient("https://api.myob.com/accountright")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-myobapi-key", "43112a28-1d90-4a9e-97a2-0c5f40f25aef");
            request.AddHeader("x-myobapi-version", "v2");
            request.AddHeader("Accept-Encoding", "gzip,deflate");
            request.AddHeader("Authorization", "Bearer " + Static.AccessToken);
            client.Execute(request);
        }
    }
}