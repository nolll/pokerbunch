using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class AddUserUrl : SiteUrl
    {
        public AddUserUrl()
            : base(WebRoutes.User.Add)
        {
        }
    }
}