using System.Collections.Generic;
using Newtonsoft.Json;

namespace DG_Engineering.Framework.Global.ERPNext
{
    internal class ERPNextEmployeeCreated
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("owner")]
            public string Owner { get; set; }

            [JsonProperty("creation")]
            public string Creation { get; set; }

            [JsonProperty("modified")]
            public string Modified { get; set; }

            [JsonProperty("modified_by")]
            public string Modified_by { get; set; }

            [JsonProperty("docstatus")]
            public int Docstatus { get; set; }

            [JsonProperty("idx")]
            public int Idx { get; set; }

            [JsonProperty("employee")]
            public string Employee { get; set; }

            [JsonProperty("naming_series")]
            public string Naming_series { get; set; }

            [JsonProperty("first_name")]
            public string First_name { get; set; }

            [JsonProperty("middle_name")]
            public object Middle_name { get; set; }

            [JsonProperty("last_name")]
            public string Last_name { get; set; }

            [JsonProperty("employee_name")]
            public string Employee_name { get; set; }

            [JsonProperty("gender")]
            public string Gender { get; set; }

            [JsonProperty("date_of_birth")]
            public string Date_of_birth { get; set; }

            [JsonProperty("salutation")]
            public object Salutation { get; set; }

            [JsonProperty("date_of_joining")]
            public string Date_of_joining { get; set; }

            [JsonProperty("image")]
            public object Image { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("user_id")]
            public object User_id { get; set; }

            [JsonProperty("create_user_permission")]
            public int Create_user_permission { get; set; }

            [JsonProperty("company")]
            public string Company { get; set; }

            [JsonProperty("department")]
            public string Department { get; set; }

            [JsonProperty("employment_type")]
            public string Employment_type { get; set; }

            [JsonProperty("employee_number")]
            public object Employee_number { get; set; }

            [JsonProperty("designation")]
            public string Designation { get; set; }

            [JsonProperty("reports_to")]
            public object Reports_to { get; set; }

            [JsonProperty("branch")]
            public object Branch { get; set; }

            [JsonProperty("grade")]
            public object Grade { get; set; }

            [JsonProperty("job_applicant")]
            public object Job_applicant { get; set; }

            [JsonProperty("scheduled_confirmation_date")]
            public object Scheduled_confirmation_date { get; set; }

            [JsonProperty("final_confirmation_date")]
            public object Final_confirmation_date { get; set; }

            [JsonProperty("contract_end_date")]
            public object Contract_end_date { get; set; }

            [JsonProperty("notice_number_of_days")]
            public int Notice_number_of_days { get; set; }

            [JsonProperty("date_of_retirement")]
            public object Date_of_retirement { get; set; }

            [JsonProperty("cell_number")]
            public string Cell_number { get; set; }

            [JsonProperty("personal_email")]
            public string Personal_email { get; set; }

            [JsonProperty("company_email")]
            public object Company_email { get; set; }

            [JsonProperty("prefered_contact_email")]
            public string Prefered_contact_email { get; set; }

            [JsonProperty("prefered_email")]
            public object Prefered_email { get; set; }

            [JsonProperty("unsubscribed")]
            public int Unsubscribed { get; set; }

            [JsonProperty("current_address")]
            public string Current_address { get; set; }

            [JsonProperty("suburb")]
            public string Suburb { get; set; }

            [JsonProperty("state")]
            public string State { get; set; }

            [JsonProperty("post_code")]
            public object Post_code { get; set; }

            [JsonProperty("country")]
            public string Country { get; set; }

            [JsonProperty("current_accommodation_type")]
            public string Current_accommodation_type { get; set; }

            [JsonProperty("permanent_address")]
            public object Permanent_address { get; set; }

            [JsonProperty("postal_suburb")]
            public object Postal_suburb { get; set; }

            [JsonProperty("postal_state")]
            public object Postal_state { get; set; }

            [JsonProperty("postal_post_code")]
            public object Postal_post_code { get; set; }

            [JsonProperty("postal_country")]
            public object Postal_country { get; set; }

            [JsonProperty("permanent_accommodation_type")]
            public string Permanent_accommodation_type { get; set; }

            [JsonProperty("person_to_be_contacted")]
            public object Person_to_be_contacted { get; set; }

            [JsonProperty("emergency_phone_number")]
            public string Emergency_phone_number { get; set; }

            [JsonProperty("relation")]
            public object Relation { get; set; }

            [JsonProperty("attendance_device_id")]
            public object Attendance_device_id { get; set; }

            [JsonProperty("holiday_list")]
            public object Holiday_list { get; set; }

            [JsonProperty("default_shift")]
            public object Default_shift { get; set; }

            [JsonProperty("expense_approver")]
            public object Expense_approver { get; set; }

            [JsonProperty("leave_approver")]
            public object Leave_approver { get; set; }

            [JsonProperty("shift_request_approver")]
            public object Shift_request_approver { get; set; }

            [JsonProperty("ctc")]
            public double Ctc { get; set; }

            [JsonProperty("salary_currency")]
            public object Salary_currency { get; set; }

            [JsonProperty("salary_mode")]
            public string Salary_mode { get; set; }

            [JsonProperty("payroll_cost_center")]
            public object Payroll_cost_center { get; set; }

            [JsonProperty("bank_name")]
            public object Bank_name { get; set; }

            [JsonProperty("bank_ac_no")]
            public object Bank_ac_no { get; set; }

            [JsonProperty("marital_status")]
            public string Marital_status { get; set; }

            [JsonProperty("family_background")]
            public object Family_background { get; set; }

            [JsonProperty("blood_group")]
            public string Blood_group { get; set; }

            [JsonProperty("health_details")]
            public object Health_details { get; set; }

            [JsonProperty("health_insurance_provider")]
            public object Health_insurance_provider { get; set; }

            [JsonProperty("health_insurance_no")]
            public object Health_insurance_no { get; set; }

            [JsonProperty("passport_number")]
            public object Passport_number { get; set; }

            [JsonProperty("valid_upto")]
            public object Valid_upto { get; set; }

            [JsonProperty("date_of_issue")]
            public object Date_of_issue { get; set; }

            [JsonProperty("place_of_issue")]
            public object Place_of_issue { get; set; }

            [JsonProperty("usi_number")]
            public object Usi_number { get; set; }

            [JsonProperty("rtio_sap_number")]
            public object Rtio_sap_number { get; set; }

            [JsonProperty("roy_hill_id_number")]
            public object Roy_hill_id_number { get; set; }

            [JsonProperty("bhp_id_number")]
            public object Bhp_id_number { get; set; }

            [JsonProperty("fmg_id_number")]
            public object Fmg_id_number { get; set; }

            [JsonProperty("bio")]
            public object Bio { get; set; }

            [JsonProperty("resignation_letter_date")]
            public object Resignation_letter_date { get; set; }

            [JsonProperty("relieving_date")]
            public object Relieving_date { get; set; }

            [JsonProperty("held_on")]
            public object Held_on { get; set; }

            [JsonProperty("new_workplace")]
            public object New_workplace { get; set; }

            [JsonProperty("leave_encashed")]
            public string Leave_encashed { get; set; }

            [JsonProperty("encashment_date")]
            public object Encashment_date { get; set; }

            [JsonProperty("reason_for_leaving")]
            public object Reason_for_leaving { get; set; }

            [JsonProperty("feedback")]
            public object Feedback { get; set; }

            [JsonProperty("lft")]
            public int Lft { get; set; }

            [JsonProperty("rgt")]
            public int Rgt { get; set; }

            [JsonProperty("old_parent")]
            public string Old_parent { get; set; }

            [JsonProperty("doctype")]
            public string Doctype { get; set; }

            [JsonProperty("external_work_history")]
            public List<object> External_work_history { get; set; }

            [JsonProperty("certification")]
            public List<object> Certification { get; set; }

            [JsonProperty("internal_work_history")]
            public List<object> Internal_work_history { get; set; }

            [JsonProperty("education")]
            public List<object> Education { get; set; }
        }

        public class Root
        {
            [JsonProperty("data")]
            public Data Data { get; set; }
        }


    }
}