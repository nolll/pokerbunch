using Web.Routes;
using Web.Urls.SiteUrls;

namespace Web.Urls.ApiUrls
{
    public class ApiRunningGameUrl : ApiUrl
    {
        private readonly string _slug;

        public ApiRunningGameUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(ApiRoutes.RunningGame, RouteParam.Slug(_slug));
    }
}