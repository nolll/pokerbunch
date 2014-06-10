using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class CashgameListUrlModel : HomegameWithOptionalYearUrlModel
    {
        public CashgameListUrlModel(string slug, int? year)
            : base(RouteFormats.CashgameList, RouteFormats.CashgameListWithYear, slug, year)
        {
        }
    }
}