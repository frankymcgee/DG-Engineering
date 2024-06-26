﻿using System.IO;
using System.Windows.Forms;

namespace DG_Engineering
{
    public partial class LoginWindow
    {
        /// <summary>
        /// Reads the Login File for Login Information
        /// </summary>
        /// <param name="path">Path of the File</param>
        /// <param name="linenumber">Line number to be read.</param>
        /// <returns>string value.</returns>
        private static string ReadFile(string path, int linenumber)
        {
            string result = null;
            try
            {
                using (var inputFile = new StreamReader(path))
                {
                    for (var i = 1; i < linenumber; i++)
                    {
                        result = inputFile.ReadLine();
                    }

                    inputFile.Close();
                    return result;
                }
            }
            catch (IOException e)
            {
                MessageBox.Show(@"There was an error reading the file:" + @"

" + e.Message,@"Attention",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            return null;
        }
    }
}