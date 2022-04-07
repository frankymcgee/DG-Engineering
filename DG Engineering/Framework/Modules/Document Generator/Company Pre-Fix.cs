namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Finds the Companies listed in the Document for Combox and outputs the appropriate prefix.
        /// </summary>
        /// <returns>e.g. DGE-</returns>
        private string CompanyPreFix()
        {
            string result = null;
            switch (DocumentForComboBox.Text)
            {
                case @"De Wet & Green Engineering PTY LTD":
                    result = @"DGE-";
                    OfficerName = @"Janko De Wet";
                    OfficerPosition = @"Managing Director";
                    break;
                case @"Norwest Rigging & Scaffolding":
                    result = @"NRS-";
                    OfficerName = @"Jason Matakatea";
                    OfficerPosition = @"Managing Director";
                    break;
            }

            return result;
        }
    }
}