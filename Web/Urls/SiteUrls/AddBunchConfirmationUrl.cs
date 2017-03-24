using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class AddBunchConfirmationUrl : SiteUrl
    {
        protected override string Input => WebRoutes.Bunch.AddConfirmation;
    }
}