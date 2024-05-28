using RestSharp;
using System.Threading.Tasks;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        
        /// <summary>
        /// Creates Job Disciplines for a Job Reference based on inputted Values.
        /// </summary>
        /// <param name="starttime">Start Time of Shift</param>
        /// <param name="endtime">End Time of Shift</param>
        /// <param name="orderid">Order ID of the Project the Task is being assigned for.</param>
        /// <param name="taskid">Task ID from Assignar.</param>
        /// <param name="quantity">Number of required Labour.</param>
        private async Task TaskCreation(string starttime, string endtime, int orderid, int taskid, decimal quantity)
        {
            if (quantity > 0)
            {
                await AssignarTaskPost(starttime,endtime,orderid,taskid,(int) quantity);
            }

        }
        /// <summary>
        /// POST Job Disciplines to a Job Reference.
        /// </summary>
        /// <param name="starttime">Start Time of the Job Discipline</param>
        /// <param name="endtime">End Time of the Job Discipline</param>
        /// <param name="orderid">the Order ID of the Project the Task is being assigned for</param>
        /// <param name="taskid">Job Discipline ID from Assignar</param>
        /// <param name="quantity">Amount of Workers required for the Job Discipline</param>
        private async Task AssignarTaskPost(string starttime, string endtime, int orderid, int taskid, int quantity)
        {
            var body = "{\n  \"task_id\": " + taskid + ",\n  \"task_quantity\": " + quantity + ",\n  \"active\": true,\n  \"req_machines\": true,\n  \"start_time\": " + starttime + ",\n  \"end_time\": " + endtime + ",\n}";
            await AssignarAPIConnect("/orders/ " + orderid + " /tasks", Method.Post, body);           
        }
    }
}