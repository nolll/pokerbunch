using Web.Routing;

namespace Web.Models.UrlModels
{
    public class DeleteCashgameUrlModel : CashgameUrlModel
    {
        public DeleteCashgameUrlModel(string slug, string dateStr)
            : base(RouteFormats.CashgameDelete, slug, dateStr)
        {
        }
    }
}