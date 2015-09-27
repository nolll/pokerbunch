using Web.Common.Routes;

namespace Web.Urls
{
    public class ChartUrl : BunchWithOptionalYearUrl
    {
        public ChartUrl(string slug, int? year)
            : base(WebRoutes.CashgameChart, WebRoutes.CashgameChartWithYear, slug, year)
        {
        }
    }
}