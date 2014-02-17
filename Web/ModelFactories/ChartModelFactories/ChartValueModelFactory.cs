using System;
using System.Globalization;
using Web.Models.ChartModels;

namespace Web.ModelFactories.ChartModelFactories
{
    public class ChartValueModelFactory : IChartValueModelFactory
    {
        public ChartValueModel Create()
        {
            return new ChartValueModel
            {
                V = string.Empty
            };
        }
        
        public ChartValueModel Create(string val)
        {
            return new ChartValueModel
                {
                    V = val
                };
        }

        public ChartValueModel Create(int? val)
        {
            return new ChartValueModel
                {
                    V = val.HasValue ? val.Value.ToString(CultureInfo.InvariantCulture) : null
                };
        }
        
        public ChartValueModel Create(DateTime dateTime)
        {
            return new ChartValueModel{
                V = FormatDate(dateTime)
            };
        }

        private string FormatDate(DateTime dateTime)
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