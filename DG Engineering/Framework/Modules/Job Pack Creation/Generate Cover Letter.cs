using System.IO;
using System.Windows.Forms;
using DG_Engineering.Framework.Global.Assignar;
using Microsoft.Office.Interop.Word;
using Newtonsoft.Json;
using RestSharp;
using Application = Microsoft.Office.Interop.Word.Application;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Generates Job Pack Cover Letters for Mob Packs.
        /// </summary>
        private async void Cover_Letter_Format()
        {
            StatusLabel.Visible = true;
            StatusLabel.Text = @"Creating Cover Letter";
            var word = new Application();
            var doc = word.Documents.Add(CoverLetterPath);
            doc.Activate();
            StatusLabel.Text = @"Generating Information";
            foreach (FormField field in doc.FormFields)
            {
                switch (field.Name)
                {
                    case "Job_Title":
                        field.Range.Text = JobPackTitle_TextBox.Text;
                        break;
                    case "Job_Week":
                        field.Range.Text = JobPackWeek_TextBox.Text;
                        break;
                    case "Job_Client":
                        field.Range.Text = JobPackClient_TextBox.Text;
                        break;
                    case "Job_Site":
                        field.Range.Text = JobPackSite_TextBox.Text;
                        break;
                    case "Job_Number":
                        field.Range.Text = JobPackNo_TextBox.Text;
                        break;
                    case "Job_PO":
                        field.Range.Text = JobPackPO_TextBox.Text;
                        break;
                }
            }

            var output = Path.Combine(Path.GetTempPath(), "Job Pack Generator\\");
            Directory.CreateDirectory(output);
            StatusLabel.Text = @"Exporting Cover Letter";
            doc.ExportAsFixedFormat(output + +_filestep + ". Job Pack Cover.pdf",
                WdExportFormat.wdExportFormatPDF);
            doc.Close(false);
            word.Quit();
            ReleaseComObjects(doc, word);
            StatusLabel.Text = @"Searching for available Documents";
            // Find all documents in Project
            var projectidsearch = await AssignarConnect(Static.AssignarDashboardUrl + "projects?external_id=" + JobPackNo_TextBox.Text, Static.JwtToken, Method.GET,null);
            var projectnumberresult = JsonConvert.DeserializeObject<ProjectSearch.Root>(projectidsearch);
            StatusLabel.Text = @"Found " + projectnumberresult.Count + @" Documents.";
            foreach (var a in projectnumberresult.Data)
            {
                Static.ProjectNumber = a.Id;
                DownloadJobDocuments(Static.AssignarDashboardUrl + "projects/" + Static.ProjectNumber + "/documents/",
                    Static.JwtToken);
            }

            StatusLabel.Visible = false;
            JobDocuments_ListBox.Items.Add(_filestep + ". Job Pack Cover");
            MessageBox.Show(@"Completed", @"Cover for Pack Completed");
        }
    }
}