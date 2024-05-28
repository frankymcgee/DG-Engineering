using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows.Forms;
using DG_Engineering.Framework.Global.Assignar;
using Newtonsoft.Json;
using RestSharp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// POST Job to Assignar
        /// </summary>
        /// <param name="projectid">The Job Number the Shift is being attached to</param>
        /// <param name="shiftname">Job Name i.e., Mobilisation | DS</param>
        /// <param name="referencenumber">ID Number for the Reference i.e., 4565001</param>
        private async Task AssignarJobPost(int projectid,string shiftname, string referencenumber)
        {
            StatusLabel.Text = @"Creating Shift: " + shiftname;
            ProjectStartDate.Format = DateTimePickerFormat.Custom;
            ProjectStartDate.CustomFormat = @"yyyy-MM-dd";
	        ProjectEndDate.Format = DateTimePickerFormat.Custom;
            ProjectEndDate.CustomFormat = @"yyyy-MM-dd";
            int orderowner;           
            if (Debugger.IsAttached)
            {
                orderowner = 48;           
            }
            else
            {
                orderowner = 156;              
            }
            string body = "{" +
                   "\n  \"id\": " + referencenumber +
                   ",\n  \"active\": true" +
                   ",\n  \"job_number\": \"" + ProjectJobNumber.Text + "\"" +
                   ",\n  \"po_number\": \"" + ProjectPONumber.Text + "\"" +
                   ",\n  \"client_id\": " + _companyId +
                   ",\n  \"order_owner\": " + orderowner +
                   ",\n  \"project_id\": " + projectid +
                   ",\n  \"location\": \"" + Predictions.Text + "\"" +
                   ",\n  \"job_description\": \"" + shiftname + "\"" +
                   ",\n  \"start_time\": \"\"" +
                   ",\n  \"shift_duration\": \"\"" +
                   ",\n  \"start_date\": \"" + ProjectStartDate.Text + "\"" +
                   ",\n  \"end_date\": \"" + ProjectEndDate.Text + "\"" +
                   ",\n  \"comments\": \"" + shiftname + "\"" +
                   ",\n  \"status_id\": 5" +
                   ",\n  \"type_id\": 1" +
                   ",\n  \"supplier_id\": null" +
                   "\n}";
            await AssignarAPIConnect("/orders", Method.Post, body);
            var id = JsonConvert.DeserializeObject<OrderResp.Root>(Static.AssignarResponseContent).Data.Id;
            if (!string.IsNullOrEmpty(ClientContact.Text))
                {
                ClientAddToJob(id.ToString());
                }
            //Day Shift
            if (shiftname.Contains(" | DS"))
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
            if (shiftname.Contains(" | NS"))
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
            await AssignarAPIConnect( "/contacts?company=" + ProjectClient.Text, Method.Post, null);
            var contactsquery = Static.AssignarResponseContent;
            var contactsresult = JsonConvert.DeserializeObject<Contacts.Root>(contactsquery);
            if (contactsresult != null)
                foreach (var a in contactsresult.Data.Where(a =>
                             a.FirstName == ClientContact.Text.Split(" ".ToCharArray())[0] &&
                             ClientContact.Text.Split(" ".ToCharArray())[1].Contains(a.LastName)))
                {
                    contactId = a.Id;
                }
            var body = "{\n \"order_id\":" + orderid + ",\n  \"contact_id\":" + contactId + "\n}";
            await AssignarAPIConnect("/orders/" + orderid + "/contacts", Method.Post, body);
        }
    }
}