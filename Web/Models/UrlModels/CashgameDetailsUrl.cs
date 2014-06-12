using Web.Routing;

namespace Web.Models.UrlModels
{
    public class CashgameDetailsUrl : CashgameUrl
    {
        public CashgameDetailsUrl(string slug, string dateStr)
            : base(RouteFormats.CashgameDetails, slug, dateStr)
        {
        }
    }
}