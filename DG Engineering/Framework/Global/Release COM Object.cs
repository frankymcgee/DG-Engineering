using System;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Word;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Releases all COM Objects for Word
        /// </summary>
        /// <param name="doc">The Document variable.</param>
        /// <param name="word">The Application variable.</param>
        private static void ReleaseComObjects(_Document doc, _Application word)
        {
            try
            {
                doc.Close(false);
                word.Quit();
                Marshal.FinalReleaseComObject(doc);
                Marshal.FinalReleaseComObject(word);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}