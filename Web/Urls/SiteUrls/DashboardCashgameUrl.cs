using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class DashboardCashgameUrl : SiteUrl
    {
        private readonly string _slug;

        public DashboardCashgameUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Admin.ClearCache, RouteParam.Slug(_slug));
    }
}