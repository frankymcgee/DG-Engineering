using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Word.Application;

// ReSharper disable once CheckNamespace
namespace DG_Engineering
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Static
    {
        public const string AssignarAuthUrl = "https://auth.assignar.com.au/login";
        public const string AssignarDashboardUrl = "https://api.assignar.com.au/v2/";
        public const string MyobClientId = "43112a28-1d90-4a9e-97a2-0c5f40f25aef";
        public const string MyobSecretKey = "oOO87NlUi6aYxB35VkqeG8om";
        public const string Companyfileuri = "https://arl2.api.myob.com/accountright";
        public const string Companyfileguid = "2f384cc3-b74b-4ddc-bd30-f6699f2e9ac9";
        public static string Refreshtoken;
        public static string Urlcoded;
        public static string AccessToken;
        public static string JwtToken;
        public static int ProjectNumber;
        public static string AssignarInternalNumber;
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
    public static class Extensions
    {
        public static Stream ConvertToBase64(this Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            var base64 = Convert.ToBase64String(bytes);
            return new MemoryStream(Encoding.UTF8.GetBytes(base64));
        }
    }
}
