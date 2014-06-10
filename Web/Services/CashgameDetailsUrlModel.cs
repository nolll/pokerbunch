using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class CashgameDetailsUrlModel : CashgameUrlModel
    {
        public CashgameDetailsUrlModel(string slug, string dateStr)
            : base(RouteFormats.CashgameDetails, slug, dateStr)
        {
        }
    }
}