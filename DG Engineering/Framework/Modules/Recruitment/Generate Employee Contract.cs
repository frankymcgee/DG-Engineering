using System.IO;
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
                    doc = word.Documents.Add(employmentcontracts + "\\DGE-HR-EMP-001 Casual Employment Agreement.dotx");
                    break;
                case @"Full-Time Employment":
                    doc = word.Documents.Add(employmentcontracts + "\\DGE-HR-EMP-001 Full-Time Agreement.dotx");
                    break;
            }

            if (doc == null) return;
            doc.Activate();
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
                        field.Range.Text = Employee_Base_Rate_TextBox.Text;
                        break;
                    case "Role_Description":
                        field.Range.Text = RetrievedText;
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
    }
}