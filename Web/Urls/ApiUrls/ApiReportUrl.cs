using Web.Routes;

namespace Web.Urls.ApiUrls
{
    public class ApiReportUrl : SlugApiUrl
    {
        public ApiReportUrl(string slug)
            : base(ApiRoutes.Report, slug)
        {
        }
    }
}