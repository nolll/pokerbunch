using Web.Routing;

namespace Web.Models.UrlModels
{
    public class AddCashgameUrl : HomegameUrl
    {
        public AddCashgameUrl(string slug)
            : base(RouteFormats.CashgameAdd, slug)
        {
        }
    }
}