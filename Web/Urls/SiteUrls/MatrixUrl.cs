using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class MatrixUrl : SiteUrl
    {
        private readonly string _slug;
        private readonly int? _year;

        public MatrixUrl(string slug, int? year)
        {
            _slug = slug;
            _year = year;
        }

        protected override string Input => _year.HasValue ? InputWithYear : InputWithoutYear;
        private string InputWithYear => RouteParams.Replace(WebRoutes.Cashgame.MatrixWithYear, RouteParam.Slug(_slug), RouteParam.Year(_year.Value));
        private string InputWithoutYear => RouteParams.Replace(WebRoutes.Cashgame.Matrix, RouteParam.Slug(_slug));
    }
}