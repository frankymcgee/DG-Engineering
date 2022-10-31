using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
//using Application = Microsoft.Office.Interop.Word.Application;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Creates an Open Windows Box for Selecting the Job Description.
        /// </summary>
        /// <returns></returns>
        // ReSharper disable once UnusedMethodReturnValue.Local
        private static string JobDescriptionExtract()
        {
            try
            {
                // Create an instance of the Open File Dialog Box
            var openFileDialog1 = new OpenFileDialog
            {
                Site = null,
                Tag = null,
                AddExtension = false,
                CheckPathExists = false,
                DefaultExt = null,
                DereferenceLinks = false,
                FileName = null,
                Filter = @"Word Documents (.docx)|*.docx|All files (*.*)|*.*",
                FilterIndex = 1,
                InitialDirectory = null,
                RestoreDirectory = false,
                ShowHelp = false,
                SupportMultiDottedExtensions = false,
                Title = null,
                ValidateNames = false,
                AutoUpgradeEnabled = false,
                CheckFileExists = false,
                Multiselect = false,
                ReadOnlyChecked = false,
                ShowReadOnly = false
            };
            // Call the ShowDialog method to show the dialog box.
            openFileDialog1.ShowDialog();
            var word = new Microsoft.Office.Interop.Word.Application();
            object miss = System.Reflection.Missing.Value;
            object path = openFileDialog1.FileName;
            object readOnly = true;
            var docs = word.Documents.Open(ref path, ref miss, ref readOnly,
                ref miss, ref miss, ref miss, ref miss,
                ref miss, ref miss, ref miss, ref miss,
                ref readOnly, ref miss, ref miss, ref miss,
                ref miss);
            // Datatable to store text from Word doc
            var dt = new System.Data.DataTable();
            dt.Columns.Add("Text");

            // Loop through each table in the document, 
            // grab only text from cells in the first column
            // in each table.
            foreach (Table tb in docs.Tables) {
                for (int row = 1; row <= tb.Rows.Count; row++) {
                    var cell = tb.Cell(row, 1);
                    _retrievedText = cell.Range.FormattedText.WordOpenXML;
                    // text now contains the content of the cell.
                }
            }
            docs.Close();
            word.Quit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return _retrievedText;
        }
    }

}