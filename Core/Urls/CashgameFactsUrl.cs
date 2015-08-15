namespace Core.Urls
{
    public class CashgameFactsUrl : BunchWithOptionalYearUrl
    {
        public CashgameFactsUrl(string slug, int? year)
            : base(Routes.CashgameFacts, Routes.CashgameFactsWithYear, slug, year)
        {
        }
    }
}