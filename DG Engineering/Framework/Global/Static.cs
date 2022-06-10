using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Word.Application;

namespace DG_Engineering
{
    public class Static
    {
        public static string AssignarAuthUrl = "https://auth.assignar.com.au/login";
        public static string AssignarDashboardUrl = "https://api.assignar.com.au/v2/";
        public static string JwtToken;
        public static int ProjectNumber;
        public static string AssignarInternalNumber;
        public static bool IsLoggedIn { get; set; }
        public static string ClientId;
        public static string UserName;
        public static string Password;
        public static string Cache = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),@"DGE",@"login.dge");
    }
    public partial class MainWindow
    {
        public static string Output = Path.GetTempPath();
        public static string RetrievedText;
        public static string CoverLetterPath = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"),
            "DG Engineering\\DG Engineering HUB - Human Resources\\1. Forms\\1. Job Packs\\Job Pack.dotx");
        public static string EmploymentContracts = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"),
            "DG Engineering\\DG Engineering HUB - Human Resources\\1. Forms\\3. Employment Agreements\\");
        public static string CompilePath = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"),
            "DG Engineering\\DG Engineering HUB - Human Resources\\Employees\\8. Forms\\New Employment Form\\Documents\\");
        public static string PreStartPath = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"),
            "DG Engineering\\DG Engineering HUB - Equipment & Maintenance\\Equipment and Maintenance\\Vehicle Pre-Start Checklist\\");
        private Application _wordApp;
        private object _missing = System.Reflection.Missing.Value;
        private string _filename;
        private static readonly string Picturepath = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"),
            "DG Engineering\\DG Engineering HUB - Business Support\\New Folder Structure\\Corporate\\Logos\\");
        private static readonly string Signpath = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"),
            "DG Engineering\\DG Engineering HUB - Human Resources\\2. Automation\\Signatures\\");
        
        public static string SimProUrl = "https://dwg.simprosuite.com/api/v1.0/companies/2/";
        public string FileUploadName;
        public OpenFileDialog OpenFileDialog { get; set; }
        public string Signature;
        public string Picture;
        private string _trim;
        private string _documentref;
        public string OfficerName;
        public string OfficerPosition;
        public int Filestep = 0;
        public int CompanyId;
        public int ProjectId;
        public static string DocumentPathFull = Path.Combine(Path.GetTempPath(), "Documents");
    }
}
