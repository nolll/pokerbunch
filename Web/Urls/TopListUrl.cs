namespace Core.Urls
{
    public class TopListUrl : BunchWithOptionalYearUrl
    {
        public TopListUrl(string slug, int? year)
            : base(Routes.CashgameToplist, Routes.CashgameToplistWithYear, slug, year)
        {
        }
    }
}