using Web.Routing;

namespace Web.Models.UrlModels
{
    public class CashgameReportUrl : PlayerUrl
    {
        public CashgameReportUrl(string slug, int playerId)
            : base(RouteFormats.CashgameReport, slug, playerId)
        {
        }
    }
}