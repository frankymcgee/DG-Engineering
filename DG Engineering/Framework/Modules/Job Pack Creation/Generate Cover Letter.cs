using System;
using System.IO;
using System.Linq;
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
        private void Cover_Letter_Format()
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
            ReleaseComObjects(doc,word);
            JobDocuments_ListBox.Items.Add(_filestep + ". Job Pack Cover");
            StatusLabel.Text = @"Searching for available Documents";
            // Find all documents in Project
            var path = Path.Combine(DGEngineering,"DG Engineering HUB - Operations\\Jobs",JobPackNo_TextBox.Text);
            Console.WriteLine(path);
            var files = Directory.GetFiles(path,"*",SearchOption.AllDirectories);
            StatusLabel.Text = @"Found " + files.Length + @" Documents.";
            foreach (var a in files)
            {
                JobDocuments_ComboBox.Items.Add(a.Split('\\').Reverse().ElementAt(2) + '\\' + a.Split('\\').Reverse().ElementAt(1)+ '\\' + a.Split('\\').Last());
            }
            StatusLabel.Visible = false;
            MessageBox.Show(@"Completed", @"Cover for Pack Completed");
        }
    }
}