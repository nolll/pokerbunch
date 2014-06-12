using Web.Routing;

namespace Web.Models.UrlModels
{
    public class CashgameFactsUrl : HomegameWithOptionalYearUrl
    {
        public CashgameFactsUrl(string slug, int? year)
            : base(RouteFormats.CashgameFacts, RouteFormats.CashgameFactsWithYear, slug, year)
        {
        }
    }
}