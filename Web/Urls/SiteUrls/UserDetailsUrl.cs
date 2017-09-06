using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class UserDetailsUrl : SiteUrl
    {
        private readonly string _userName;

        public UserDetailsUrl(string userName)
        {
            _userName = userName;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.User.Details, RouteReplace.UserName(_userName));
    }
}