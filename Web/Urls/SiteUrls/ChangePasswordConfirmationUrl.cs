using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class ChangePasswordConfirmationUrl : SiteUrl
    {
        public ChangePasswordConfirmationUrl()
            : base(WebRoutes.User.ChangePasswordConfirmation)
        {
        }
    }
}