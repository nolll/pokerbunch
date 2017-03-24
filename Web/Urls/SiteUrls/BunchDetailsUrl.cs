using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class BunchDetailsUrl : SiteUrl
    {
        private readonly string _slug;

        public BunchDetailsUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Bunch.Details, RouteParam.Slug(_slug));
    }
}