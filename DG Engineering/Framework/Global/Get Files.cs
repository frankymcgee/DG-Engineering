using System.Collections.Generic;
using System.IO;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Looks for all Files in a Directory.
        /// </summary>
        /// <returns>string[]</returns>
        private static IEnumerable<string> GetFiles(string compilepath)
        {
            var dirs = Directory.GetFiles(compilepath);
            return dirs;
        }
    }
}