using Web.Routing;

namespace Web.Models.UrlModels
{
    public class CashgameReportUrlModel : PlayerUrlModel
    {
        public CashgameReportUrlModel(string slug, int playerId)
            : base(RouteFormats.CashgameReport, slug, playerId)
        {
        }
    }
}