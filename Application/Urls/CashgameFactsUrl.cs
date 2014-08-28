namespace Application.Urls
{
    public class CashgameFactsUrl : BunchWithOptionalYearUrl
    {
        public CashgameFactsUrl(string slug, int? year)
            : base(RouteFormats.CashgameFacts, RouteFormats.CashgameFactsWithYear, slug, year)
        {
        }
    }
}