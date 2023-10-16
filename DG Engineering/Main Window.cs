using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using DG_Engineering.Framework.Global.Assignar;
using DG_Engineering.Framework.Global.SimPro;
using DG_Engineering.Framework.Global.ERPNext;
using DG_Engineering.Framework.Global.MYOB;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using RestSharp;
using Timer = System.Timers.Timer;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Microsoft.Web.WebView2.WinForms;

namespace DG_Engineering
{
    public partial class MainWindow : Form
    {
        #region Initialise

        public MainWindow()
        {
            InitializeComponent();
            if (!Debugger.IsAttached)
            {
                TestButton.Visible = false;
            }
        }

        #endregion

        #region Main Window Load
        private async void MainWindow_Load(object sender, EventArgs e)
        {
            Version version;
            try
            {
                version = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
            catch (Exception)
            {
                version = Assembly.GetExecutingAssembly().GetName().Version;
            }
            string sandbox = Static.ClientId == "dgengineering" ? "DG Engineering" : "SANDBOX ENVIRONMENT";
            VersionLabel.Text = @"Version: " + version + @"  |  " + "Connected to: " + sandbox;
            var environment = await CoreWebView2Environment.CreateAsync(null, Path.GetTempPath());            
            await AdminViewer.EnsureCoreWebView2Async(environment);
            await ScheduleViewer.EnsureCoreWebView2Async(environment);
            await ProjectViewer.EnsureCoreWebView2Async(environment);
            await RecruitmentViewer.EnsureCoreWebView2Async(environment);
            await Jobs_Viewer.EnsureCoreWebView2Async(environment);
            ScheduleViewer.CoreWebView2.Navigate("https://dashboard.assignar.com.au/scheduler/timeline");
            ProgressBar_Compiler.Step = 25;
            Assignar_Tabs.TabPages.Remove(Job_Pack_tab);
            Assignar_Tabs.TabPages.Remove(Admin_Tab);
            Assignar_Tabs.TabPages.Remove(Schedule_Tab);
            Assignar_Tabs.TabPages.Remove(Clients_Tab);
            Assignar_Tabs.TabPages.Remove(Recruitment_Tab);
            Assignar_Tabs.TabPages.Remove(DocumentGen_Tab);
            Assignar_Tabs.TabPages.Remove(cst_Tab);
            await DownloadRoleDescriptions();
            await DownloadClientList();
            var timer = new Timer
            {
                AutoReset = true,
                Enabled = true,
                Interval = 1000,
                Site = null,
                SynchronizingObject = null
            };
            timer.Elapsed += OnTimedEvent;
            var progressbar = new Timer
            {
                AutoReset = true,
                Enabled = true,
                Interval = 2000,
                Site = null,
                SynchronizingObject = null
            };
        }
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Static.ExpiresIn--;
            if (Static.ExpiresIn <= 1150)
            {
                RefreshMyob();
            }
        }
        #endregion

        #region Modules

        #region Job Creation
        private async void JobNumberSearch_Click(object sender, EventArgs e)
        {
            await MyobSearch(ProjectJobNumber.Text);
            try
            {
                ClientContact.Items.Clear();
                await AssignarAPIConnect("/contacts?company=" + ProjectClient.Text, Method.Get, null);
                var contactsquery = Static.AssignarResponseContent;
                var contactsresult = JsonConvert.DeserializeObject<Contacts.Root>(contactsquery);
                foreach (var a in contactsresult.Data)
                {
                    ClientContact.Items.Add(a.FirstName + " " + a.LastName + " - " + a.JobTitle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Error: " + ex.Message, @"Site Contact error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private async void PushAssignar_Button_Click(object sender, EventArgs e)
        {
            if (ProjectPONumber.Text == @"MISSING PO NUMBER")
            {
                MessageBox.Show(@"PLEASE ADD PROJECT PO NUMBER", @"Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                await CompanyIdExtract(ProjectClient.Text);
                await AssignarProjectPost();                
            }
        }
        private async Task Address_upd_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ProjectClient.Text)) return;
            var request =
                MyobConnect(Static.Companyfileuri + "/" + Static.Companyfileguid, "/Contact/Customer?$filter=CompanyName eq \'" + ProjectClient.Text + "\'", Method.Get).Content;
            var result = JsonConvert.DeserializeObject<Customer.Root>(request);
            ProjectAddress.Items.Clear();
            foreach (var b in result.Items.SelectMany(a => a.Addresses))
            {
                ProjectAddress.Items.Add(b.Street + ", " + b.City + ", " + b.State + ", " + b.PostCode);
            }
            ClientContact.Items.Clear();
            await AssignarAPIConnect("/contacts?company=" + ProjectClient.Text, Method.Get, null);
            var contactsquery = Static.AssignarResponseContent;
            var contactsresult = JsonConvert.DeserializeObject<Contacts.Root>(contactsquery);
            foreach (var a in contactsresult.Data)
            {
                ClientContact.Items.Add(a.FirstName + " " + a.LastName + " - " + a.JobTitle);
            }
        }
        private void PushToJobPackButton_Click(object sender, EventArgs e)
        {
            PushToJobPack();
        }
        #endregion

        #region Jobs
        private async void Job_MyobSearch_Click(object sender, EventArgs e)
        {
            await MyobSearch(Jobs_ProjectNumber.Text);
        }
        private void Jobs_GenerateCover_Click(object sender, EventArgs e)
        {
            JobCoverLetterChecklist();
        }
        #endregion

        #region Administration
        private async void AdminProjButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AdminProjectNumber.Text))
            {
                MessageBox.Show(@"PLEASE ADD PROJECT NUMBER", @"Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                await AdminProjSearch();
            }
        }

        private async Task AdminProjSearch()
        {
            await AdminJobInformation("/projects/" + AdminProjectNumber.Text);
            var url = @"https://dashboard.assignar.com.au/v1/#!/projects/detail/" + Static.AssignarInternalNumber + @"/edit";
            AdminViewer.CoreWebView2.Navigate(url);
        }

        private async void AdminDispJobInfo_Click(object sender, EventArgs e)
        {
            AdminJobNo.Text = AdminJobComboBox.Text.Split(" | ".ToCharArray())[0];
            await AssignarShiftInformation("/orders/" + AdminJobNo.Text);
        }
        private void WipeCleanButton_Click(object sender, EventArgs e)
        {

        }

        private async void AdminNewJobBtn_Click(object sender, EventArgs e)
        {
            await AssignarNewShift();
        }
        #endregion

        #region Schedule
        #endregion

        #region Clients
        #endregion

        #region Fieldworkers
        private async void CreateWorkerButton_Click(object sender, EventArgs e)
        {
            CreateWorkerBtn.Text = "GENERATING";
            await ERPNextLogin();
            int employmenttype = 1;
            switch (fwEmployment.Text)
            {
                case "Casual":
                    employmenttype = 1;
                    break;
                case "Casual Flat Rate":
                    employmenttype = 2;
                    break;
                case "Full Time":
                    employmenttype = 3;
                    break;
                case "Full Time Flat Rate":
                    employmenttype = 4;
                    break;
            }
            var body = @"{" + "\n" +
            @"  ""user_type_id"": 2," + "\n" +
            @"  ""first_name"": """ + fld_first.Text + @"""," + "\n" +
            @"  ""last_name"": """ + fld_last.Text + @"""," + "\n" +
            @"  ""username"": """ + fld_first.Text.ToLower() + "." + fld_last.Text.ToLower() + @"""," + "\n" +
            @"  ""full_name"": """ + fld_first.Text + " " + fld_last.Text + @"""," + "\n" +
            @"  ""full_name"": """ + fld_first.Text + " " + fld_last.Text + @"""," + "\n" +
            @"  ""dob"": """ + fld_dob.Value.ToString("yyyy-MM-dd") + @"""," + "\n" +
            @"  ""address"":  """ + fld_add.Text + @"""," + "\n" +
            @"  ""suburb"": """ + fld_sub.Text + @"""," + "\n" +
            @"  ""state"": """ + fld_state.Text + @"""," + "\n" +
            @"  ""postcode"": """ + fld_postcode.Text + @"""," + "\n" +
            @"  ""email"": """ + fld_email.Text + @"""," + "\n" +
            @"  ""contact"": """ + fld_number.Text + @"""," + "\n" +
            @"  ""employment_type"": " + employmenttype + "," + "\n" +
            @"  ""emergency_contact"": """ + emcontact.Text + @"""," + "\n" +
            @"  ""emergency_contact_name"": """ + emnumber.Text + @"""," + "\n" +
            @"  ""password"": """ + fld_first.Text.ToLower() + "." + fld_last.Text.ToLower() + @"""," + "\n" +
            @"  ""active"": true," + "\n" +
            @"}";
            await AssignarAPIConnect("/users", Method.Post, body);
            await CreateUserAsync(Static.Cookie, Static.ERPNext);
            await CreateEmployeeAsync(Static.Cookie, Static.ERPNext);
            await ERPNextLogout(Static.Cookie, Static.ERPNext);
        }
        public async Task CreateEmployeeAsync(string cookie, RestClient client)
        {
            var request = new RestRequest("/api/resource/Employee", Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("b544d47f25a916a", "079d58b09f6cd01");
            request.AddHeader("Cookie", "full_name=Administrator; sid=" + cookie + "; system_user=yes; user_id=Administrator;");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("naming_series", "HR-EMP-");
            request.AddParameter("first_name", fld_first.Text);
            request.AddParameter("last_name", fld_last.Text);
            request.AddParameter("employee_name", fld_first.Text + " " + fld_last.Text);
            request.AddParameter("gender", fld_gender.Text);
            request.AddParameter("date_of_birth", fld_dob.Value.ToString("yyyy-MM-dd"));
            request.AddParameter("date_of_joining", fld_doj.Value.ToString("yyyy-MM-dd"));
            request.AddParameter("status", "Active");
            request.AddParameter("user_id", fld_email.Text);
            request.AddParameter("designation", fwPosHeld.Text);
            request.AddParameter("create_user_permission", "1");
            request.AddParameter("company", "De Wet & Green Engineering PTY LTD");
            request.AddParameter("department", "Execution - DW&GEPL");
            request.AddParameter("employment_type", fwEmployment.Text);
            request.AddParameter("cell_number", fld_number.Text);
            request.AddParameter("personal_email", fld_email.Text);
            request.AddParameter("unsubscribed", "1");
            request.AddParameter("current_address", fld_add.Text);
            request.AddParameter("suburb", fld_sub.Text);
            request.AddParameter("state", fld_state.Text);
            request.AddParameter("postcode", fld_postcode.Text);
            request.AddParameter("country", "Australia");
            request.AddParameter("person_to_be_contected", emcontact.Text);
            request.AddParameter("emergency_phone_number", emnumber.Text);
            RestResponse response = await client.ExecuteAsync(request);
            var employeename = JsonConvert.DeserializeObject<ERPNextEmployeeCreated.Root>(response.Content);
            Static.ERPNextEmployeeDesignation = employeename.Data.Name;
            Console.WriteLine(response.Content);
            //Update Password
            var update = new RestRequest("/api/resource/Employee/" + Static.ERPNextEmployeeDesignation, Method.Put);
            update.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            update.AddHeader("Accept", "application/json");
            update.AddHeader("b544d47f25a916a", "079d58b09f6cd01");
            update.AddHeader("Cookie", "full_name=Administrator; sid=" + cookie + "; system_user=yes; user_id=Administrator;");
            update.AddParameter("new_password", fld_first.Text.ToLower() + "." + fld_last.Text.ToLower());
            RestResponse updateresponse = await client.ExecuteAsync(update);
            Console.WriteLine(updateresponse.Content);
        }

        public async Task CreateUserAsync(string cookie, RestClient client)
        {
            var request = new RestRequest("/api/resource/User", Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("b544d47f25a916a", "079d58b09f6cd01");
            request.AddHeader("Cookie", "full_name=Administrator; sid=" + cookie + "; system_user=yes; user_id=Administrator;");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("name", fld_email.Text);
            request.AddParameter("idx", "1");
            request.AddParameter("name", "1");
            request.AddParameter("email", fld_email.Text);
            request.AddParameter("first_name", fld_first.Text);
            request.AddParameter("last_name", fld_last.Text);
            request.AddParameter("full_name", fld_first.Text + " " + fld_last.Text);
            request.AddParameter("user_name", fld_first.Text + "." + fld_last.Text);
            request.AddParameter("country", "Australia");
            request.AddParameter("time_zone", "Australia/Perth");
            request.AddParameter("user_category", "Others");
            request.AddParameter("Send_welcome_email", "1");
            request.AddParameter("unsubscribed", "0");
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
            var update = new RestRequest("api/resource/User/" + fld_email.Text, Method.Put);
            update.AddHeader("Content-Type", "application/json");
            update.AddHeader("Accept", "application/json");
            update.AddHeader("b544d47f25a916a", "079d58b09f6cd01");
            update.AddHeader("Cookie", "full_name=Administrator; sid=" + cookie + "; system_user=yes; user_id=Administrator;");
            var body = @"
" + "\n" +
 @"{
" + "\n" +
 @"    ""roles"": [
" + "\n" +
 @"            {
" + "\n" +
 @"                ""role"": ""Student""
" + "\n" +
 @"            },
" + "\n" +
 @"            {
" + "\n" +
 @"                ""role"": ""Employee""
" + "\n" +
 @"            }
" + "\n" +
 @"    ]
" + "\n" +
 @"}";
            update.AddStringBody(body, DataFormat.Json);
            RestResponse updateresponse = await client.ExecuteAsync(update);
            Console.WriteLine(updateresponse.Content);

            var password = new RestRequest("api/resource/User/" + fld_email.Text, Method.Put);
            password.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            password.AddHeader("Accept", "application/json");
            password.AddHeader("b544d47f25a916a", "079d58b09f6cd01");
            password.AddHeader("Cookie", "full_name=Administrator; sid=" + cookie + "; system_user=yes; user_id=Administrator;");
            password.AddParameter("new_password", fld_first.Text.ToLower() + "." + fld_last.Text.ToLower());
            RestResponse passresponse = await client.ExecuteAsync(password);
            Console.WriteLine(passresponse.Content);
            CreateWorkerBtn.Text = "Create Worker";
            MessageBox.Show(@"New Worker Added. SMS's can now be send for onboarding", @"Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Job Pack Compiler
        private void GenerateCover_Button_Click(object sender, EventArgs e)
        {
            Cover_Letter_Format();
        }
        private void AddToJobPack_Button_Click(object sender, EventArgs e)
        {
            //DownloadDocumentFromUrl(Static.AssignarDashboardUrl + "projects/" + Static.ProjectNumber + "/documents/",Static.JwtToken);
            DownloadFilesForJobPack(JobDocuments_ComboBox.Text);
        }
        private void GenerateJobPack_Button_Click(object sender, EventArgs e)
        {
            switch (GenerateJobPack_Button.Text)
            {
                case @"GENERATE":
                    CombineJobPackFiles();
                    _filestep = 0;
                    JobDocuments_ListBox.Items.Clear();
                    break;
            }

        }
        private void JobDocuments_ComboBox_DropDown(object sender, EventArgs e)
        {
            var senderComboBox = (ComboBox)sender;
            var width = senderComboBox.DropDownWidth;
            var g = senderComboBox.CreateGraphics();
            var font = senderComboBox.Font;
            var vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                    ? SystemInformation.VerticalScrollBarWidth : 0;

            width = (from string s in ((ComboBox)sender).Items select (int)g.MeasureString(s, font).Width + vertScrollBarWidth).Prepend(width).Max();
            senderComboBox.DropDownWidth = width;
        }
        #endregion

        #region Recruitment
        private void Generate_Contract_Button_Click(object sender, EventArgs e)
        {
            ProgressBar_Compiler.Value = 0;
            ProgressBar_Compiler.PerformStep();
            Directory.CreateDirectory(Output + "\\" + New_Employee_Name_TextBox.Text);
            ProgressBar_Compiler.PerformStep();
            GenerateNewEmployeeContract(Employment_Type_ComboBox.Text, EmploymentContracts);
            ProgressBar_Compiler.Value = 100;
        }
        private void Generate_New_Employee_Pack_Button_Click(object sender, EventArgs e)
        {
            ProgressBar_Compiler.Value = 0;
            ProgressBar_Compiler.PerformStep();
            GenerateNsp();
            ProgressBar_Compiler.PerformStep();
            GenerateNewEmployeePack();
            ProgressBar_Compiler.Value = 100;
        }
        #endregion

        #region Document Generator
        private void DocumentForComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_documentref))
            {
                DocRefTextBox.Text = CompanyPreFix() + OpenFileDialog.SafeFileName?.Split(' ')[0];
            }
        }
        private void OpenDocumentButton_Click(object sender, EventArgs e)
        {
            FindDocument();
        }
        private void Generate_Button_Click(object sender, EventArgs e)
        {
            GenButtonTextSwitch();
        }

        #endregion

        #region Client Signed Timesheet
        private void Cst_btn_Click(object sender, EventArgs e)
        {
            //DownloadTimesheets(Static.AssignarDashboardUrl, "/v2/timesheets?project_id=" + cst_project.Text, Static.JwtToken);
        }
        #endregion

        #endregion

        #region Closing Form
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            GC.Collect();
        }


        #endregion

        public async void TestButton_Click(object sender, EventArgs e)
        {
            string equipment = "Diesel Welder Generator;Welding Machine - Caddy;Flextechs 350A;LN25 Wire Feeder;ADFLO Welding Helmet;400CFM Compressor Trailer Mounted;185CFM Air Compressor;ThermoCouples;Laser Alignment Machine & Laptop;14T Tool Truck;Tool Trailer;Bearing Inspection Trailer;Fuel Trailer;Box Trailer 6 x 10;Light Boilermakers Truck;Medium Tool Box (4 Man Crew);Life Buoys;Site Box Mechanical;Plasma Cutter;Oxy/Acetylene Kit cw/ Gas;Straight Line Gas Cutter;Sandblasting Set up;Painting Set Up;Arc Air Gouging Equipment;Specialised Measuring Equipment;Thermal Lance;Pin Push/Pull Jacking Jig;80 kVA Generator;Gas Tester;Knack Box - Plasma;Knack Box - Rigging - 5T;Knack Box - Rigging - 5T-10T;Knack Box - Rigging - 20T-50T;10T CHAIN BLOCK 6M DROP;20T CHAIN BLOCK 6M DROP;3.2T Turfer;9T Turfer;6T Air Winch 20m Drop;Thread Cutting Machine;Trestles Certified EA;Knack Box - Electrical - 10A-15A;Knack Box - Electrical - 32A-64A;Knack Box - Hydraulic - 5T-20T;Knack Box - Hydraulic - 30T-50T;Knack Box - Hydraulic - 60T-100T;Hydration Station;2.5T Forklift;LV - Tooled;LV - Untooled;12 Seat Bus;BUS 19 SEATER;20T Franna;4T Truck (Dual Cab);4T Truck (Dual Cab) - Tooled;9T Truck (Flat Bed);12T Truck";
            List<string> equipmentList = equipment.Split(';').ToList();
            try
            {
                foreach (string item in equipmentList)
                {
                    string body = "{" +
                   ",\n  \"taxonomy\": \"machine_tag\"" +
                   ",\n  \"name\": \"" + item + "\"" +
                   ",\n  \"description\": " + item + " Tag for Reporting Purposes." + "\"" +
                   ",\n  \"color\": \"#FFFFFF\"" +
                   "\n}";
                    await AssignarAPIConnect("/tags/", Method.Post, body);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Whoops! An Error has occurred trying to navigate to your Project. The error is as follows:" + @"

" + ex, @"Error");
            }
        }
    }
}