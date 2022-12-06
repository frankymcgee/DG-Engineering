using System;
using System.IO;
using System.Linq;
using Microsoft.Office.Interop.Word;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Generates Employee Contracts
        /// </summary>
        /// <param name="employmenttype">Employment Type</param>
        /// <param name="employmentcontracts">Path to the Employment Contract Templates</param>
        private void GenerateNewEmployeeContract(string employmenttype, string employmentcontracts)
        {
            StatusLabel.Visible = true;
            StatusLabel.Text = @"Generating Employee Contract";
            switch (RepresentativeComboBox.Text)
            {
                    case "Damien Voigt":
                    _signature = Path.Combine(SignPath, @"damien.png");
                    break;
                    case "Leigh Wright":
                    _signature = Path.Combine(SignPath, @"leigh.png");
                    break;
            }
            var filename = Path.GetTempPath();
            var signRanges = new System.Collections.Generic.List<Range>();
            JobDescriptionExtract();
            var word = new Application();
            Document doc = null;
            StatusLabel.Text = @"Generating Contract";
            switch (employmenttype)
            {
                case @"Casual Employment":
                    doc = word.Documents.Add(employmentcontracts + "\\HR-EMP-001 Casual Employment Agreement.dotx");
                    break;
                case @"Full-Time Employment":
                    doc = word.Documents.Add(employmentcontracts + "\\DGE-HR-FRM-024 Full Time Local-Pilbara Region Employment Agreement.dotx");
                    break;
            }

            if (doc == null) return;
            doc.Activate();
            ProgressBar_Compiler.PerformStep();
            StatusLabel.Text = @"Digitally Signing Contract";
            doc.BuiltInDocumentProperties["Company"].Value = "De Wet and Green Engineering PTY LTD";
            //signature Search
            foreach (var s in doc.InlineShapes.Cast<InlineShape>()
                         .Where(s => s.Type == WdInlineShapeType.wdInlineShapePicture)
                         .Where(s => s.AlternativeText == "Signature"))
            {
                signRanges.Add(s.Range);
                var signheight = s.Height;
                var signwidth = s.Width;
                s.Delete();
                //Signature Replacement
                foreach (var ils in signRanges.Select(r => r.InlineShapes.AddPicture(_signature, ref _missing, ref _missing, ref _missing)))
                {
                    ils.Height = signheight;
                    ils.Width = signwidth;
                }
            }
            StatusLabel.Text = @"Filling in Fields";
            //Custom Fields Replacement
            foreach (Field myMergeField in doc.Fields)
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
                        case "address":
                            myMergeField.Select();
                            word.Selection.TypeText("50 Coolawanyah Road, Karratha Industrial Estate, WA 6714");
                            break;
                        case "email":
                            myMergeField.Select();
                            word.Selection.TypeText("recruitment@dgengineering.com.au");
                            break;
                        case "website":
                            myMergeField.Select();
                            word.Selection.TypeText("www.dgengineering.com.au");
                            break;
                        case "telephone":
                            myMergeField.Select();
                            word.Selection.TypeText("(08) 9103 3895");
                            break;
                        case "acnnumber":
                            myMergeField.Select();
                            word.Selection.TypeText("32 623 753 391");
                            break;
                        case "tradingas":
                            myMergeField.Select();
                            word.Selection.TypeText("DG Engineering");
                            break;
                        case "payratetype":
                            myMergeField.Select();
                            word.Selection.TypeText("Pay Rate\t");
                            break;
                    }
                }
            }

            StatusLabel.Text = @"Inputting Data into Contract";
            //FormField Replacement
            foreach (FormField field in doc.FormFields)
            {
                switch (field.Name)
                {
                    case "DGE_Position":
                        switch (RepresentativeComboBox.Text)
                        {
                            case @"Damien Voigt":
                                field.Range.Text = @"Operations Manager";
                                break;
                            case @"Leigh Wright":
                                field.Range.Text = @"General Manager";
                                break;
                        }
                        break;
                        case "DGE_Name":
                            field.Range.Text = RepresentativeComboBox.Text;
                        break;
                        case "Date_Signed":
                            field.Range.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        break;
                    case "Full_Name":
                        field.Range.Text = New_Employee_Name_TextBox.Text;
                        break;
                    case "Employee_Address":
                        field.Range.Text = Employee_Address_TextBox.Text;
                        break;
                    case "Employee_City":
                        field.Range.Text = Employee_Suburb_TextBox.Text;
                        break;
                    case "Employee_State":
                        field.Range.Text = Employee_State_ComboBox.Text;
                        break;
                    case "Employee_Postcode":
                        field.Range.Text = Employee_Postcode_TextBox.Text;
                        break;
                    case "Employee_Contact_Num":
                        field.Range.Text = Employee_Contact_Number_TextBox.Text;
                        break;
                    case "Employee_Email":
                        field.Range.Text = Employee_Email_TextBox.Text;
                        break;
                    case "Employee_commence":
                        field.Range.Text = Employee_Commencement_Date_Picker.Text;
                        break;
                    case "Employee_Role":
                        field.Range.Text = Job_Position_ComboBox.Text;
                        break;
                    case "Employee_Pay_Rate":
                        switch (employmenttype)
                        {
                            case @"Casual Employment":
                                field.Range.Text = Employee_Base_Rate_TextBox.Text + " Flat Rate / Hr";
                                break;
                            case @"Full-Time Employment":
                                field.Range.Text = Employee_Base_Rate_TextBox.Text + " / Hr\t";
                                break;
                        }
                        
                        break;
                    case "Role_Description":
                        field.Range.InsertXML(_retrievedText);
                        break;
                    case "Working_Away":
                        if (WorkingAwayCheckBox.Checked)
                        {
                            field.Range.InsertXML(SiteAllowanceExtract(employmentcontracts + "Site allowance.docx"));
                        }
//                         field.Range.Text = WorkingAwayCheckBox.Checked
//                            ? @"Additional Benefits

//Site allowance / LAHA

//    $4.00 P/H flat site allowance / LAHA:
//        a) The company is offering to as per your contract (clause 10.2) a variation to your current contract to include a benefit that will supplement your contractual rate by a further AUD $4.00 (Four Australian Dollars only) per hour worked while working away from home.
        
//        b) Conditions of receiving Site allowance / LAHA
//            i) The benefit may be only claimed for hours worked onsite including travel time to and from site
//            ii) Employee must be working away from home.
        
//        c) Benefit cannot be claimed for the following:
//            i) Off Site, Mobilisation and Demobilisation carried out in Karratha.
//            ii) Sick leave onsite or days off whilst on site.
//            iii) Standdown.":"";
                        break;
                }
            }
            ProgressBar_Compiler.PerformStep();
            StatusLabel.Text = @"Exporting Employee Contract";
            switch (Local_Worker_Checkbox.Checked)
            {
                case false:
                    switch (OutputTypeComboBox.Text)
                    {
                        case "Adobe Acrobat PDF Document":
                            doc.ExportAsFixedFormat(filename + New_Employee_Name_TextBox.Text + " Contract.pdf", WdExportFormat.wdExportFormatPDF);
                            RecruitmentViewer.CoreWebView2.Navigate(filename + New_Employee_Name_TextBox.Text + " Contract.pdf");
                            ReleaseComObjects(doc, word);
                            break;

                        case "Microsoft Word Document":
                            doc.SaveAs2(filename + New_Employee_Name_TextBox.Text + " Contract.docx");
                            RecruitmentViewer.CoreWebView2.Navigate(filename + New_Employee_Name_TextBox.Text + " Contract.docx");
                            ReleaseComObjects(doc, word);
                            break;
                    }
                    
                    break;
                case true:
                {
                    var outputfilename = filename + New_Employee_Name_TextBox.Text + "\\00. Contract.pdf";
                    switch (OutputTypeComboBox.Text)
                    {
                        case "Adobe Acrobat PDF Document":
                            doc.ExportAsFixedFormat(outputfilename, WdExportFormat.wdExportFormatPDF);
                            CombineContractFiles();
                            ReleaseComObjects(doc, word);
                            Directory.Delete(Path.GetTempPath() + New_Employee_Name_TextBox.Text, true);
                            break;
                        case "Microsoft Word Document":
                            doc.ExportAsFixedFormat(outputfilename, WdExportFormat.wdExportFormatPDF);
                            CombineContractFiles();
                            ReleaseComObjects(doc, word);
                            Directory.Delete(Path.GetTempPath() + New_Employee_Name_TextBox.Text, true);
                            break;
                    }
                    break;
                }
            }
            _retrievedText = null;
            StatusLabel.Visible = false;
        }
    }
}