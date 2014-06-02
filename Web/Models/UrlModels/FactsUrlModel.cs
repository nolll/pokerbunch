using Web.Routing;

namespace Web.Models.UrlModels
{
    public class FactsUrlModel : HomegameWithOptionalYearUrlModel
    {
        public FactsUrlModel(string slug, int? year)
            : base(RouteFormats.CashgameFacts, RouteFormats.CashgameFactsWithYear, slug, year)
        {
        }
    }
}