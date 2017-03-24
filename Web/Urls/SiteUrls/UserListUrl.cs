using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class UserListUrl : SiteUrl
    {
        protected override string Input => WebRoutes.User.List;
    }
}