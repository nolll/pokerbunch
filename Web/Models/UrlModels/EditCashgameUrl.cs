using Web.Routing;

namespace Web.Models.UrlModels
{
    public class EditCashgameUrl : CashgameUrl
    {
        public EditCashgameUrl(string slug, string dateStr)
            : base(RouteFormats.CashgameEdit, slug, dateStr)
        {
        }
    }
}