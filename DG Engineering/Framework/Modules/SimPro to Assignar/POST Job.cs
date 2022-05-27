﻿using System.Net;
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
        /// <param name="jobname">Job Name i.e. Mobilisation|DS</param>
        public void AssignarJobPost(string jobname)
        {
            StatusLabel.Text = @"Creating Job: " + jobname;
            ProjectStartDate.Format = DateTimePickerFormat.Custom;
            ProjectStartDate.CustomFormat = @"yyyy-MM-dd";
	        ProjectEndDate.Format = DateTimePickerFormat.Custom;
            ProjectEndDate.CustomFormat = @"yyyy-MM-dd";
	        var restClient = new RestClient(Static.AssignarDashboardUrl + "orders")
	        {
		        Timeout = -1
	        };
	        var restRequest = new RestRequest(Method.POST);
	        restRequest.AddHeader("Content-Type", "application/json");
	        restRequest.AddHeader("Authorization", Static.JwtToken);
	        var value = @"{
                                ""active"": true,
                                ""po_number"": " + ProjectPOTextBox.Text +
                                ",\n  \"client_id\": " + CompanyId +
                                ",\n  \"order_owner\": 114," +
                                "\n  \"project_id\": " + ProjectId +
                                ",\n  \"location\": " + ProjectAddress_TextBox.Text +
                                ",\n  \"job_description\": " + jobname +
                                ",\n  \"start_time\": \"\"" +
                                ",\n  \"shift_duration\": \"\"" +
                                ",\n  \"start_date\": " + ProjectStartDate.Text + 
                                ",\n  \"end_date\": " + ProjectEndDate.Text +
                                ",\n  \"comments\": " + "Job Number is: " + SimProQuoteText.Text +
                                ",\n  \"status_id\": 5" +
                                ",\n  \"type_id\": 1" +
                                ",\n  \"supplier_id\": null" +
                                ",\n  \"tags\": [" +
                                "\n            {" +
                                "\n                \"tag_id\": 37" +
                                ",\n                \"name\": \"DG Engineering | KTA\"" +
                                ",\n                \"description\": \"DG Engineering Karratha\"" +
                                ",\n                \"color\": \"#f44336\"" +
                                "\n            }" +
                                "\n        ]" +
                                "\n}";
	        restRequest.AddParameter("application/json", value, ParameterType.RequestBody);
	        var restResponse = restClient.Execute(restRequest);
	        if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                return;
            }
            string starttime = null;
            string endtime = null;
            var id = JsonConvert.DeserializeObject<OrderResp.Root>(restResponse.Content).Data.Id;
            ClientAddToJob(id.ToString());
            switch (jobname)
            {
                case "Mobilisation|DS":
                    starttime = "06:00";
                    endtime = "18:00";
                    break;
                case "Mobilisation|NS":
                    starttime = "18:00";
                    endtime = "06:00";
                    break;
                case "Work|DS":
                    starttime = "06:00";
                    endtime = "18:00";
                    break;
                case "Work|NS":
                    starttime = "18:00";
                    endtime = "06:00";
                    break;
                case "DeMobilisation|DS":
                    starttime = "06:00";
                    endtime = "18:00";
                    break;
                case "DeMobilisation|NS":
                    starttime = "18:00";
                    endtime = "06:00";
                    break;
            }
            //Superintendent Tasks
            if (SuperintDSUD.Value > 0)
            {
                switch (jobname)
                {
                    case "Mobilisation|DS":
                        starttime = "05:00";
                        endtime = "18:00";
                        break;
                    case "Mobilisation|NS":
                        starttime = "17:00";
                        endtime = "06:00";
                        break;
                    case "Work|DS":
                        starttime = "05:00";
                        endtime = "18:00";
                        break;
                    case "Work|NS":
                        starttime = "17:00";
                        endtime = "06:00";
                        break;
                    case "DeMobilisation|DS":
                        starttime = "05:00";
                        endtime = "18:00";
                        break;
                    case "DeMobilisation|NS":
                        starttime = "17:00";
                        endtime = "06:00";
                        break;
                }
                TaskCreation(starttime,endtime,id,3,SuperintDSUD.Value);
            }
            if (SuperintNSUD.Value > 0)
            {
                switch (jobname)
                {
                    case "Mobilisation|DS":
                        starttime = "05:00";
                        endtime = "18:00";
                        break;
                    case "Mobilisation|NS":
                        starttime = "17:00";
                        endtime = "06:00";
                        break;
                    case "Work|DS":
                        starttime = "05:00";
                        endtime = "18:00";
                        break;
                    case "Work|NS":
                        starttime = "17:00";
                        endtime = "06:00";
                        break;
                    case "DeMobilisation|DS":
                        starttime = "05:00";
                        endtime = "18:00";
                        break;
                    case "DeMobilisation|NS":
                        starttime = "17:00";
                        endtime = "06:00";
                        break;
                }
                TaskCreation(starttime,endtime,id,3,SuperintNSUD.Value);
            }
            //Supervisor Tasks
            if (SupervisorDSUD.Value > 0)
            {
                switch (jobname)
                {
                    case "Mobilisation|DS":
                        starttime = "05:00";
                        endtime = "18:00";
                        break;
                    case "Mobilisation|NS":
                        starttime = "17:00";
                        endtime = "06:00";
                        break;
                    case "Work|DS":
                        starttime = "05:00";
                        endtime = "18:00";
                        break;
                    case "Work|NS":
                        starttime = "17:00";
                        endtime = "06:00";
                        break;
                    case "DeMobilisation|DS":
                        starttime = "05:00";
                        endtime = "18:00";
                        break;
                    case "DeMobilisation|NS":
                        starttime = "17:00";
                        endtime = "06:00";
                        break;
                }
                TaskCreation(starttime,endtime,id,3,SupervisorDSUD.Value);
            }
            if (SupervisorNSUD.Value > 0)
            {
                switch (jobname)
                {
                    case "Mobilisation|DS":
                        starttime = "05:00";
                        endtime = "18:00";
                        break;
                    case "Mobilisation|NS":
                        starttime = "17:00";
                        endtime = "06:00";
                        break;
                    case "Work|DS":
                        starttime = "05:00";
                        endtime = "18:00";
                        break;
                    case "Work|NS":
                        starttime = "17:00";
                        endtime = "06:00";
                        break;
                    case "DeMobilisation|DS":
                        starttime = "05:00";
                        endtime = "18:00";
                        break;
                    case "DeMobilisation|NS":
                        starttime = "17:00";
                        endtime = "06:00";
                        break;
                }
                TaskCreation(starttime,endtime,id,3,SupervisorNSUD.Value);
            }
            if (jobname.Contains("|DS"))
            {
                //Day Shift
                TaskCreation(starttime,endtime,id,32,LHDSUD.Value);
                TaskCreation(starttime,endtime,id,8,BlastPntDSUD.Value);
                TaskCreation(starttime,endtime,id,9,BMWDSUD.Value);
                TaskCreation(starttime,endtime,id,17,MechFitterDSUD.Value);
                TaskCreation(starttime,endtime,id,23,RiggerDSUD.Value);
                TaskCreation(starttime,endtime,id,24,CraneDvrDSUD.Value);
                TaskCreation(starttime,endtime,id,31,CWDSUD.Value);
                TaskCreation(starttime,endtime,id,25,ScaffDSUD.Value);
                TaskCreation(starttime,endtime,id,26,TADSUD.Value);
                TaskCreation(starttime,endtime,id,14,TechnicianDSUD.Value);
                TaskCreation(starttime,endtime,id,1,ExcavOpDSUD.Value);
                TaskCreation(starttime,endtime,id,16,HSEQDSUD.Value);
            }
            else if (jobname.Contains("|NS"))
            {
                //Night Shift
                TaskCreation(starttime,endtime,id,32,LHNSUD.Value);
                TaskCreation(starttime,endtime,id,8,BlasPntNSUD.Value);
                TaskCreation(starttime,endtime,id,9,BMWNSUD.Value);
                TaskCreation(starttime,endtime,id,17,MechFitterNSUD.Value);
                TaskCreation(starttime,endtime,id,23,RiggerNSUD.Value);
                TaskCreation(starttime,endtime,id,24,CraneDvrNSUD.Value);
                TaskCreation(starttime,endtime,id,31,CWNSUD.Value);
                TaskCreation(starttime,endtime,id,25,ScaffNSUD.Value);
                TaskCreation(starttime,endtime,id,26,TANSUD.Value);
                TaskCreation(starttime,endtime,id,14,TechnicianNSUD.Value);
                TaskCreation(starttime,endtime,id,1,ExcavOpNSUD.Value);
                TaskCreation(starttime,endtime,id,16,HSEQNSUD.Value);
            }
        }
        public void ClientAddToJob(string orderid)
        {
            var contact_id = 0;
            var contactsquery = AssignarConnect(Static.AssignarDashboardUrl + "contacts?company=" + SimProClient_TextBox.Text, Static.JwtToken, RestSharp.Method.GET, null);
            var contactsresult = JsonConvert.DeserializeObject<Contacts.Root>(contactsquery);
            foreach (var a in contactsresult.Data)
            {
                if (a.FirstName == ClientContact_ComboBox.Text.Split(" ".ToCharArray())[0] && ClientContact_ComboBox.Text.Split(" ".ToCharArray())[1].Contains(a.LastName))
                {
                    contact_id = a.Id;
                }
            }
            var body = "{\n \"order_id\":" + orderid + ",\n  \"contact_id\":" + contact_id + "\n}";
            var request = AssignarConnect(Static.AssignarDashboardUrl + "orders/" + orderid + "/contacts", Static.JwtToken, Method.POST, body);
        }
    }
}