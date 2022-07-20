using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DG_Engineering.Framework.Global.Assignar
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class Documents
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Project
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Document
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class AttachmentUrl
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("expiry")]
        public long Expiry { get; set; }
    }

    public class Datum
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("project_id")]
        public int ProjectId { get; set; }

        [JsonProperty("document_id")]
        public int DocumentId { get; set; }

        [JsonProperty("external_id")]
        public object ExternalId { get; set; }

        [JsonProperty("attachment")]
        public string Attachment { get; set; }

        [JsonProperty("created_time")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("modified_time")]
        public DateTime ModifiedTime { get; set; }

        [JsonProperty("expiry_date")]
        public object ExpiryDate { get; set; }

        [JsonProperty("expiry_date_alert")]
        public object ExpiryDateAlert { get; set; }

        [JsonProperty("expiry_number")]
        public object ExpiryNumber { get; set; }

        [JsonProperty("expiry_number_alert")]
        public object ExpiryNumberAlert { get; set; }

        [JsonProperty("comments")]
        public string Comments { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }

        [JsonProperty("project")]
        public Project Project { get; set; }

        [JsonProperty("document")]
        public Document Document { get; set; }

        [JsonProperty("attachment_url")]
        public AttachmentUrl AttachmentUrl { get; set; }
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
