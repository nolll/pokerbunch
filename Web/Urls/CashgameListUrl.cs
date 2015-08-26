namespace Web.Urls
{
    public class CashgameListUrl : BunchWithOptionalYearUrl
    {
        public CashgameListUrl(string slug, int? year)
            : base(Routes.CashgameList, Routes.CashgameListWithYear, slug, year)
        {
        }
    }
}