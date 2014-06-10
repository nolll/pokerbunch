using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class DeleteCashgameUrlModel : CashgameUrlModel
    {
        public DeleteCashgameUrlModel(string slug, string dateStr)
            : base(RouteFormats.CashgameDelete, slug, dateStr)
        {
        }
    }
}