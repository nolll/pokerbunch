using Web.Common.Routes;

namespace Web.Urls
{
    public class AddUserUrl : SiteUrl
    {
        public AddUserUrl()
            : base(WebRoutes.UserAdd)
        {
        }
    }
}