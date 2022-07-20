using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// ReSharper disable once CheckNamespace
namespace DG_Engineering.Framework.Global.MYOB
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class Job
    {
       // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Item
    {
        [JsonProperty("UID")]
        public string Uid { get; set; }

        [JsonProperty("Number")]
        public string Number { get; set; }

        [JsonProperty("IsHeader")]
        public bool IsHeader { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("ParentJob")]
        public object ParentJob { get; set; }

        [JsonProperty("LinkedCustomer")]
        public LinkedCustomer LinkedCustomer { get; set; }

        [JsonProperty("PercentComplete")]
        public double PercentComplete { get; set; }

        [JsonProperty("StartDate")]
        public object StartDate { get; set; }

        [JsonProperty("FinishDate")]
        public object FinishDate { get; set; }

        [JsonProperty("Contact")]
        public string Contact { get; set; }

        [JsonProperty("Manager")]
        public object Manager { get; set; }

        [JsonProperty("IsActive")]
        public bool IsActive { get; set; }

        [JsonProperty("TrackReimbursables")]
        public bool TrackReimbursables { get; set; }

        [JsonProperty("LastModified")]
        public DateTime LastModified { get; set; }

        [JsonProperty("URI")]
        public string Uri { get; set; }

        [JsonProperty("RowVersion")]
        public string RowVersion { get; set; }
    }

    public class LinkedCustomer
    {
        [JsonProperty("UID")]
        public string Uid { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("DisplayID")]
        public string DisplayId { get; set; }

        [JsonProperty("URI")]
        public string Uri { get; set; }
    }

    public class Root
    {
        [JsonProperty("Items")]
        public List<Item> Items { get; set; }

        [JsonProperty("NextPageLink")]
        public object NextPageLink { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }
    }


    }

}
