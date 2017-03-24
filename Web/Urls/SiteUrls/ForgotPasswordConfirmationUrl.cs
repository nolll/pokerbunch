using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class ForgotPasswordConfirmationUrl : SiteUrl
    {
        protected override string Input => WebRoutes.User.ForgotPasswordConfirmation;
    }
}