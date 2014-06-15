namespace Application.Urls
{
    public class CashgameReportUrl : PlayerUrl
    {
        public CashgameReportUrl(string slug, int playerId)
            : base(RouteFormats.CashgameReport, slug, playerId)
        {
        }
    }
}