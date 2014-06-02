using Web.Routing;

namespace Web.Models.UrlModels
{
    public class TopListUrlModel : HomegameWithOptionalYearUrlModel
    {
        public TopListUrlModel(string slug, int? year)
            : base(RouteFormats.CashgameToplist, RouteFormats.CashgameToplistWithYear, slug, year)
        {
        }
    }
}