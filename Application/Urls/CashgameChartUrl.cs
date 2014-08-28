namespace Application.Urls
{
    public class CashgameChartUrl : BunchWithOptionalYearUrl
    {
        public CashgameChartUrl(string slug, int? year)
            : base(RouteFormats.CashgameChart, RouteFormats.CashgameChartWithYear, slug, year)
        {
        }
    }
}