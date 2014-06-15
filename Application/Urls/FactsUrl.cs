namespace Application.Urls
{
    public class FactsUrl : HomegameWithOptionalYearUrl
    {
        public FactsUrl(string slug, int? year)
            : base(RouteFormats.CashgameFacts, RouteFormats.CashgameFactsWithYear, slug, year)
        {
        }
    }
}