using Web.Common.Routes;

namespace Web.Urls
{
    public class ApiReportUrl : ApiUrl
    {
        public ApiReportUrl(string slug)
            : base(ApiRoutes.Report)
        {
        }
    }
}