using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class EndCashgameUrlModel : HomegameUrlModel
    {
        public EndCashgameUrlModel(string slug)
            : base(RouteFormats.CashgameEnd, slug)
        {
        }
    }
}