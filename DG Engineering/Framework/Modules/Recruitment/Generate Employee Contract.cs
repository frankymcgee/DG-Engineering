﻿using System;
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
            var filename = Path.GetTempPath();
            JobDescriptionExtract();
            var word = new Application();
            Document doc = null;
            switch (employmenttype)
            {
                case @"Casual Employment":
                    doc = word.Documents.Add(employmentcontracts + "\\HR-EMP-001 Casual Employment Agreement.dotx");
                    break;
                case @"Full-Time Employment":
                    doc = word.Documents.Add(employmentcontracts + "\\HR-EMP-001 Full-Time Agreement.dotx");
                    break;
            }

            if (doc == null) return;
            doc.Activate();
            doc.BuiltInDocumentProperties["Company"].Value = "De Wet and Green Engineering PTY LTD";
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
                            word.Selection.TypeText("1967 Anderson Road, Karratha Industrial Estate, WA 6714");
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
                            word.Selection.TypeText("Pay Rate");
                            break;
                    }
                }
            }
            //FormField Replacement
            foreach (FormField field in doc.FormFields)
            {
                switch (field.Name)
                {
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
                                field.Range.Text = Employee_Base_Rate_TextBox.Text + " / Hr";
                                break;
                        }
                        
                        break;
                    case "Role_Description":
                        field.Range.Text = RetrievedText;
                        break;
                    case "Working_Away":
                        if (WorkingAwayCheckBox.Checked)
                        {
                            field.Range.Text =
                                @"Local Housing Allowance*
                                FIFO Working Away Allowance*:
                                The Employer will provide for additional Allowances according to the number of hours completed per fortnight, as follows:

                                Up to 76 Hours 
                                0 – 76 hours per fortnight 		    Extra: $ 5.00 per hour

                                Up to 100 Hours
                                0 – 76 hours per fortnight		    Extra: $ 5.00 per hour
                                77 – 100 hours per fortnight		Extra: $ 6.00 per hour

                                Up to 120 Hours
                                0 – 76 hours per fortnight		    Extra: $ 5.00 per hour
                                77 – 100 hours per fortnight		Extra: $ 6.00 per hour
                                101 – 120 hours per fortnight		Extra: $ 7.00 per hour

                                Above 120 Hours
                                0 – 76 hours per fortnight		    Extra: $ 5.00 per hour
                                77 – 100 hours per fortnight		Extra: $ 6.00 per hour
                                101 – 120 hours per fortnight		Extra: $ 7.00 per hour
                                120 plus hours per fortnight		Extra: $ 9.00 per hour

                                *All leave, public holidays and training excluded.";
                        }
                        break;
                }
            }

            switch (Local_Worker_Checkbox.Checked)
            {
                case false:
                    doc.ExportAsFixedFormat(filename + "Contract.pdf", WdExportFormat.wdExportFormatPDF);
                    WebBrowserControl.Navigate(filename + "Contract.pdf");
                    ReleaseComObjects(doc, word);
                    break;
                case true:
                {
                    var outputfilename = filename + New_Employee_Name_TextBox.Text + "\\00. Contract.pdf";
                    doc.ExportAsFixedFormat(outputfilename, WdExportFormat.wdExportFormatPDF);
                    CombineContractFiles();
                    ReleaseComObjects(doc, word);
                    Directory.Delete(Path.GetTempPath() + New_Employee_Name_TextBox.Text, true);
                    break;
                }
            }
        }
        public string WorkingAway(string employmentcontracts)
        {
            string text = null;
            object miss = System.Reflection.Missing.Value;
            object path = employmentcontracts + "\\Local Housing Allowance.docx";
            object readOnly = true;
            var word = new Application();
            var docs = word.Documents.Open(ref path, ref miss, ref readOnly,
                ref miss, ref miss, ref miss, ref miss,
                ref miss, ref miss, ref miss, ref miss,
                ref miss, ref miss, ref miss, ref miss,
                ref miss);

            // Datatable to store text from Word doc
            var dt = new System.Data.DataTable();
            dt.Columns.Add("Text");

            // Loop through each table in the document, 
            // grab only text from cells in the first column
            // in each table.
            foreach (var tb in docs.Tables.Cast<Table>().Where(tb => tb.Columns.Count.Equals(1)))
            {
                // insert code here to get text from cells in first column
                // and insert into datatable.
                for (var row = 1; row <= tb.Rows.Count; row++)
                {
                    var cell = tb.Cell(row, 1);
                    text = cell.Range.Text;
                    // text now contains the content of the cell.
                }
            }

            docs.Close();
            word.Quit();
            return text;
        }
    }
}