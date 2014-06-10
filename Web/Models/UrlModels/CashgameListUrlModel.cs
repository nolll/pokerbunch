using Web.Routing;

namespace Web.Models.UrlModels
{
    public class CashgameListUrlModel : HomegameWithOptionalYearUrlModel
    {
        public CashgameListUrlModel(string slug, int? year)
            : base(RouteFormats.CashgameList, RouteFormats.CashgameListWithYear, slug, year)
        {
        }
    }
}