using Web.Routes;
using Web.Urls.SiteUrls;

namespace Web.Urls.ApiUrls
{
    public class ApiReportUrl : ApiUrl
    {
        private readonly string _slug;

        public ApiReportUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(ApiRoutes.Report, RouteReplace.Slug(_slug));
    }
}