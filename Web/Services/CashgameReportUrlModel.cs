using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class CashgameReportUrlModel : PlayerUrlModel
    {
        public CashgameReportUrlModel(string slug, int playerId)
            : base(RouteFormats.CashgameReport, slug, playerId)
        {
        }
    }
}