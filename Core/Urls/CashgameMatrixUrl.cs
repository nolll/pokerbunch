namespace Core.Urls
{
    public class CashgameMatrixUrl : BunchWithOptionalYearUrl
    {
        public CashgameMatrixUrl(string slug, int? year)
            : base(RouteFormats.CashgameMatrix, RouteFormats.CashgameMatrixWithYear, slug, year)
        {
        }
    }
}