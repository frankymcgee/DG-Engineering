using System;
using System.IO;
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
        public static string RefreshToken;
        public static string UrlCoded;
        public static string AccessToken;
        public static string JwtToken;
        public static int ProjectNumber;
        public static string AssignarInternalNumber;
        public static string ClientId;
        public static string UserName;
        public static string Password;
        public static readonly string Cache = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),@"DGE",@"login.dge");
        // Version information for an assembly consists of the following four values:
        //
        //      Major Version
        //      Minor Version
        //      Build Number
        //      Revision

        public const string Version = "1.2.0.026";
    }
    public partial class MainWindow
    {
        private static readonly string Output = Path.GetTempPath();
        private static string _retrievedText;
        private static readonly string CoverLetterPath = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"),
            "DG Engineering\\DG Engineering HUB - Human Resources\\1. Forms\\1. Job Packs\\Job Pack.dotx");
        private static readonly string JobCoverPath = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"),
            "DG Engineering\\DG Engineering HUB - Health & Safety\\HSEQT\\12. Admin\\1. Forms\\DGE-HSEQ-ADM-FRM-010 Job Cover Letter & Checklist.dotx");
        private static readonly string EmploymentContracts = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"),
            "DG Engineering\\DG Engineering HUB - Human Resources\\1. Forms\\3. Employment Agreements\\");
        private static readonly string CompilePath = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"),
            "DG Engineering\\DG Engineering HUB - Human Resources\\Employees\\8. Forms\\New Employment Form\\Documents\\");

        private static readonly string PreStartPath = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"),
            "DG Engineering\\DG Engineering HUB - Equipment & Maintenance\\Equipment and Maintenance\\Vehicle Pre-Start Checklist\\");
        private Application _wordApp;
        private object _missing = System.Reflection.Missing.Value;
        private string _filename;
        private static readonly string PicturePath = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"),
            "DG Engineering\\DG Engineering HUB - Business Support\\New Folder Structure\\Corporate\\Logos\\");
        private static readonly string SignPath = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"),
            "DG Engineering\\DG Engineering HUB - Human Resources\\2. Automation\\Signatures\\");
        private OpenFileDialog OpenFileDialog { get; set; }
        private string _signature;
        private string _picture;
        private string _trim;
        private string _documentref;
        private string _officerName;
        private string _officerPosition;
        private int _filestep;
        private int _companyId;
        private int _projectId;
        // ReSharper disable once NotAccessedField.Local
        private string _fileUploadName;
    }

}
