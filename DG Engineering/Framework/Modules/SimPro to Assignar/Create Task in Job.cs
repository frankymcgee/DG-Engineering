using System;
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