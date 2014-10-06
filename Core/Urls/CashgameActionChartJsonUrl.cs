namespace Core.Urls
{
    public class CashgameActionChartJsonUrl : CashgamePlayerUrl
    {
        public CashgameActionChartJsonUrl(string slug, string dateStr, int playerId)
            : base(RouteFormats.CashgameActionChartJson, slug, dateStr, playerId)
        {
        }
    }
}