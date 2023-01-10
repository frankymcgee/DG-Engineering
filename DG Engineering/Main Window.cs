using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Timers;
using System.Windows.Forms;
using DG_Engineering.Framework.Global.Assignar;
using DG_Engineering.Framework.Global.MYOB;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using RestSharp;
using Timer = System.Timers.Timer;

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
                version =  System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
            catch(Exception)
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
            Assignar_Tabs.TabPages.Remove(Clients_Tab);
            Assignar_Tabs.TabPages.Remove(DocumentGen_Tab);
            DownloadRoleDescriptions(Static.AssignarDashboardUrl + "tasks/", Static.JwtToken);
            DownloadClientList(Static.AssignarDashboardUrl + "clients/", Static.JwtToken);
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

            #region Project Creation
        private async void JobNumberSearch_Click(object sender, EventArgs e)
        {
            await MyobSearch(ProjectJobNumber.Text);
        }
        private async void PushAssignar_Button_Click(object sender, EventArgs e)
        {
            if (ProjectPONumber.Text == @"MISSING PO NUMBER")
            {
                MessageBox.Show(@"PLEASE ADD PROJECT PO NUMBER",@"Attention",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                await CompanyIdExtract(ProjectClient.Text);
                await AssignarProjectPost();
            }
        }
        private async void Address_upd_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ProjectClient.Text)) return;
            var request =
                MyobConnect(
                        Static.Companyfileuri + "/" + Static.Companyfileguid +
                        "/Contact/Customer?$filter=CompanyName eq \'" + ProjectClient.Text + "\'",
                        Method.GET)
                    .Content;
            var result = JsonConvert.DeserializeObject<Customer.Root>(request);
            ProjectAddress.Items.Clear();
            foreach (var b in result.Items.SelectMany(a => a.Addresses))
            {
                ProjectAddress.Items.Add(b.Street + ", " + b.City + ", " + b.State + ", " + b.PostCode);
            }
            ClientContact.Items.Clear();
            var contactsquery = await AssignarConnect(Static.AssignarDashboardUrl + "contacts?company=" + ProjectClient.Text,Static.JwtToken,Method.GET,null);
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
        private void AdminProjButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AdminProjectNumber.Text))
            {
                MessageBox.Show(@"PLEASE ADD PROJECT NUMBER", @"Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                AdminProjSearch();
            }
        }

        private void AdminProjSearch()
        {
            AdminProjectInformation(Static.AssignarDashboardUrl + "projects/" + AdminProjectNumber.Text, Static.JwtToken);
            var url = @"https://dashboard.assignar.com.au/v1/#!/projects/detail/" + Static.AssignarInternalNumber + @"/edit";
            AdminViewer.CoreWebView2.Navigate(url);
        }

        private void AdminDispJobInfo_Click(object sender, EventArgs e)
        {
            AdminJobNo.Text = AdminJobComboBox.Text.Split(" | ".ToCharArray())[0];
            AdminDownloadJobInformation(Static.AssignarDashboardUrl + "orders/" + AdminJobNo.Text, Static.JwtToken);
        }
        #endregion

            #region Schedule
    #endregion

            #region Clients
    #endregion

            #region Fieldworkers
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

        #endregion

        #region Closing Form
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            GC.Collect();
        }


        #endregion

        private void TestButton_Click(object sender, EventArgs e)
        {
            CreateFolderStructure();
        }

        private void JobDocuments_ComboBox_DropDown(object sender, EventArgs e)
        {
            var senderComboBox = (ComboBox)sender;
            var width = senderComboBox.DropDownWidth;
            var g = senderComboBox.CreateGraphics();
            var font = senderComboBox.Font;
            var vertScrollBarWidth = 
                (senderComboBox.Items.Count>senderComboBox.MaxDropDownItems)
                    ?SystemInformation.VerticalScrollBarWidth:0;

            width = (from string s in ((ComboBox) sender).Items select (int) g.MeasureString(s, font).Width + vertScrollBarWidth).Prepend(width).Max();
            senderComboBox.DropDownWidth = width;
        }

        private void WipeCleanButton_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateFieldworkers();
        }

        public void CreateFieldworkers()
        {
            //fld_dob.Format = DateTimePickerFormat.Custom;
            //fld_dob.CustomFormat = @"yyyy-MM-dd";
            //var client = new RestClient("http://dgengineering.com.au/api/resource/User");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.POST);
            //request.AddHeader("b544d47f25a916a", "079d58b09f6cd01");
            //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            //request.AddHeader("Accept", "application/json");
            //request.AddParameter("name", "\"" + fld_first.ToString() + " " + fld_last.ToString() + "\"");
            //request.AddParameter("enabled", "1");
            //request.AddParameter("email", "\"" + fld_email + "\"");
            //request.AddParameter("first_name", "\"" + fld_first.ToString() + "\"");
            //request.AddParameter("last_name", "\"" + fld_last.ToString() + "\"");
            //request.AddParameter("full_name", "\"" + fld_first.ToString() + " " + fld_last.ToString() + "\"");
            //request.AddParameter("username", "\"" + fld_first.ToString() + "." + fld_last.ToString() + "\"");
            //request.AddParameter("country", "Australia");
            //request.AddParameter("time_zone", "Australia/Perth");
            //request.AddParameter("user_category", "Employee");
            //request.AddParameter("send_welcome_email", "1");
            //request.AddParameter("unsubscribed", "1");
            //request.AddParameter("birth_date", "\"" + fld_dob + "\"");
            //IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);

            var client = new RestClient("http://dgengineering.com.au/api/resource/User");
client.Timeout = -1;
var request = new RestRequest(Method.POST);
request.AddHeader("Accept", "application/json");
request.AddHeader("b544d47f25a916a", "079d58b09f6cd01");
request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
request.AddParameter("name", "no thankyou");
request.AddParameter("enabled", "1");
request.AddParameter("email", "sean@webwire.com.au");
request.AddParameter("first_name", "no");
request.AddParameter("last_name", "thankyou");
request.AddParameter("full_name", "no thankyou");
request.AddParameter("username", "no.thankyou");
request.AddParameter("country", "Australia");
request.AddParameter("time_zone", "Australia/Perth");
request.AddParameter("user_category", "Employee");
request.AddParameter("send_welcome_email", "1");
request.AddParameter("unsubscribed", "1");
request.AddParameter("birth_date", "1990-08-15");
IRestResponse response = client.Execute(request);
Console.WriteLine(response.Content);
        }
    }
}