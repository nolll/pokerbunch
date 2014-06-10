using Web.Routing;

namespace Web.Models.UrlModels
{
    public class CashgameDetailsUrlModel : CashgameUrlModel
    {
        public CashgameDetailsUrlModel(string slug, string dateStr)
            : base(RouteFormats.CashgameDetails, slug, dateStr)
        {
        }
    }
}