using Newtonsoft.Json;

namespace DG_Engineering.Framework.Global.Assignar
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class Login
    {
        // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
        public class LanguageBundle
        {
            [JsonProperty("order")]
            public string Order { get; set; }

            [JsonProperty("orders")]
            public string Orders { get; set; }

            [JsonProperty("competencies")]
            public string Competencies { get; set; }

            [JsonProperty("competency")]
            public string Competency { get; set; }

            [JsonProperty("task")]
            public string Task { get; set; }

            [JsonProperty("tasks")]
            public string Tasks { get; set; }

            [JsonProperty("supplier")]
            public string Supplier { get; set; }

            [JsonProperty("suppliers")]
            public string Suppliers { get; set; }
        }
        
        public class Subscription
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("expiration")]
            public object Expiration { get; set; }
        }
        
        public class Data
        {
            [JsonProperty("language_bundle")]
            public LanguageBundle LanguageBundle { get; set; }

            [JsonProperty("settings")]
            public string Settings { get; set; }

            [JsonProperty("subscription")]
            public Subscription Subscription { get; set; }

            [JsonProperty("token")]
            public string Token { get; set; }

            [JsonProperty("user")]
            public string User { get; set; }
        }

        public class Root
        {
            [JsonProperty("code")]
            public int Code { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("data")]
            public Data Data { get; set; }
        }


    }
}
