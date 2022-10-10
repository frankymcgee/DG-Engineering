using System.IO;
using System.Linq;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.Office.Interop.Word;
using PdfSharp.Pdf.IO;
using Document = iText.Layout.Document;
using PdfSharpDocument = PdfSharp.Pdf.PdfDocument;
using PdfSharpReader = PdfSharp.Pdf.IO.PdfReader;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Generates the Job Pack.
        /// </summary>
        private void CombineJobPackFiles()
        {
            StatusLabel.Visible = true;
            StatusLabel.Text = @"Combining Information. Please Wait...";
            // Get some file names
            var files = GetFiles(Path.GetTempPath() + "Job Pack Generator\\");
            // Open the output document
            var outputDocument = new PdfSharpDocument();
            // Iterate files
            foreach (var file in files)
            {
                if (file.EndsWith(".pdf"))
                {
                    // Open the document to import pages from it.
                    var inputDocument = PdfSharpReader.Open(file, PdfDocumentOpenMode.Import);
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
                else if (file.EndsWith(".jpg") || file.EndsWith(".jpeg") || file.EndsWith(".png"))
                {
                    // Must have write permissions to the path folder
                    var writer = new PdfWriter(file + ".pdf");
                    var pdf = new PdfDocument(writer);
                    var document = new Document(pdf);
                    // Add image
                    var img = new Image(ImageDataFactory.Create(file)).SetTextAlignment(TextAlignment.CENTER);
                    document.Add(img);
                    document.Close();
                    // Open the document to import pages from it.
                    var inputDocument = PdfSharpReader.Open(file + ".pdf", PdfDocumentOpenMode.Import);
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
                else if (file.EndsWith(".doc") || file.EndsWith(".docx"))
                {
                    var wordname = file.Split('\\').Last();
                    var word = new Application();
                    var doc = word.Documents.Open(file);
                    doc.Activate();
                    doc.ExportAsFixedFormat(Path.GetTempPath() + "Job Pack Generator\\" + wordname + ".pdf",WdExportFormat.wdExportFormatPDF);
                    ReleaseComObjects(doc,word);
                    // Open the document to import pages from it.
                    var inputDocument = PdfSharpReader.Open(Path.GetTempPath() + "Job Pack Generator\\"+ wordname + ".pdf", PdfDocumentOpenMode.Import);
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
            }
            
            StatusLabel.Text = @"Saving Document";
            // Save the document...
            var filename = Path.GetTempPath() + JobPackNo_TextBox.Text + " Job Pack.pdf";
            _fileUploadName = JobPackNo_TextBox.Text + " Job Pack.pdf";
            outputDocument.Save(filename);
            // ...and start a viewer.
            JobPackBrowser.Navigate(filename);
            outputDocument.Close();
            //Deletes the Generator Folder including anything inside.
            //Directory.Delete(Path.GetTempPath() + "Job Pack Generator\\", true);
            StatusLabel.Visible = false;
        }
    }
}