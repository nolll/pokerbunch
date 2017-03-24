using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class AddAppConfirmationUrl : SiteUrl
    {
        protected override string Input => WebRoutes.App.AddConfirmation;
    }
}