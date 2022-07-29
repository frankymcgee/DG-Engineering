using System;
using System.Collections.Generic;
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
        /// Will Grab all Inputs from the Document Generator Tab and will output a .pdf document for printing / emailing.
        /// </summary>
        /// <param name="document">The Path to the Document Template being created from.</param>
        private void UpdateDoc(object document)
        {
            var prefix = CompanyPreFix();
            _wordApp = new Application();
            var appDocuments = _wordApp.Documents;
            var aDoc = appDocuments.Open(document);
            var headerRanges = new List<Range>();
            var signRanges = new List<Range>();
            var pictureRanges = new List<Range>();
            //Logo Header Replacement
            foreach (var s in aDoc.Sections.Cast<Section>()
                         .Select(section => section.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary])
                         .SelectMany(headerfooter =>
                             headerfooter.Range.InlineShapes.Cast<InlineShape>()
                                 .Where(s => s.AlternativeText == "Logo")))
            {
                headerRanges.Add(s.Range);
                var headerheight = s.Height;
                var headerwidth = s.Width;
                s.Delete();
                //Logo Replacement
                foreach (var ils in headerRanges.Select(r =>
                             r.InlineShapes.AddPicture(_picture, ref _missing, ref _missing, ref _missing)))
                {
                    ils.Height = headerheight;
                    ils.Width = headerwidth;
                }
            }

            //signature Search
            foreach (var s in aDoc.InlineShapes.Cast<InlineShape>()
                         .Where(s => s.Type == WdInlineShapeType.wdInlineShapePicture)
                         .Where(s => s.AlternativeText == "Signature"))
            {
                signRanges.Add(s.Range);
                var signheight = s.Height;
                var signwidth = s.Width;
                s.Delete();
                //Signature Replacement
                foreach (var ils in signRanges.Select(r =>
                             r.InlineShapes.AddPicture(Path.Combine(_signature), ref _missing, ref _missing,
                                 ref _missing)))
                {
                    ils.Height = signheight;
                    ils.Width = signwidth;
                }
            }

            //Picture Search
            foreach (var s in aDoc.InlineShapes.Cast<InlineShape>()
                         .Where(s => s.Type == WdInlineShapeType.wdInlineShapePicture)
                         .Where(s => s.AlternativeText != "Signature"))
            {
                pictureRanges.Add(s.Range);
                var scaledHeight = s.Height;
                var scaledWidth = s.Width;
                //Picture Replacement
                foreach (var p in pictureRanges.Select(r =>
                             r.InlineShapes.AddPicture(Path.Combine(PreStartPath, VehicleComboBox.Text, "Pictures",
                                 s.AlternativeText, "image.jpg"))))
                {
                    p.Height = scaledHeight;
                    p.Width = scaledWidth;
                }

                pictureRanges.Clear();
                s.Delete();
            }

            //Custom Fields Replacement
            foreach (Field myMergeField in aDoc.Fields)
            {
                var rngFieldCode = myMergeField.Code;
                var fieldText = rngFieldCode.Text;
                if (fieldText.StartsWith(" MERGEFIELD"))
                {
                    var endMerge = fieldText.IndexOf("\\", StringComparison.Ordinal);
                    var fieldName = fieldText.Substring(11, endMerge - 11);
                    fieldName = fieldName.Trim();
                    switch (fieldName)
                    {
                        case "signature":
                            myMergeField.Select();
                            _wordApp.Selection.TypeText("Electronic Signature");
                            break;
                        case "officer_name":
                            myMergeField.Select();
                            _wordApp.Selection.TypeText(_officerName);
                            break;
                        case "officer_position":
                            myMergeField.Select();
                            _wordApp.Selection.TypeText(_officerPosition);
                            break;
                        case "auth_name":
                            myMergeField.Select();
                            _wordApp.Selection.TypeText("Sean Long");
                            break;
                        case "auth_role":
                            myMergeField.Select();
                            _wordApp.Selection.TypeText("Business Support");
                            break;
                        case "vehicle":
                            myMergeField.Select();
                            _wordApp.Selection.TypeText(VehicleComboBox.Text);
                            break;
                    }
                }
            }

            //Built-in Fields Replacement
            aDoc.BuiltInDocumentProperties["Subject"].Value = DocRefTextBox.Text;
            aDoc.BuiltInDocumentProperties["Category"].Value = DocTypeTextBox.Text;
            aDoc.BuiltInDocumentProperties["Title"].Value = DocTitle_TextBox.Text;
            aDoc.BuiltInDocumentProperties["Keywords"].Value = prefix;
            aDoc.BuiltInDocumentProperties["Company"].Value = DocumentForComboBox.Text;

            try
            {
                if (DocTitle_TextBox.Text.Contains("Pre Start"))
                {
                    aDoc.SaveAs2(Path.GetTempPath() + DocTypeTextBox.Text + " " + VehicleComboBox.Text + ".docx",
                        WdSaveFormat.wdFormatDocumentDefault);
                    ReleaseComObjects(aDoc, _wordApp);
                    DocumentGeneratorViewer.Navigate(
                        Path.GetTempPath() + DocTypeTextBox.Text + " " + VehicleComboBox.Text + ".docx");
                    DocumentGeneratorViewer.Navigate("about:blank");
                }
                else
                {
                    aDoc.ExportAsFixedFormat(DocTypeTextBox.Text + ".pdf", WdExportFormat.wdExportFormatPDF);
                    ReleaseComObjects(aDoc, _wordApp);
                    DocumentGeneratorViewer.Navigate(Path.GetTempPath() + DocTypeTextBox.Text + ".pdf");
                }
            }
            catch (Exception e)
            {
                DocumentGeneratorViewer.Navigate("about:blank");
                MessageBox.Show(@"Error:" + @"

" + e.Message,@"Attention",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
    }
}