namespace Core.Urls
{
    public class TopListUrl : BunchWithOptionalYearUrl
    {
        public TopListUrl(string slug, int? year)
            : base(RouteFormats.CashgameToplist, RouteFormats.CashgameToplistWithYear, slug, year)
        {
        }
    }
}