namespace Application.Urls
{
    public class TopListUrl : HomegameWithOptionalYearUrl
    {
        public TopListUrl(string slug, int? year)
            : base(RouteFormats.CashgameToplist, RouteFormats.CashgameToplistWithYear, slug, year)
        {
        }
    }
}