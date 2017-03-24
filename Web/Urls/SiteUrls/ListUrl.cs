using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class ListUrl : SiteUrl
    {
        private readonly string _slug;
        private readonly int? _year;

        public ListUrl(string slug, int? year)
        {
            _slug = slug;
            _year = year;
        }

        protected override string Input => _year.HasValue ? InputWithYear : InputWithoutYear;
        private string InputWithYear => RouteParams.Replace(WebRoutes.Cashgame.ListWithYear, RouteParam.Slug(_slug), RouteParam.Year(_year.Value));
        private string InputWithoutYear => RouteParams.Replace(WebRoutes.Cashgame.List, RouteParam.Slug(_slug));
    }
}