using System;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Generates New Employee Pack.
        /// </summary>
        private void GenerateNewEmployeePack()
        {
            ProgressBar_Compiler.PerformStep();
            var outputDocument = new PdfDocument();
            var filesArray = new string[6];
            filesArray[0] = CompilePath + "DGE-HR-FRM-NSP.pdf";
            filesArray[1] = CompilePath + "Role Descriptions\\" + Job_Position_ComboBox.Text + ".pdf";
            filesArray[2] = CompilePath + "DGE - Fair Work Information Statement.pdf";
            filesArray[3] = CompilePath + "DGE - Tax Declaration Form.pdf";
            filesArray[4] = CompilePath + "DGE - Superannuation Standard Choice Form.pdf";
            filesArray[5] = CompilePath + "DGE-HSEQ-HR-FRM-CTH.pdf";
            foreach (var file in filesArray)
            {
                try
                {
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
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            var filename = Path.GetTempPath() + New_Employee_Name_TextBox.Text + " New Starter Pack.pdf";
            outputDocument.Save(filename);
            WebBrowserControl.Navigate(filename);
            outputDocument.Close();
            ProgressBar_Compiler.PerformStep();
            ProgressBar_Compiler.Value = 0;
        }
    }
}