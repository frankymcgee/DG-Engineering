using System;
using System.Globalization;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Give the option to push Job Information straight to the Job Pack Compiler Tab.
        /// </summary>
        public void PushDataToJobPackGenerator()
        {
            var count = 0;
            JobPackTitle_TextBox.Text = Project_Name_TextBox.Text;
            JobPackWeek_TextBox.Text = WeeksInYear(Start_Date_TextBox.Text).ToString();
            JobPackClient_TextBox.Text = Client_Name_TextBox.Text;
            JobPackSite_TextBox.Text = Site_Address_TextBox.Text;
            JobPackNo_TextBox.Text = Project_External_ID_TextBox.Text;
            JobPackPO_TextBox.Text = Project_Po_TextBox.Text;
            foreach (var a in Assignar_Tabs.TabPages)
            {
                count++;
                if (a.ToString().Contains(@"Job Pack Compiler"))
                {
                    Assignar_Tabs.SelectTab(count - 1);
                }
            }
        }

        public static int WeeksInYear(string date)
        {
            var cal = new GregorianCalendar(GregorianCalendarTypes.Localized);
            var converted = Convert.ToDateTime(date);
            return cal.GetWeekOfYear(converted, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}