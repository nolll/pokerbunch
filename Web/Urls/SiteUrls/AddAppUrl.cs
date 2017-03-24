using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class AddAppUrl : SiteUrl
    {
        protected override string Input => WebRoutes.App.Add;
    }
}