using Web.Common.Routes;

namespace Web.Urls
{
    public class EditUserUrl : UserUrl
    {
        public EditUserUrl(string userName)
            : base(WebRoutes.UserEdit, userName)
        {
        }
    }
}