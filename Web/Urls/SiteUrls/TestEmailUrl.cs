using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class TestEmailUrl : SiteUrl
    {
        protected override string Input => WebRoutes.Admin.SendEmail;
    }
}