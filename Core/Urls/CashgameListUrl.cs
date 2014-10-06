namespace Core.Urls
{
    public class CashgameListUrl : BunchWithOptionalYearUrl
    {
        public CashgameListUrl(string slug, int? year)
            : base(RouteFormats.CashgameList, RouteFormats.CashgameListWithYear, slug, year)
        {
        }
    }
}