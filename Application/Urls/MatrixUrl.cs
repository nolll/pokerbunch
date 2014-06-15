namespace Application.Urls
{
    public class MatrixUrl : HomegameWithOptionalYearUrl
    {
        public MatrixUrl(string slug, int? year)
            : base(RouteFormats.CashgameMatrix, RouteFormats.CashgameMatrixWithYear, slug, year)
        {
        }
    }
}