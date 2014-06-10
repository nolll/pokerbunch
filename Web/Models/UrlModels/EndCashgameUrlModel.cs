using Web.Routing;

namespace Web.Models.UrlModels
{
    public class EndCashgameUrlModel : HomegameUrlModel
    {
        public EndCashgameUrlModel(string slug)
            : base(RouteFormats.CashgameEnd, slug)
        {
        }
    }
}