using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class AddLocationUrl : SiteUrl
    {
        private readonly string _slug;

        public AddLocationUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Location.Add, RouteParam.Slug(_slug));
    }
}