using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class AddUserUrl : SiteUrl
    {
        protected override string Input => WebRoutes.User.Add;
    }
}