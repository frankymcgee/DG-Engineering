using Microsoft.Office.Interop.Word;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Generates New Starter Pack.
        /// </summary>
        private void GenerateNsp()
        {
            var word = new Application();
            var doc = word.Documents.Add(CompilePath + "DGE-HR-FRM-NSP.dotx");
            doc.Activate();
            foreach (FormField field in doc.FormFields)
            {
                switch (field.Name)
                {
                    case "Name":
                        field.Range.Text = New_Employee_Name_TextBox.Text;
                        break;
                    case "Position_Title":
                        field.Range.Text = Job_Position_ComboBox.Text;
                        break;
                    case "Address":
                        field.Range.Text = Employee_Address_TextBox.Text;
                        break;
                    case "Suburb":
                        field.Range.Text = Employee_Suburb_TextBox.Text;
                        break;
                    case "State":
                        field.Range.Text = Employee_State_ComboBox.Text;
                        break;
                    case "Postcode":
                        field.Range.Text = Employee_Postcode_TextBox.Text;
                        break;
                    case "Mobile":
                        field.Range.Text = Employee_Contact_Number_TextBox.Text;
                        break;
                    case "Email_Address":
                        field.Range.Text = Employee_Email_TextBox.Text;
                        break;
                    case "Full_Name":
                        field.Range.Text = New_Employee_Name_TextBox.Text;
                        break;
                }
            }

            doc.ExportAsFixedFormat(CompilePath + "DGE-HR-FRM-NSP.pdf", WdExportFormat.wdExportFormatPDF);
            ReleaseComObjects(doc, word);
        }
    }
}