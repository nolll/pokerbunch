namespace Application.Urls
{
    public class CashgameChartJsonUrl : HomegameWithOptionalYearUrl
    {
        public CashgameChartJsonUrl(string slug, int? year)
            : base(RouteFormats.CashgameChartJson, RouteFormats.CashgameChartJsonWithYear, slug, year)
        {
        }
    }
}