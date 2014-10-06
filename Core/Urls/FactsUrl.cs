namespace Core.Urls
{
    public class FactsUrl : BunchWithOptionalYearUrl
    {
        public FactsUrl(string slug, int? year)
            : base(RouteFormats.CashgameFacts, RouteFormats.CashgameFactsWithYear, slug, year)
        {
        }
    }
}