using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DG_Engineering.Framework.Global.Assignar
{
    internal class OrderResp
    {
        public class DateCreated
        {
            [JsonProperty("val")]
            public string Val { get; set; }
        }

        public class Tag
        {
            [JsonProperty("tag_id")]
            public int TagId { get; set; }
        }

        public class Data
        {
            [JsonProperty("date_created")]
            public DateCreated DateCreated { get; set; }

            [JsonProperty("modified_by")]
            public string ModifiedBy { get; set; }

            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("active")]
            public bool Active { get; set; }

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

            [JsonProperty("status_id")]
            public int StatusId { get; set; }

            [JsonProperty("type_id")]
            public int TypeId { get; set; }

            [JsonProperty("supplier_id")]
            public object SupplierId { get; set; }

            [JsonProperty("tags")]
            public List<Tag> Tags { get; set; }
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
