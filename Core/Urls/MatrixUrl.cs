namespace Core.Urls
{
    public class MatrixUrl : BunchWithOptionalYearUrl
    {
        public MatrixUrl(string slug, int? year)
            : base(RouteFormats.CashgameMatrix, RouteFormats.CashgameMatrixWithYear, slug, year)
        {
        }
    }
}