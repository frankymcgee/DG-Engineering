using System.IO;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Will Change the Text on the Generator Button
        /// </summary>
        private void GenButtonTextSwitch()
        {
            switch (Generate_Button.Text)
            {
                case @"Generate":
                    DocTitle_TextBox.Text = _trim;
                    switch (DocumentForComboBox.Text)
                    {
                        case @"De Wet & Green Engineering PTY LTD":
                            _picture = Path.Combine(PicturePath, @"DGE.png");
                            _signature = Path.Combine(SignPath, @"Janko.png");
                            break;
                        case @"Norwest Rigging & Scaffolding":
                            _picture = Path.Combine(PicturePath, @"NRS.png");
                            _signature = Path.Combine(SignPath, @"Jason.png");
                            break;
                    }

                    Generate_Button.Text = @"Close";
                    break;
                case @"Close":
                    DocumentGeneratorViewer.Navigate("about:blank");
                    Generate_Button.Text = @"Generate";
                    break;
            }

            DocTitle_TextBox.Text = _trim;
            switch (DocumentForComboBox.Text)
            {
                case @"De Wet & Green Engineering PTY LTD":
                    _picture = Path.Combine(PicturePath, @"DGE.png");
                    _signature = Path.Combine(SignPath, @"Janko.png");
                    break;
                case @"Norwest Rigging & Scaffolding":
                    _picture = Path.Combine(PicturePath, @"NRS.png");
                    _signature = Path.Combine(SignPath, @"Jason.png");
                    break;
            }

            UpdateDoc(_filename);
        }
    }
}