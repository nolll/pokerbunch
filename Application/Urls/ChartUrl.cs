namespace Application.Urls
{
    public class ChartUrl : BunchWithOptionalYearUrl
    {
        public ChartUrl(string slug, int? year)
            : base(RouteFormats.CashgameChart, RouteFormats.CashgameChartWithYear, slug, year)
        {
        }
    }
}