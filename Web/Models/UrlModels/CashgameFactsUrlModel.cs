using Web.Routing;

namespace Web.Models.UrlModels
{
    public class CashgameFactsUrlModel : HomegameWithOptionalYearUrlModel
    {
        public CashgameFactsUrlModel(string slug, int? year)
            : base(RouteFormats.CashgameFacts, RouteFormats.CashgameFactsWithYear, slug, year)
        {
        }
    }
}