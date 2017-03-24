using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class AddUserConfirmationUrl : SiteUrl
    {
        protected override string Input => WebRoutes.User.AddConfirmation;
    }
}