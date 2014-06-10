using Web.Routing;

namespace Web.Models.UrlModels
{
    public class EditCashgameUrlModel : CashgameUrlModel
    {
        public EditCashgameUrlModel(string slug, string dateStr)
            : base(RouteFormats.CashgameEdit, slug, dateStr)
        {
        }
    }
}