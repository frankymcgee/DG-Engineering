using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DG_Engineering.Framework.Global.Assignar
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class OrderResp
    {
        public class Data
    {
        [JsonProperty("date_created")]
        public DateTime Date_created { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("job_number")]
        public string Job_number { get; set; }

        [JsonProperty("po_number")]
        public string Po_number { get; set; }

        [JsonProperty("client_id")]
        public int Client_id { get; set; }

        [JsonProperty("order_owner")]
        public int Order_owner { get; set; }

        [JsonProperty("project_id")]
        public int Project_id { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("job_description")]
        public string Job_description { get; set; }

        [JsonProperty("start_time")]
        public string Start_time { get; set; }

        [JsonProperty("shift_duration")]
        public string Shift_duration { get; set; }

        [JsonProperty("start_date")]
        public string Start_date { get; set; }

        [JsonProperty("end_date")]
        public string End_date { get; set; }

        [JsonProperty("comments")]
        public string Comments { get; set; }

        [JsonProperty("status_id")]
        public int Status_id { get; set; }

        [JsonProperty("type_id")]
        public int Type_id { get; set; }

        [JsonProperty("supplier_id")]
        public object Supplier_id { get; set; }

        [JsonProperty("modified_by")]
        public string Modified_by { get; set; }

        [JsonProperty("modified_time")]
        public DateTime Modified_time { get; set; }
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
