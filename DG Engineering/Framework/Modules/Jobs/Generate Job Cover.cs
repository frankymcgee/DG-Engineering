using System.IO;
using Microsoft.Office.Interop.Word;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using Application = Microsoft.Office.Interop.Word.Application;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Generates Job Cover Letters & checklists for Internal Use.
        /// </summary>
        private void JobCoverLetterChecklist()
        {
            StatusLabel.Visible = true;
            StatusLabel.Text = @"Creating Job Cover Letter";
            var word = new Application();
            var doc = word.Documents.Add(JobCoverPath);
            doc.Activate();
            foreach (FormField field in doc.FormFields)
            {
                switch (field.Name)
                {
                    case "JobNo":
                        field.Range.Text = Jobs_ProjectNumber.Text;
                        break;
                    case "JobPoNo":
                        field.Range.Text = Jobs_PoNo.Text;
                        break;
                    case "JobClient":
                        field.Range.Text = Jobs_Client.Text;
                        break;
                    case "JobSite":
                        field.Range.Text = Jobs_Site.Text;
                        break;
                    case "JobDesc":
                        field.Range.Text = Jobs_JobName.Text;
                        break;
                }
            }
            var output = Path.Combine(Path.GetTempPath(), "Job Cover Letter\\");
            Directory.CreateDirectory(output);
            StatusLabel.Text = @"Exporting Job Cover Letter and Checklist. Please Wait";
            doc.ExportAsFixedFormat(output + Jobs_ProjectNumber.Text + " Job Cover Letter and Checklist.pdf", WdExportFormat.wdExportFormatPDF);
            doc.Close(false);
            word.Quit();
            ReleaseComObjects(doc, word);
            // Open the output document
            var outputDocument = new PdfDocument();
            
                // Open the document to import pages from it.
                var inputDocument = PdfReader.Open(output + Jobs_ProjectNumber.Text + " Job Cover Letter and Checklist.pdf", PdfDocumentOpenMode.Import);
                // Iterate pages
                var count = inputDocument.PageCount;
                for (var idx = 0; idx < count; idx++)
                {
                    switch (idx >= 1)
                    {
                        case true:
                        {
                            // Get the page from the external document...
                            var page = inputDocument.Pages[idx];
                            // ...and add it to the output document.
                            outputDocument.AddPage(page);
                            break;
                        }
                    }
                }
                inputDocument.Close();
                StatusLabel.Text = @"Saving Document";
            // Save the document...
            var filename = output + Jobs_ProjectNumber.Text + " Job Pack Cover.pdf";
            outputDocument.Save(filename);
            // ...and start a viewer.
            Jobs_Viewer.CoreWebView2.Navigate(filename);
            StatusLabel.Visible = false;
        }
    }
}