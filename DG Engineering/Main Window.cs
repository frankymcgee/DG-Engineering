using System;
using System.IO;
using System.Windows.Forms;

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
            await JobsTabViewer.EnsureCoreWebView2Async();
            await ScheduleViewer.EnsureCoreWebView2Async();
            await ProjectViewer.EnsureCoreWebView2Async();
            await RecruitmentViewer.EnsureCoreWebView2Async();
            ScheduleViewer.CoreWebView2.Navigate("https://dashboard.assignar.com.au/scheduler/timeline");
            ProgressBar_Compiler.Step = 25;
            Assignar_Tabs.TabPages.Remove(Clients_Tab);
            Assignar_Tabs.TabPages.Remove(Fieldworkers_Tab);
            Assignar_Tabs.TabPages.Remove(Jobs_Tab);
            Assignar_Tabs.TabPages.Remove(Job_Pack_tab);
            DownloadAllProjects(Static.AssignarDashboardUrl + "projects/", Static.JwtToken);
            DownloadRoleDescriptions(Static.AssignarDashboardUrl + "tasks/", Static.JwtToken);
        }
        #endregion
        #region Modules
        #region SimPro to Assignar
        private void SimProQuoteSearch_Click(object sender, EventArgs e)
        {
            SimProSearch();
        } private void PushAssignar_Button_Click(object sender, EventArgs e)
        {
            AssignarProjectPost();
        }
        #endregion
        #region Jobs
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
                Static.AssignarDashboardUrl + "projects/" + Manual_Search_TextBox.Text + "/documents/",
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
