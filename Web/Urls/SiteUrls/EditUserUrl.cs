using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class EditUserUrl : UserUrl
    {
        public EditUserUrl(string userName)
            : base(WebRoutes.User.Edit, userName)
        {
        }
    }
}