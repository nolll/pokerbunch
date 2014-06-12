using Web.Routing;

namespace Web.Models.UrlModels
{
    public class EndCashgameUrl : HomegameUrl
    {
        public EndCashgameUrl(string slug)
            : base(RouteFormats.CashgameEnd, slug)
        {
        }
    }
}