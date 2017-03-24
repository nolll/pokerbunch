using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class AddBunchUrl : SiteUrl
    {
        protected override string Input => WebRoutes.Bunch.Add;
    }
}