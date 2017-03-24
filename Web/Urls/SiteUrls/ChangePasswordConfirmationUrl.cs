using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class ChangePasswordConfirmationUrl : SiteUrl
    {
        protected override string Input => WebRoutes.User.ChangePasswordConfirmation;
    }
}