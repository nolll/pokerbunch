using Web.Routes;
using Web.Urls.SiteUrls;

namespace Web.Urls.ApiUrls
{
    public class ApiBuyinUrl : ApiUrl
    {
        private readonly string _slug;

        public ApiBuyinUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(ApiRoutes.Buyin, RouteParam.Slug(_slug));
    }
}