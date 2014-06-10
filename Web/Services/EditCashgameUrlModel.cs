using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class EditCashgameUrlModel : CashgameUrlModel
    {
        public EditCashgameUrlModel(string slug, string dateStr)
            : base(RouteFormats.CashgameEdit, slug, dateStr)
        {
        }
    }
}