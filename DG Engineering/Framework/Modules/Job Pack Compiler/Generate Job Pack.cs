using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Generates the Job Pack.
        /// </summary>
        private void CombineJobPackFiles()
        {
            // Get some file names
            var files = GetFiles(Path.GetTempPath() + "Job Pack Generator\\");
            // Open the output document
            var outputDocument = new PdfDocument();
            // Iterate files
            foreach (var file in files)
            {
                if (!file.EndsWith(".pdf")) continue;
                // Open the document to import pages from it.
                var inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);
                // Iterate pages
                var count = inputDocument.PageCount;
                for (var idx = 0; idx < count; idx++)
                {
                    // Get the page from the external document...
                    var page = inputDocument.Pages[idx];
                    // ...and add it to the output document.
                    outputDocument.AddPage(page);
                }
            }

            // Save the document...
            var filename = Path.GetTempPath() + JobPackNo_TextBox.Text + " Job Pack.pdf";
            FileUploadName = JobPackNo_TextBox.Text + " Job Pack.pdf";
            outputDocument.Save(filename);
            // ...and start a viewer.
            JobPackBrowser.Navigate(filename);
            outputDocument.Close();
            //Deletes the Generator Folder including anything inside.
            Directory.Delete(Path.GetTempPath() + "Job Pack Generator\\", true);
        }
    }
}