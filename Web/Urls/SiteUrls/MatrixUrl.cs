namespace Web.Urls.SiteUrls
{
    public class MatrixUrl : BunchWithOptionalYearUrl
    {
        public MatrixUrl(string slug, int? year)
            : base(WebRoutes.Cashgame.Matrix, WebRoutes.Cashgame.MatrixWithYear, slug, year)
        {
        }
    }
}