namespace Application.Urls
{
    public class CashgameFactsUrl : HomegameWithOptionalYearUrl
    {
        public CashgameFactsUrl(string slug, int? year)
            : base(RouteFormats.CashgameFacts, RouteFormats.CashgameFactsWithYear, slug, year)
        {
        }
    }
}