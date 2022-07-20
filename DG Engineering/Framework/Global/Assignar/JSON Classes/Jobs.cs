﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace DG_Engineering.Framework.Global.Assignar
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class Jobs
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Geolocation
    {
        [JsonProperty("type")]
        // ReSharper disable once MemberHidesStaticFromOuterClass
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public List<double> Coordinates { get; set; }
    }

    public class Geolocation2
    {
        [JsonProperty("type")]
        // ReSharper disable once MemberHidesStaticFromOuterClass
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public List<double> Coordinates { get; set; }
    }

    public class Client
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Project
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Status
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Type
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public class Datum
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("job_number")]
        public object JobNumber { get; set; }

        [JsonProperty("po_number")]
        public string PoNumber { get; set; }

        [JsonProperty("client_id")]
        public int ClientId { get; set; }

        [JsonProperty("order_owner")]
        public int OrderOwner { get; set; }

        [JsonProperty("project_id")]
        public int ProjectId { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("location2")]
        public string Location2 { get; set; }

        [JsonProperty("job_description")]
        public string JobDescription { get; set; }

        [JsonProperty("start_time")]
        public string StartTime { get; set; }

        [JsonProperty("shift_duration")]
        public string ShiftDuration { get; set; }

        [JsonProperty("start_date")]
        public string StartDate { get; set; }

        [JsonProperty("end_date")]
        public string EndDate { get; set; }

        [JsonProperty("comments")]
        public string Comments { get; set; }

        [JsonProperty("date_created")]
        public string DateCreated { get; set; }

        [JsonProperty("created_by_id")]
        public int? CreatedById { get; set; }

        [JsonProperty("modified_time")]
        public string ModifiedTime { get; set; }

        [JsonProperty("modified_by")]
        public string ModifiedBy { get; set; }

        [JsonProperty("modified_by_id")]
        public int? ModifiedById { get; set; }

        [JsonProperty("status_id")]
        public int StatusId { get; set; }

        [JsonProperty("type_id")]
        public int TypeId { get; set; }

        [JsonProperty("supplier_id")]
        public object SupplierId { get; set; }

        [JsonProperty("geolocation")]
        public Geolocation Geolocation { get; set; }

        [JsonProperty("geolocation2")]
        public Geolocation2 Geolocation2 { get; set; }

        [JsonProperty("client")]
        public Client Client { get; set; }

        [JsonProperty("project")]
        public Project Project { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("type")]
        public Type Type { get; set; }

        [JsonProperty("attachments")]
        public List<object> Attachments { get; set; }

        [JsonProperty("tags")]
        public List<object> Tags { get; set; }
    }

    public class Root
    {
        [JsonProperty("status")]
        // ReSharper disable once MemberHidesStaticFromOuterClass
        public int Status { get; set; }

        [JsonProperty("data")]
        public List<Datum> Data { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
    }
}
