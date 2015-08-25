using Core.Urls;

namespace Web.Urls
{
    public class ChartUrl : BunchWithOptionalYearUrl
    {
        public ChartUrl(string slug, int? year)
            : base(Routes.CashgameChart, Routes.CashgameChartWithYear, slug, year)
        {
        }
    }
}