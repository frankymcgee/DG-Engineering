using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Conbine Contract Files.
        /// </summary>
        private void CombineContractFiles()
        {
            File.Copy(EmploymentContracts + "DGE-ADD-LAHA-001.pdf",
                Path.GetTempPath() + New_Employee_Name_TextBox.Text + "\\01. DGE-ADD-LAHA-001.pdf",
                true);
            // Get some file names
            var files = GetFiles(Output + New_Employee_Name_TextBox.Text);
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
            var filename = Path.GetTempPath() + "Contract.pdf";
            outputDocument.Save(filename);
            // ...and start a viewer.
            RecruitmentViewer.CoreWebView2.Navigate(filename);
            outputDocument.Close();
        }
    }
}