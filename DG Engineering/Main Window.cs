using System;
using System.IO;
using System.Linq;
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
        }

        #endregion
        #region Main Window Load
        private async void MainWindow_Load(object sender, EventArgs e)
        {
            VersionLabel.Text = @"Version 1.2.020  |    ";
            var environment = await CoreWebView2Environment.CreateAsync(null, Path.GetTempPath());
            await JobsTabViewer.EnsureCoreWebView2Async(environment);
            await AdminViewer.EnsureCoreWebView2Async(environment);
            await ScheduleViewer.EnsureCoreWebView2Async(environment);
            await ProjectViewer.EnsureCoreWebView2Async(environment);
            await RecruitmentViewer.EnsureCoreWebView2Async(environment);
            ScheduleViewer.CoreWebView2.Navigate("https://dashboard.assignar.com.au/scheduler/timeline");
            ProgressBar_Compiler.Step = 25;
            Assignar_Tabs.TabPages.Remove(Clients_Tab);
            Assignar_Tabs.TabPages.Remove(Fieldworkers_Tab);
            Assignar_Tabs.TabPages.Remove(Jobs_Tab);
            Assignar_Tabs.TabPages.Remove(DocumentGen_Tab);
            DownloadAllProjects(Static.AssignarDashboardUrl + "projects/", Static.JwtToken);
            DownloadRoleDescriptions(Static.AssignarDashboardUrl + "tasks/", Static.JwtToken);
            DownloadClientList(Static.AssignarDashboardUrl + "clients/", Static.JwtToken);
            var timer = new Timer
            {
                AutoReset = true,
                Enabled = true,
                Interval = 1200000,
                Site = null,
                SynchronizingObject = null
            };
            timer.Elapsed += OnTimedEvent;
        }
        private static void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            RefreshMyob();
        }
        #endregion
        #region Modules
        #region Project Creation
        private void JobNumberSearch_Click(object sender, EventArgs e)
        {
            MyobSearch();
        }
        private void PushAssignar_Button_Click(object sender, EventArgs e)
        {
            if (ProjectPONumber.Text == @"MISSING PO NUMBER")
            {
                MessageBox.Show(@"PLEASE ADD PROJECT PO NUMBER",@"Attention",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(ProjectJobNumber.Text))
                {
                    var dialog = MessageBox.Show(@"There is no Job Number. Continue?", "Exit",MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        CompanyIdExtract(ProjectClient.Text);
                        AssignarProjectPost();
                    }
                    else if (dialog == DialogResult.No)
                    {
                    }
                }
                else
                {
                    CompanyIdExtract(ProjectClient.Text);
                    AssignarProjectPost();
                }
            }
        }
        private void Address_upd_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ProjectClient.Text)) return;
            var request =
                MyobConnect(
                        Static.Companyfileuri + "/" + Static.Companyfileguid +
                        "/Contact/Customer?$filter=CompanyName eq \'" + ProjectClient.Text + "\'",
                        Method.GET)
                    .Content;
            Console.WriteLine(request);
            var result = JsonConvert.DeserializeObject<Customer.Root>(request);
            ProjectAddress.Items.Clear();
            foreach (var b in result.Items.SelectMany(a => a.Addresses))
            {
                ProjectAddress.Items.Add(b.Street + ", " + b.City + ", " + b.State + ", " + b.PostCode);
            }
            ClientContact.Items.Clear();
            var contactsquery = AssignarConnect(Static.AssignarDashboardUrl + "contacts?company=" + ProjectClient.Text,Static.JwtToken,RestSharp.Method.GET,null);
            var contactsresult = JsonConvert.DeserializeObject<Contacts.Root>(contactsquery);
            foreach (var a in contactsresult.Data)
            {
                ClientContact.Items.Add(a.FirstName + " " + a.LastName + " - " + a.JobTitle);
            }
        }
        #endregion
        #region Jobs
        private void PushToJobPackButton_Click(object sender, EventArgs e)
        {
            PushToJobPack();
        }
        private void Job_Search_Button_Click(object sender, EventArgs e)
        {
            DownloadProjectInformation(Static.AssignarDashboardUrl + "projects/" + Manual_Search_TextBox.Text,
                Static.JwtToken);
        }
        private void All_Projects_Button_Click(object sender, EventArgs e)
        {
            var project = All_Projects_ComboBox.Text.Split(Convert.ToChar("-"));
            DownloadProjectInformation(Static.AssignarDashboardUrl + "projects/" + project[0],
                Static.JwtToken);
        }
        private void PushToJobPack_Button_Click(object sender, EventArgs e)
        {
            PushDataToJobPackGenerator();
        }
        private void Display_Job_Info_button_Click(object sender, EventArgs e)
        {
            DownloadJobInformation(
                Static.AssignarDashboardUrl + "orders/" + Job_List_ComboBox.Text.Split(Convert.ToChar("-"))[0],
                Static.JwtToken);
        }
        #endregion
        #region Administration
        private void AdminProjButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AdminJobNumber.Text))
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
            AdminProjectInformation(Static.AssignarDashboardUrl + "projects?external_id=" + AdminJobNumber.Text, Static.JwtToken);
            var url = @"https://dashboard.assignar.com.au/v1/#!/projects/detail/" + Static.AssignarInternalNumber + @"/edit";
            Console.WriteLine(url);
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
            DownloadDocumentFromUrl(
                Static.AssignarDashboardUrl + "projects/" + Static.ProjectNumber + "/documents/",
                Static.JwtToken);
        }
        private void GenerateJobPack_Button_Click(object sender, EventArgs e)
        {
            switch (GenerateJobPack_Button.Text)
            {
                case @"GENERATE":
                    CombineJobPackFiles();
                    Filestep = 0;
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

    }
}