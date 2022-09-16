using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            switch (Debugger.IsAttached)
            {
                case false:
                    CreateFromGanttButton.Visible = false;
                    break;
                case true:
                    WipeCleanButton.Visible = true;
                    break;
            }

            VersionLabel.Text = @"Version: " + version + @"  |    Connected to: " + Static.ClientId + @"  |    ";
            var environment = await CoreWebView2Environment.CreateAsync(null, Path.GetTempPath());
            await AdminViewer.EnsureCoreWebView2Async(environment);
            await ScheduleViewer.EnsureCoreWebView2Async(environment);
            await ProjectViewer.EnsureCoreWebView2Async(environment);
            await RecruitmentViewer.EnsureCoreWebView2Async(environment);
            await Jobs_Viewer.EnsureCoreWebView2Async(environment);
            ScheduleViewer.CoreWebView2.Navigate("https://dashboard.assignar.com.au/scheduler/timeline");
            ProgressBar_Compiler.Step = 25;
            Assignar_Tabs.TabPages.Remove(Clients_Tab);
            Assignar_Tabs.TabPages.Remove(Fieldworkers_Tab);
            Assignar_Tabs.TabPages.Remove(DocumentGen_Tab);
            DownloadRoleDescriptions(Static.AssignarDashboardUrl + "tasks/", Static.JwtToken);
            DownloadClientList(Static.AssignarDashboardUrl + "clients/", Static.JwtToken);
            var timer = new Timer
            {
                AutoReset = true,
                Enabled = true,
                Interval = 3600000,
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
            progressbar.Elapsed += ProgressbarOnElapsed;
        }

        private void ProgressbarOnElapsed(object sender, ElapsedEventArgs e)
        {
            if (ProgressBar.ProgressBar != null && ProgressBar.ProgressBar.Value > 0)
            {
                ProgressBar.ProgressBar.Value = 0;
            }
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            RefreshMyob();
        }
        #endregion

        #region Modules

            #region Project Creation
        private async void JobNumberSearch_Click(object sender, EventArgs e)
        {
            await MyobSearch(ProjectJobNumber.Text);
        }
        private void PushAssignar_Button_Click(object sender, EventArgs e)
        {
            if (ProjectPONumber.Text == @"MISSING PO NUMBER")
            {
                MessageBox.Show(@"PLEASE ADD PROJECT PO NUMBER",@"Attention",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                CompanyIdExtract(ProjectClient.Text);
                AssignarProjectPost();
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
            if (result != null)
                foreach (var b in result.Items.SelectMany(a => a.Addresses))
                {
                    ProjectAddress.Items.Add(b.Street + ", " + b.City + ", " + b.State + ", " + b.PostCode);
                }

            ClientContact.Items.Clear();
            var contactsquery = await AssignarConnect(Static.AssignarDashboardUrl + "contacts?company=" + ProjectClient.Text,Static.JwtToken,Method.GET,null);
            var contactsresult = JsonConvert.DeserializeObject<Contacts.Root>(contactsquery);
            if (contactsresult != null)
                foreach (var a in contactsresult.Data)
                {
                    ClientContact.Items.Add(a.FirstName + " " + a.LastName + " - " + a.JobTitle);
                }
        }
        private void PushToJobPackButton_Click(object sender, EventArgs e)
        {
            PushToJobPack();
        }

        private void CreateFromGanttButton_Click(object sender, EventArgs e)
        {
            if (ProjectPONumber.Text == @"MISSING PO NUMBER")
            {
                MessageBox.Show(@"PLEASE ADD PROJECT PO NUMBER",@"Attention",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                var openFileDialog1 = new OpenFileDialog
                {
                    Site = null,
                    Tag = null,
                    AddExtension = false,
                    CheckPathExists = false,
                    DefaultExt = null,
                    DereferenceLinks = false,
                    FileName = null,
                    Filter = @"Project File (.mpp)|*.mpp|All files (*.*)|*.*",
                    FilterIndex = 1,
                    InitialDirectory = null,
                    RestoreDirectory = false,
                    ShowHelp = false,
                    SupportMultiDottedExtensions = false,
                    Title = null,
                    ValidateNames = false,
                    AutoUpgradeEnabled = false,
                    CheckFileExists = false,
                    Multiselect = false,
                    ReadOnlyChecked = false,
                    ShowReadOnly = false
                };
                // Call the ShowDialog method to show the dialog box.
                openFileDialog1.ShowDialog();
                CompanyIdExtract(ProjectClient.Text);
                AssignarProjectPostfromGantt(openFileDialog1.FileName);
            }
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
        private void WipeCleanButton_Click(object sender, EventArgs e)
        {
            ClearOrders();
        }

        public async void ClearOrders()
        {
            var jobsearch =  await AssignarConnect(Static.AssignarDashboardUrl + "orders", Static.JwtToken, Method.GET, null);
            var jobinfo = JsonConvert.DeserializeObject<OrdersList.Root>(jobsearch);
            var projectsearch = await AssignarConnect(Static.AssignarDashboardUrl + "projects/", Static.JwtToken, Method.GET, null);
            var projectinfo = JsonConvert.DeserializeObject<Projects.Project>(projectsearch);
            var message = MessageBox.Show(@"There are " + projectinfo.Count + @" Projects and " + jobinfo.Count +  @" Orders. Are you sure you wish to remove all of these?", @"Attention", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (message != DialogResult.Yes) return;
            foreach (var a in jobinfo.Data)
            {
                await AssignarConnect(Static.AssignarDashboardUrl + "orders/" + a.Id, Static.JwtToken, Method.DELETE, null);
            }

            foreach (var b in projectinfo.Data)
            {
                await AssignarConnect(Static.AssignarDashboardUrl + "projects/" + b.Id, Static.JwtToken, Method.DELETE, null);
            }
        }

    }
}