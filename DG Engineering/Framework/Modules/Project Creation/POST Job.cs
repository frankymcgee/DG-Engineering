﻿using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using DG_Engineering.Framework.Global.Assignar;
using Newtonsoft.Json;
using RestSharp;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// POST Job to Assignar
        /// </summary>
        /// <param name="jobname">Job Name i.e., Mobilisation | DS</param>
        /// <param name="idnumber">ID Number for the Reference i.e., 4565001</param>
        private void AssignarJobPost(string jobname, string idnumber)
        {
            StatusLabel.Text = @"Creating Job:  " + jobname;
            ProjectStartDate.Format = DateTimePickerFormat.Custom;
            ProjectStartDate.CustomFormat = @"yyyy-MM-dd";
	        ProjectEndDate.Format = DateTimePickerFormat.Custom;
            ProjectEndDate.CustomFormat = @"yyyy-MM-dd";
            var restClient = new RestClient(Static.AssignarDashboardUrl + "orders");
            var restRequest = new RestRequest(Static.AssignarDashboardUrl + "orders", Method.POST);
	        restRequest.AddHeader("Content-Type", "application/json");
	        restRequest.AddHeader("Authorization", Static.JwtToken);
            string value;
            if (Debugger.IsAttached)
            {
                value = @"{
                                ""active"": true,
                                ""po_number"": " + ProjectPONumber.Text +
                            ",\n  \"id\": " + idnumber +
                            ",\n  \"client_id\": " + _companyId +
                            ",\n  \"order_owner\": 41" +
                            ",\n  \"project_id\": " + _projectId +
                            ",\n  \"job_number\": \"" + ProjectJobNumber.Text + "\"" + 
                            ",\n  \"po_number\": \"" + ProjectPONumber.Text + "\"" + 
                            ",\n  \"location\": \"" + ProjectAddress.Text + "\"" + 
                            ",\n  \"job_description\": \"" + jobname + "\"" + 
                            ",\n  \"start_time\": \"\"" +
                            ",\n  \"shift_duration\": \"\"" +
                            ",\n  \"start_date\": " + ProjectStartDate.Text + 
                            ",\n  \"end_date\": " + ProjectEndDate.Text +
                            ",\n  \"comments\": \"" + jobname + "\"" + 
                            ",\n  \"status_id\": 5" +
                            ",\n  \"type_id\": 1" +
                            ",\n  \"supplier_id\": null" +
                            "\n}";
            }
            else
            {
                value = @"{
                                ""active"": true,
                                ""po_number"": " + ProjectPONumber.Text +
                            ",\n  \"id\": " + idnumber +
                            ",\n  \"client_id\": " + _companyId +
                            ",\n  \"order_owner\": 186" +
                            ",\n  \"project_id\": " + _projectId +
                            ",\n  \"job_number\": \"" + ProjectJobNumber.Text + "\"" + 
                            ",\n  \"po_number\": \"" + ProjectPONumber.Text + "\"" + 
                            ",\n  \"location\": \"" + ProjectAddress.Text + "\"" + 
                            ",\n  \"job_description\": \"" + jobname + "\"" + 
                            ",\n  \"start_time\": \"\"" +
                            ",\n  \"shift_duration\": \"\"" +
                            ",\n  \"start_date\": " + ProjectStartDate.Text + 
                            ",\n  \"end_date\": " + ProjectEndDate.Text +
                            ",\n  \"comments\": \"" + jobname + "\"" + 
                            ",\n  \"status_id\": 5" +
                            ",\n  \"type_id\": 1" +
                            ",\n  \"supplier_id\": null" +
                            "\n}";
            }
	        
	        restRequest.AddParameter("application/json", value, ParameterType.RequestBody);
	        var restResponse = restClient.Execute(restRequest);
	        if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                return;
            }
            var starttime = "06:00";
            var endtime = "18:00";
            var id = JsonConvert.DeserializeObject<OrderResp.Root>(restResponse.Content).Data.Id;
            ClientAddToJob(id.ToString());
            //Superintendent Tasks
            if (SuperintDSUD.Value > 0 || SuperintNSUD.Value > 0)
            {
                switch (jobname)
                {
                    
                    case "Work | DS":
                        starttime = "05:00";
                        endtime = "18:00";
                        break;
                    case "Work | NS":
                        starttime = "17:00";
                        endtime = "06:00";
                        break;
                }
                TaskCreation(starttime,endtime,id,33,SuperintDSUD.Value);
                TaskCreation(starttime,endtime,id,33,SuperintNSUD.Value);
            }
            //Supervisor Tasks
            if (SupervisorDSUD.Value > 0 || SupervisorNSUD.Value > 0)
            {
                switch (jobname)
                {
                   case "Work | DS":
                        starttime = "05:00";
                        endtime = "18:00";
                        break;
                    case "Work | NS":
                        starttime = "17:00";
                        endtime = "06:00";
                        break;
                }
                TaskCreation(starttime,endtime,id,3,SupervisorDSUD.Value);
                TaskCreation(starttime,endtime,id,3,SupervisorNSUD.Value);
            }
            //Site Safety Adviser Tasks
            if (HSEQDSUD.Value > 0 || HSEQNSUD.Value > 0)
            {
                switch (jobname)
                {
                    case "Work | DS":
                        starttime = "05:00";
                        endtime = "18:00";
                        break;
                    case "Work | NS":
                        starttime = "17:00";
                        endtime = "06:00";
                        break;
                }
                TaskCreation(starttime,endtime,id,16,HSEQDSUD.Value);
                TaskCreation(starttime,endtime,id,16,HSEQNSUD.Value);
            }
            if (jobname.Contains(" | DS"))
            {
                //Day Shift
                TaskCreation("06:00","18:00",id,32,LHDSUD.Value);
                TaskCreation("06:00","18:00",id,8,BlastPntDSUD.Value);
                TaskCreation("06:00","18:00",id,9,BMWDSUD.Value);
                TaskCreation("06:00","18:00",id,17,MechFitterDSUD.Value);
                TaskCreation("06:00","18:00",id,23,RiggerDSUD.Value);
                TaskCreation("06:00","18:00",id,24,CraneDvrDSUD.Value);
                TaskCreation("06:00","18:00",id,31,CWDSUD.Value);
                TaskCreation("06:00","18:00",id,25,ScaffDSUD.Value);
                TaskCreation("06:00","18:00",id,26,TADSUD.Value);
                TaskCreation("06:00","18:00",id,14,TechnicianDSUD.Value);
                TaskCreation("06:00","18:00",id,1,ExcavOpDSUD.Value);
            }
            else if (jobname.Contains(" | NS"))
            {
                //Night Shift
                TaskCreation("18:00","06:00",id,32,LHNSUD.Value);
                TaskCreation("18:00","06:00",id,8,BlasPntNSUD.Value);
                TaskCreation("18:00","06:00",id,9,BMWNSUD.Value);
                TaskCreation("18:00","06:00",id,17,MechFitterNSUD.Value);
                TaskCreation("18:00","06:00",id,23,RiggerNSUD.Value);
                TaskCreation("18:00","06:00",id,24,CraneDvrNSUD.Value);
                TaskCreation("18:00","06:00",id,31,CWNSUD.Value);
                TaskCreation("18:00","06:00",id,25,ScaffNSUD.Value);
                TaskCreation("18:00","06:00",id,26,TANSUD.Value);
                TaskCreation("18:00","06:00",id,14,TechnicianNSUD.Value);
                TaskCreation("18:00","06:00",id,1,ExcavOpNSUD.Value);
            }
        }
        private async void ClientAddToJob(string orderid)
        {
            var contactId = 0;
            var contactsquery = await AssignarConnect(Static.AssignarDashboardUrl + "contacts?company=" + ProjectClient.Text, Static.JwtToken, Method.POST, null);
            var contactsresult = JsonConvert.DeserializeObject<Contacts.Root>(contactsquery);
            if (contactsresult != null)
                foreach (var a in contactsresult.Data.Where(a =>
                             a.FirstName == ClientContact.Text.Split(" ".ToCharArray())[0] &&
                             ClientContact.Text.Split(" ".ToCharArray())[1].Contains(a.LastName)))
                {
                    contactId = a.Id;
                }

            var body = "{\n \"order_id\":" + orderid + ",\n  \"contact_id\":" + contactId + "\n}";
            await AssignarConnect(Static.AssignarDashboardUrl + "orders/" + orderid + "/contacts", Static.JwtToken, Method.POST, body);
        }
    }
}