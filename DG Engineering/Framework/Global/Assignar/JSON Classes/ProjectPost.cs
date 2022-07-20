using System;
using Newtonsoft.Json;

namespace DG_Engineering.Framework.Global.Assignar.ProjectPost
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class ProjectPost
    {
        public class Data
        {
            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("active")]
            public string Active { get; set; }

            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("client_id")]
            public int ClientId { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("modified_by")]
            public string ModifiedBy { get; set; }

            [JsonProperty("modified_time")]
            public DateTime ModifiedTime { get; set; }
        }

        public class Root
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("data")]
            public Data Data { get; set; }
        }
    }
}
