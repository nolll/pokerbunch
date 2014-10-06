namespace Core.Urls
{
    public class ListUrl : BunchWithOptionalYearUrl
    {
        public ListUrl(string slug, int? year)
            : base(RouteFormats.CashgameList, RouteFormats.CashgameListWithYear, slug, year)
        {
        }
    }
}