using System;
using System.Globalization;
using Newtonsoft.Json;
using Web.Annotations;

namespace Web.Models.ChartModels
{
    public class ChartValueModel
    {
	    [JsonProperty("v")]
        public string V { [UsedImplicitly] get; set; }
        
        [JsonProperty("f")]
        public string F { [UsedImplicitly] get; private set; }

        public ChartValueModel()
            : this(string.Empty)
        {
        }

        public ChartValueModel(string val)
        {
            V = val;
            F = null;
        }

        public ChartValueModel(int? val)
            : this(val.HasValue ? val.Value.ToString(CultureInfo.InvariantCulture) : null)
        {
        }

        public ChartValueModel(DateTime val)
            : this(FormatDate(val))
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