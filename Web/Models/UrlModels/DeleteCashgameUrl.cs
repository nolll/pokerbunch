using Web.Routing;

namespace Web.Models.UrlModels
{
    public class DeleteCashgameUrl : CashgameUrl
    {
        public DeleteCashgameUrl(string slug, string dateStr)
            : base(RouteFormats.CashgameDelete, slug, dateStr)
        {
        }
    }
}