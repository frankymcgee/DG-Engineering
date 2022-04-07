using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DG_Engineering.Framework.Global.Assignar
{
    internal class Roles
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Datum
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("external_id")]
            public object ExternalId { get; set; }

            [JsonProperty("active")]
            public bool Active { get; set; }

            [JsonProperty("modified_by")]
            public string ModifiedBy { get; set; }

            [JsonProperty("modified_time")]
            public DateTime ModifiedTime { get; set; }

            [JsonProperty("req_machines")]
            public bool ReqMachines { get; set; }

            [JsonProperty("pay_level")]
            public object PayLevel { get; set; }

            [JsonProperty("payroll_acc")]
            public object PayrollAcc { get; set; }

            [JsonProperty("charge_code_id")]
            public object ChargeCodeId { get; set; }

            [JsonProperty("supervisor")]
            public bool Supervisor { get; set; }

            [JsonProperty("deleted_at")]
            public object DeletedAt { get; set; }

            [JsonProperty("deleted_by_id")]
            public object DeletedById { get; set; }
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
