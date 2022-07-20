using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DG_Engineering.Framework.Global.Assignar
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class Projectinfo
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Client
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("external_id")]
            public object ExternalId { get; set; }
        }

        public class Datum
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("client_id")]
            public int ClientId { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("start_date")]
            public string StartDate { get; set; }

            [JsonProperty("end_date")]
            public string EndDate { get; set; }

            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("address")]
            public object Address { get; set; }

            [JsonProperty("address_geo")]
            public object AddressGeo { get; set; }

            [JsonProperty("suburb")]
            public object Suburb { get; set; }

            [JsonProperty("state")]
            public object State { get; set; }

            [JsonProperty("postcode")]
            public object Postcode { get; set; }

            [JsonProperty("external_id")]
            public string ExternalId { get; set; }

            [JsonProperty("active")]
            public bool Active { get; set; }

            [JsonProperty("parent_id")]
            public object ParentId { get; set; }

            [JsonProperty("root_path")]
            public object RootPath { get; set; }

            [JsonProperty("modified_by")]
            public string ModifiedBy { get; set; }

            [JsonProperty("modified_time")]
            public DateTime ModifiedTime { get; set; }

            [JsonProperty("deleted_at")]
            public object DeletedAt { get; set; }

            [JsonProperty("deleted_by_id")]
            public object DeletedById { get; set; }

            [JsonProperty("client")]
            public Client Client { get; set; }

            [JsonProperty("parent")]
            public object Parent { get; set; }

            [JsonProperty("sub_projects")]
            public List<object> SubProjects { get; set; }

            [JsonProperty("tags")]
            public List<Tag> Tags { get; set; }

            [JsonProperty("supervisors")]
            public List<object> Supervisors { get; set; }
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

        public class Tag
        {
            [JsonProperty("tag_id")]
            public int TagId { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("color")]
            public string Color { get; set; }
        }


    }
}
