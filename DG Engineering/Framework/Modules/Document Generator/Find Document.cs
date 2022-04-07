using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Will open a Dialog box to find the Document Templates.
        /// </summary>
        private void FindDocument()
        {
            try
            {
                OpenFileDialog = new OpenFileDialog
                {
                    Site = null,
                    Tag = null,
                    AddExtension = false,
                    CheckPathExists = false,
                    DefaultExt = null,
                    DereferenceLinks = false,
                    FileName = null,
                    Filter = null,
                    FilterIndex = 0,
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
                // Set filter options and filter index
                OpenFileDialog.Filter = @"Word Template (.dotx)|*.dotx";
                OpenFileDialog.FilterIndex = 1;
                OpenFileDialog.InitialDirectory = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"),
                    "DG Engineering\\");
                OpenFileDialog.Title = @"Select the Document";
                OpenFileDialog.ShowDialog();
                _filename = OpenFileDialog.FileName;
                var fullname = OpenFileDialog.SafeFileName?.Substring(OpenFileDialog.SafeFileName.LastIndexOf('-') + 1);
                var name = fullname?.Split('.')[0];
                var fulltrim = Regex.Replace(name ?? string.Empty, @"[\d-]", string.Empty);
                _trim = fulltrim.TrimStart(' ');
                DocTitle_TextBox.Text = _trim;
                _documentref = CompanyPreFix() + OpenFileDialog.SafeFileName?.Split(' ')[0];
                DocRefTextBox.Text = _documentref;
                var foldercount = _filename.Split('\\').Length;
                var folder = _filename.Split('\\')[foldercount - 2];
                DocTypeTextBox.Text = folder.ToUpper();
                if (!DocTitle_TextBox.Text.Contains("Pre Start")) return;
                VehicleLabel.Visible = true;
                VehicleComboBox.Visible = true;
                var folders = Directory.GetDirectories(_filename.Remove(_filename.LastIndexOf('\\') + 1));
                foreach (var s in folders)
                {
                    if (s.Substring(s.LastIndexOf("\\", StringComparison.Ordinal) + 1).StartsWith("[")) continue;
                    VehicleComboBox.Items.Add(s.Substring(s.LastIndexOf("\\", StringComparison.Ordinal) + 1));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}