using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DG_Engineering.Framework.Global.Assignar
{
   public class Login
    {
        // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
        public class LanguageBundle
        {
            [JsonPropertyName("order")]
            public string Order { get; set; }

            [JsonPropertyName("orders")]
            public string Orders { get; set; }

            [JsonPropertyName("competencies")]
            public string Competencies { get; set; }

            [JsonPropertyName("competency")]
            public string Competency { get; set; }

            [JsonPropertyName("task")]
            public string Task { get; set; }

            [JsonPropertyName("tasks")]
            public string Tasks { get; set; }

            [JsonPropertyName("supplier")]
            public string Supplier { get; set; }

            [JsonPropertyName("suppliers")]
            public string Suppliers { get; set; }
        }

        public class Subscription
        {
            [JsonPropertyName("type")]
            public string Type { get; set; }

            [JsonPropertyName("expiration")]
            public object Expiration { get; set; }
        }

        public class Data
        {
            [JsonPropertyName("language_bundle")]
            public LanguageBundle LanguageBundle { get; set; }

            [JsonPropertyName("settings")]
            public string Settings { get; set; }

            [JsonPropertyName("subscription")]
            public Subscription Subscription { get; set; }

            [JsonPropertyName("token")]
            public string Token { get; set; }

            [JsonPropertyName("user")]
            public string User { get; set; }
        }

        public class Root
        {
            [JsonPropertyName("code")]
            public int Code { get; set; }

            [JsonPropertyName("message")]
            public string Message { get; set; }

            [JsonPropertyName("data")]
            public Data Data { get; set; }
        }


    }
}
