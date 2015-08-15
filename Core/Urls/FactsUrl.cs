namespace Core.Urls
{
    public class FactsUrl : BunchWithOptionalYearUrl
    {
        public FactsUrl(string slug, int? year)
            : base(Routes.CashgameFacts, Routes.CashgameFactsWithYear, slug, year)
        {
        }
    }
}