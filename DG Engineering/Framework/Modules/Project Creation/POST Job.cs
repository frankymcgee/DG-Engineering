using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
        private async Task AssignarJobPost(string jobname, string idnumber)
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
                value = "{" +
                    "\n  \"id\": " + idnumber +
                    ",\n  \"active\": true" +
                    ",\n  \"job_number\": \"" + ProjectJobNumber.Text + "\"" + 
                    ",\n  \"po_number\": \"" + ProjectPONumber.Text + "\"" + 
                    ",\n  \"client_id\": " + _companyId +
                    ",\n  \"order_owner\": 48" +
                    ",\n  \"project_id\": " + _projectId +
                    ",\n  \"location\": \"" + ProjectAddress.Text + "\"" + 
                    ",\n  \"job_description\": \"" + jobname + "\"" + 
                    ",\n  \"start_time\": \"\"" +
                    ",\n  \"shift_duration\": \"\"" +
                    ",\n  \"start_date\": \"" + ProjectStartDate.Text + "\"" + 
                    ",\n  \"end_date\": \"" + ProjectEndDate.Text + "\"" + 
                    ",\n  \"comments\": \"" + jobname + "\"" + 
                    ",\n  \"status_id\": 5" +
                    ",\n  \"type_id\": 1" +
                    ",\n  \"supplier_id\": null" +
                    "\n}";
            }
            else
            {
                value = "{" +
                    ",\n  \"id\": " + idnumber +
                    ",\n  \"active\": true" +
                    ",\n  \"job_number\": \"" + ProjectJobNumber.Text + "\"" + 
                    ",\n  \"po_number\": " + ProjectPONumber.Text +
                    ",\n  \"client_id\": " + _companyId +
                    ",\n  \"order_owner\": 186" +
                    ",\n  \"project_id\": " + _projectId +
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
            var id = JsonConvert.DeserializeObject<OrderResp.Root>(restResponse.Content).Data.Id;
            if (!string.IsNullOrEmpty(ClientContact.Text))
                {
                ClientAddToJob(id.ToString());
                }
            //Day Shift
            if (jobname.Contains(" | DS"))
            {
                foreach (Control a in Shift_Tabs.TabPages[0].Controls)
                {
                    if (a.GetType().ToString().Equals("System.Windows.Forms.NumericUpDown"))
                    {
                        if (a.Text != "0" && a.Tag.ToString().Contains("Day"))
                        {
                            var discipline = Convert.ToInt32(a.Tag.ToString().Split(',')[1]);
                            if (discipline == 33 || discipline == 3 || discipline == 16)
                            {
                                await TaskCreation("05:00", "18:00", id, discipline, Convert.ToDecimal(a.Text));
                            }
                            else
                            {
                                await TaskCreation("06:00", "18:00", id, discipline, Convert.ToDecimal(a.Text));
                            }
                        }
                    }
                }
            }
            //Night Shift
            if (jobname.Contains(" | NS"))
            {
                foreach (Control a in Shift_Tabs.TabPages[1].Controls)
                {
                    if (a.GetType().ToString().Equals("System.Windows.Forms.NumericUpDown"))
                    {
                        if (a.Text != "0" && a.Tag.ToString().Contains("Night"))
                        {
                            var discipline = Convert.ToInt32(a.Tag.ToString().Split(',')[1]);
                            if (discipline == 33 || discipline == 3 || discipline == 16)
                            {
                                await TaskCreation("17:00", "06:00", id, discipline, Convert.ToDecimal(a.Text));
                            }
                            else
                            {
                                await TaskCreation("18:00", "06:00", id, discipline, Convert.ToDecimal(a.Text));
                            }
                        }
                    }
                }
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