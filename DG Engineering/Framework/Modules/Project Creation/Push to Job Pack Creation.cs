using System;
using System.Globalization;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Pushes information to Job Pack Generation
        /// </summary>
        private void PushToJobPack()
        {
            JobPackTitle_TextBox.Text = ProjectName.Text;
            JobPackClient_TextBox.Text = ProjectClient.Text;
            JobPackSite_TextBox.Text = ProjectAddress.Text;
            JobPackNo_TextBox.Text = ProjectJobNumber.Text;
            JobPackPO_TextBox.Text = ProjectPONumber.Text;
            const CalendarWeekRule weekRule = CalendarWeekRule.FirstFourDayWeek;
            const DayOfWeek firstWeekDay = DayOfWeek.Monday;
            var calendar = System.Threading.Thread.CurrentThread.CurrentCulture.Calendar;
            var currentWeek = calendar.GetWeekOfYear(ProjectStartDate.Value, weekRule, firstWeekDay);
            JobPackWeek_TextBox.Text = $@"{currentWeek:00}" + @" - " + ProjectStartDate.Value.Year;
        }
    }
}
