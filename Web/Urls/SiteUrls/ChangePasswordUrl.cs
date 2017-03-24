using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class ChangePasswordUrl : SiteUrl
    {
        protected override string Input => WebRoutes.User.ChangePassword;
    }
}