namespace Application.Urls
{
    public class ListUrl : HomegameWithOptionalYearUrl
    {
        public ListUrl(string slug, int? year)
            : base(RouteFormats.CashgameList, RouteFormats.CashgameListWithYear, slug, year)
        {
        }
    }
}