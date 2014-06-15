namespace Application.Urls
{
    public class CashgameDetailsChartJsonUrl : CashgameUrl
    {
        public CashgameDetailsChartJsonUrl(string slug, string dateStr)
            : base(RouteFormats.CashgameDetailsChartJson, slug, dateStr)
        {
        }
    }
}