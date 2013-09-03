using System;

namespace Web.Models.ChartModels{

	public class ChartDateTimeValueModel : ChartValueModel {

		public ChartDateTimeValueModel(DateTime dateTime)
            : base(FormatDate(dateTime))
        {
		}

		private static string FormatDate(DateTime dateTime){
			const string format = "Date({0}, {1}, {2}, {3}, {4}, {5})";
			var year = dateTime.Year;
			var month = dateTime.Month;
			var day = dateTime.Day;
			var hour = dateTime.Hour;
			var minute = dateTime.Minute;
			var second = dateTime.Second;
			return string.Format(format, year, month, day, hour, minute, second);
		}

	}

}