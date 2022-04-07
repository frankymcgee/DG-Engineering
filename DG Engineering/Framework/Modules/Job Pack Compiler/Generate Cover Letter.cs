using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using Application = Microsoft.Office.Interop.Word.Application;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Generates Job Pack Cover Letters for Mob Packs.
        /// </summary>
        public void Cover_Letter_Format()
        {
            var word = new Application();
            var doc = word.Documents.Add(CoverLetterPath);
            doc.Activate();
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
            doc.ExportAsFixedFormat(output + +Filestep + ". Job Pack Cover.pdf",
                WdExportFormat.wdExportFormatPDF);
            doc.Close(false);
            word.Quit();
            ReleaseComObjects(doc, word);
            DownloadJobDocuments(Static.AssignarDashboardUrl + "projects/" + Manual_Search_TextBox.Text + "/documents/",
                Static.JwtToken);
            JobDocuments_ListBox.Items.Add(Filestep + ". Job Pack Cover");
            MessageBox.Show(@"Completed", @"Cover for Pack Completed");
        }
    }
}