namespace Application.Urls
{
    public class CashgameMatrixUrl : HomegameWithOptionalYearUrl
    {
        public CashgameMatrixUrl(string slug, int? year)
            : base(RouteFormats.CashgameMatrix, RouteFormats.CashgameMatrixWithYear, slug, year)
        {
        }
    }
}