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
                            Picture = Path.Combine(Picturepath, @"DGE.png");
                            Signature = Path.Combine(Signpath, @"Janko.png");
                            break;
                        case @"Norwest Rigging & Scaffolding":
                            Picture = Path.Combine(Picturepath, @"NRS.png");
                            Signature = Path.Combine(Signpath, @"Jason.png");
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
                    Picture = Path.Combine(Picturepath, @"DGE.png");
                    Signature = Path.Combine(Signpath, @"Janko.png");
                    break;
                case @"Norwest Rigging & Scaffolding":
                    Picture = Path.Combine(Picturepath, @"NRS.png");
                    Signature = Path.Combine(Signpath, @"Jason.png");
                    break;
            }

            UpdateDoc(_filename);
        }
    }
}