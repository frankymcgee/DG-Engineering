using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DG_Engineering.Framework.Global.Assignar.JSON_Classes
{
    internal class Timesheets
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Activity1
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("timesheet_id")]
            public int Timesheet_id { get; set; }

            [JsonProperty("activity_id")]
            public int Activity_id { get; set; }

            [JsonProperty("start_time")]
            public string Start_time { get; set; }

            [JsonProperty("end_time")]
            public string End_time { get; set; }

            [JsonProperty("activity")]
            public Activity1 Activity { get; set; }
        }

        public class Activity2
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("external_id")]
            public object External_id { get; set; }
        }

        public class Asset
        {
            [JsonProperty("asset_id")]
            public int Asset_id { get; set; }
        }

        public class Asset2
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("external_id")]
            public object External_id { get; set; }
        }

        public class Client
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("external_id")]
            public string External_id { get; set; }
        }

        public class Datum
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("client_id")]
            public int Client_id { get; set; }

            [JsonProperty("user_id")]
            public int User_id { get; set; }

            [JsonProperty("project_id")]
            public int Project_id { get; set; }

            [JsonProperty("task_id")]
            public int Task_id { get; set; }

            [JsonProperty("location")]
            public string Location { get; set; }

            [JsonProperty("order_id")]
            public int Order_id { get; set; }

            [JsonProperty("supplier_id")]
            public object Supplier_id { get; set; }

            [JsonProperty("job_number")]
            public string Job_number { get; set; }

            [JsonProperty("start_time")]
            public string Start_time { get; set; }

            [JsonProperty("start_datetime")]
            public object Start_datetime { get; set; }

            [JsonProperty("end_time")]
            public string End_time { get; set; }

            [JsonProperty("end_datetime")]
            public object End_datetime { get; set; }

            [JsonProperty("break_time")]
            public int Break_time { get; set; }

            [JsonProperty("travel_time_to")]
            public string Travel_time_to { get; set; }

            [JsonProperty("travel_time_from")]
            public string Travel_time_from { get; set; }

            [JsonProperty("total_time")]
            public string Total_time { get; set; }

            [JsonProperty("comments")]
            public string Comments { get; set; }

            [JsonProperty("office_use")]
            public object Office_use { get; set; }

            [JsonProperty("submitted_date")]
            public string Submitted_date { get; set; }

            [JsonProperty("date_of_creation")]
            public DateTime Date_of_creation { get; set; }

            [JsonProperty("modified_time")]
            public DateTime Modified_time { get; set; }

            [JsonProperty("modified_by")]
            public string Modified_by { get; set; }

            [JsonProperty("submitted_by")]
            public object Submitted_by { get; set; }

            [JsonProperty("checked")]
            public bool Checked { get; set; }

            [JsonProperty("locked")]
            public bool Locked { get; set; }

            [JsonProperty("latitude")]
            public string Latitude { get; set; }

            [JsonProperty("longitude")]
            public string Longitude { get; set; }

            [JsonProperty("latitude2")]
            public object Latitude2 { get; set; }

            [JsonProperty("longitude2")]
            public object Longitude2 { get; set; }

            [JsonProperty("signature_name")]
            public string Signature_name { get; set; }

            [JsonProperty("signature")]
            public string Signature { get; set; }

            [JsonProperty("deleted_time")]
            public object Deleted_time { get; set; }

            [JsonProperty("client")]
            public Client Client { get; set; }

            [JsonProperty("project")]
            public Project Project { get; set; }

            [JsonProperty("task")]
            public Task Task { get; set; }

            [JsonProperty("user")]
            public User User { get; set; }

            [JsonProperty("digitaldocket")]
            public Digitaldocket Digitaldocket { get; set; }

            [JsonProperty("supplier")]
            public Supplier Supplier { get; set; }

            [JsonProperty("assets")]
            public List<Asset> Assets { get; set; }

            [JsonProperty("pay_items")]
            public List<object> Pay_items { get; set; }

            [JsonProperty("activities")]
            public List<Activity1> Activities { get; set; }

            [JsonProperty("allowances")]
            public List<object> Allowances { get; set; }

            [JsonProperty("docket")]
            public object Docket { get; set; }

            [JsonProperty("asset")]
            public Asset Asset { get; set; }

            [JsonProperty("signature_url")]
            public SignatureUrl Signature_url { get; set; }
        }

        public class Digitaldocket
        {
            [JsonProperty("digital_docket_id")]
            public object Digital_docket_id { get; set; }
        }

        public class Project
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("external_id")]
            public string External_id { get; set; }
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

        public class SignatureUrl
        {
            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("expiry")]
            public object Expiry { get; set; }
        }

        public class Supplier
        {
            [JsonProperty("name")]
            public object Name { get; set; }

            [JsonProperty("external_id")]
            public object External_id { get; set; }
        }

        public class Task
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("external_id")]
            public object External_id { get; set; }
        }

        public class User
        {
            [JsonProperty("full_name")]
            public string Full_name { get; set; }

            [JsonProperty("username")]
            public string Username { get; set; }

            [JsonProperty("first_name")]
            public string First_name { get; set; }

            [JsonProperty("last_name")]
            public string Last_name { get; set; }

            [JsonProperty("employee_id")]
            public string Employee_id { get; set; }
        }


    }
}
