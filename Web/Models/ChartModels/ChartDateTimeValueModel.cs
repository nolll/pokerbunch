using System;

namespace Web.Models.ChartModels
{
    public class ChartDateTimeValueModel : ChartValueModel
    {
        public ChartDateTimeValueModel(DateTime val)
            : base(FormatDate(val))
        {
        }

        private static string FormatDate(DateTime dateTime)
        {
            var year = dateTime.Year;
            var month = dateTime.Month - 1;
            var day = dateTime.Day;
            var hour = dateTime.Hour;
            var minute = dateTime.Minute;
            var second = dateTime.Second;
            return $"Date({year}, {month}, {day}, {hour}, {minute}, {second})";
        }
    }
}