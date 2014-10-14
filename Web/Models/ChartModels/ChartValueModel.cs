using System;
using System.Globalization;
using Newtonsoft.Json;
using Web.Annotations;

namespace Web.Models.ChartModels
{
    public class ChartValueModel
    {
        [UsedImplicitly]
	    [JsonProperty("v")]
        public string V { [UsedImplicitly] get; set; }

        [UsedImplicitly]
        [JsonProperty("f")]
        public string F { [UsedImplicitly] get; private set; }

        public ChartValueModel(string val)
        {
            V = val;
            F = null;
        }
    }

    public class ChartIntValueModel : ChartValueModel
    {
        public ChartIntValueModel(int? val)
            : base(val.HasValue ? val.Value.ToString(CultureInfo.InvariantCulture) : null)
        {
        }
    }

    public class ChartDateTimeValueModel : ChartValueModel
    {
        public ChartDateTimeValueModel(DateTime val)
            : base(FormatDate(val))
        {
        }

        private static string FormatDate(DateTime dateTime)
        {
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