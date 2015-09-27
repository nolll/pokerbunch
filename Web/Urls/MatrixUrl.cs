using Web.Common.Routes;

namespace Web.Urls
{
    public class MatrixUrl : BunchWithOptionalYearUrl
    {
        public MatrixUrl(string slug, int? year)
            : base(WebRoutes.CashgameMatrix, WebRoutes.CashgameMatrixWithYear, slug, year)
        {
        }
    }
}