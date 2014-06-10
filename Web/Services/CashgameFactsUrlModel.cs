using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class CashgameFactsUrlModel : HomegameWithOptionalYearUrlModel
    {
        public CashgameFactsUrlModel(string slug, int? year)
            : base(RouteFormats.CashgameFacts, RouteFormats.CashgameFactsWithYear, slug, year)
        {
        }
    }
}