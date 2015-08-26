namespace Web.Urls
{
    public class MatrixUrl : BunchWithOptionalYearUrl
    {
        public MatrixUrl(string slug, int? year)
            : base(Routes.CashgameMatrix, Routes.CashgameMatrixWithYear, slug, year)
        {
        }
    }
}