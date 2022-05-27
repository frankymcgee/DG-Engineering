using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DG_Engineering.Framework.Global.Assignar
{
    internal class Contacts
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Datum
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("first_name")]
            public string FirstName { get; set; }

            [JsonProperty("last_name")]
            public string LastName { get; set; }

            [JsonProperty("company")]
            public string Company { get; set; }

            [JsonProperty("office_phone")]
            public string OfficePhone { get; set; }

            [JsonProperty("mobile")]
            public string Mobile { get; set; }

            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("job_title")]
            public string JobTitle { get; set; }

            [JsonProperty("address_1")]
            public object Address1 { get; set; }

            [JsonProperty("address_2")]
            public object Address2 { get; set; }

            [JsonProperty("city")]
            public object City { get; set; }

            [JsonProperty("state")]
            public object State { get; set; }

            [JsonProperty("postcode")]
            public object Postcode { get; set; }

            [JsonProperty("comments")]
            public object Comments { get; set; }

            [JsonProperty("external_id")]
            public object ExternalId { get; set; }

            [JsonProperty("created_at")]
            public DateTime CreatedAt { get; set; }

            [JsonProperty("modified_at")]
            public DateTime ModifiedAt { get; set; }

            [JsonProperty("tags")]
            public List<object> Tags { get; set; }
        }

        public class Root
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("data")]
            public List<Datum> Data { get; set; }

            [JsonProperty("count")]
            public int Count { get; set; }
        }


    }
}
