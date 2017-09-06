using Web.Routes;
using Web.Urls.SiteUrls;

namespace Web.Urls.ApiUrls
{
    public class ApiCashoutUrl : ApiUrl
    {
        private readonly string _slug;

        public ApiCashoutUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(ApiRoutes.Cashout, RouteReplace.Slug(_slug));
    }
}