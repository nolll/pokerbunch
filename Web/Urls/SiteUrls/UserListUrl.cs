using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class UserListUrl : SiteUrl
    {
        public UserListUrl()
            : base(WebRoutes.User.List)
        {
        }
    }
}