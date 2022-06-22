using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using RestSharp;

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
            VersionLabel.Text = @"Version 1.2.017  |    ";
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
            RefreshMyob();
            MyobConnect(Static.companyfileuri + "/" + Static.companyfileguid + "/Sale/Quote", Method.GET);
            var timer = new System.Timers.Timer();
            timer.Interval = 1200000;
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }
        private static void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            RefreshMyob();
        }
        #endregion
        #region Modules
        #region SimPro to Assignar
        private void SimProQuoteSearch_Click(object sender, EventArgs e)
        {
            //SimProSearch();
            MyobSearch();
        } 
        private void PushAssignar_Button_Click(object sender, EventArgs e)
        {
            if (ProjectPOTextBox.Text == @"MISSING PO NUMBER")
            {
                MessageBox.Show(@"PLEASE ADD PROJECT PO NUMBER",@"Attention",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                AssignarProjectPost();
                SendEmail();
            }
        }
        private void DownloadDocButton_Click(object sender, EventArgs e)
        {
            SimProDocDownload();
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


        private void button1_Click(object sender, EventArgs e)
        {
            //ConvertCsvFileToJsonObject("C:\\Users\\seann\\OneDrive - DG Engineering (1)\\Desktop\\test.csv");
        }
        public string ConvertCsvFileToJsonObject(string path) 
        {
            var lines = File.ReadAllLines(path);

            var csv = lines.Select(line => line.Split(',')).ToList();

            var properties = lines[0].Split(',');

            var listObjResult = new List<Dictionary<string, string>>();

            for (var i = 1; i < lines.Length; i++)
            {
                var objResult = new Dictionary<string, string>();
                for (var j = 0; j < properties.Length; j++)
                    objResult.Add(properties[j], csv[i][j]);

                listObjResult.Add(objResult);
            }
            Console.WriteLine(JsonConvert.SerializeObject(listObjResult));
            return JsonConvert.SerializeObject(listObjResult); 
        }
    }
}
