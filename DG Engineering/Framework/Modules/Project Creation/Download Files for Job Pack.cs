using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        private void DownloadFilesForJobPack(string file)
        {
            var path = Path.Combine(DGEngineering, "DG Engineering HUB - Operations\\Jobs", JobPackNo_TextBox.Text,
                JobDocuments_ComboBox.Text);
            _filestep++;
            var output = Path.Combine(Path.GetTempPath(), "Job Pack Generator\\");
            File.Copy(path, output + _filestep + ". " + file.Split('\\').Last());
            MessageBox.Show(@"Added", @"Document Added");
            JobDocuments_ListBox.Items.Add(_filestep + ". " + file.Split('\\').Last());
        }
    }
}