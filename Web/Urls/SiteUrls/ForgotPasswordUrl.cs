using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class ForgotPasswordUrl : SiteUrl
    {
        protected override string Input => WebRoutes.User.ForgotPassword;
    }
}