using PokerBunch.Common.Routes;
using PokerBunch.Common.Urls.SiteUrls;

namespace PokerBunch.Common.Urls.ApiUrls
{
    public class ApiUserUrl : ApiUrl
    {
        private readonly string _userName;

        public ApiUserUrl(string userName)
        {
            _userName = userName;
        }

        protected override string Input => RouteParams.Replace(ApiRoutes.User.Get, RouteReplace.UserName(_userName));
    }
}