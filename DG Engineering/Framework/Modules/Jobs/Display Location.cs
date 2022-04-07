namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Displays Job Location in Viewer
        /// </summary>
        /// <param name="lat">Latitude</param>
        /// <param name="lng">Logitude</param>
        public void DisplayJobLocation(double lat, double lng)
        {
            JobsTabViewer.CoreWebView2.Navigate(
                "https://www.google.com/maps/dir/?api=1&origin=-20.773450981466155%2C116.87255793221904&destination=" +
                lat + "%2C" + lng + "&zoom=12&basemap=satellite");
        }
    }
}