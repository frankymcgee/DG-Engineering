using System;
using System.Net;
using System.Windows.Forms;
using DG_Engineering.Framework.Global.Assignar;
using DG_Engineering.Framework.Global.Assignar.ProjectPost;
using Newtonsoft.Json;
using RestSharp;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// POST Project from SimPro to Assignar.
        /// </summary>
        public void AssignarProjectPost()
        {
            ProjectStartDate.Format = DateTimePickerFormat.Custom;
            ProjectStartDate.CustomFormat = @"yyyy-MM-dd";
            ProjectEndDate.Format = DateTimePickerFormat.Custom;
            ProjectEndDate.CustomFormat = @"yyyy-MM-dd";
            var restClient = new RestClient(Static.AssignarDashboardUrl + "projects")
            {
                Timeout = -1
            };
            var restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", Static.JwtToken);
            var value = "{\n  \"active\": true,\n  \"client_id\": " + CompanyId + ",\n  \"name\": " + ProjectNameTextBox.Text + ",\n  \"address\": " + ProjectAddress_TextBox.Text + ",\n  \"external_id\": " + SimProQuoteText.Text + ",\n  \"start_date\": " + ProjectStartDate.Text + ",\n  \"end_date\": " + ProjectEndDate.Text + ",\n  \"tags\": [\n            {\n                \"tag_id\": 7,\n                \"name\": \"DGE\",\n                \"description\": \"Project for DGE\",\n                \"color\": \"#f44336\"\n            }\n        ]\n}";
            restRequest.AddParameter("application/json", value, ParameterType.RequestBody);
            var restResponse = restClient.Execute(restRequest);
            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                ProjectId = JsonConvert.DeserializeObject<ProjectPost.Root>(restResponse.Content).Data.Id;
                AssignarJobPost("Mobilisation|DS");
                AssignarJobPost("Mobilisation|NS");
                AssignarJobPost("Work|DS");
                AssignarJobPost("Work|NS");
                AssignarJobPost("DeMobilisation|DS");
                AssignarJobPost("DeMobilisation|NS");
                MessageBox.Show(@"Project Created in Assignar. Please upload the Documents that are required into the Project.", @"Success");
                DownloadAllProjects(Static.AssignarDashboardUrl + "projects/", Static.JwtToken);
                ProjectViewer.CoreWebView2.Navigate("https://dashboard.assignar.com.au/v1/#!/projects/detail/" + ProjectId + "/documents");
            }
            else
            {
                MessageBox.Show(@"Whoops! An Error has occurred trying to create your Project. Please try again.", @"Error");
            }
        }

        public void AssignarJobPost(string jobname)
        {
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
                                ",\n  \"project_id\": " + ProjectId +
                                ",\n  \"job_description\": " + jobname +
                                ",\n  \"start_time\": \"\"" +
                                ",\n  \"shift_duration\": \"\"" +
                                ",\n  \"start_date\": " + ProjectStartDate.Text + 
                                ",\n  \"end_date\": " + ProjectEndDate.Text +
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
        /// <summary>
        /// Creates the Task based on inputted Values.
        /// </summary>
        /// <param name="starttime">Start Time of Shift</param>
        /// <param name="endtime">End Time of Shift</param>
        /// <param name="orderid">Order ID of the Project the Task is being assigned for.</param>
        /// <param name="taskid">Task ID from Assignar.</param>
        /// <param name="quantity">Number of required Labour.</param>
        public void TaskCreation(string starttime, string endtime, int orderid, int taskid, decimal quantity)
        {
            if (quantity > 0)
            {
                AssignarTaskPost(starttime,endtime,orderid,taskid,(int) quantity);
            }

        }
        public void AssignarTaskPost(string starttime, string endtime, int orderid, int taskid, int quantity)
        {
            var restClient = new RestClient(Static.AssignarDashboardUrl + "orders/" + orderid + "/tasks")
            {
                Timeout = -1
            };
            var restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", Static.JwtToken);
            var value = "{\n  \"task_id\": " + taskid + ",\n  \"task_quantity\": " + quantity + ",\n  \"active\": true,\n  \"req_machines\": true,\n  \"start_time\": " + starttime + ",\n  \"end_time\": " + endtime + ",\n}";
            restRequest.AddParameter("application/json", value, ParameterType.RequestBody);
            Console.WriteLine(restClient.Execute(restRequest).Content);
        }
        
    }
}