using Web.Routing;

namespace Web.Models.UrlModels
{
    public class ListUrlModel : HomegameWithOptionalYearUrlModel
    {
        public ListUrlModel(string slug, int? year)
            : base(RouteFormats.CashgameList, RouteFormats.CashgameListWithYear, slug, year)
        {
        }
    }
}