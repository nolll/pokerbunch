using Web.Routing;

namespace Web.Models.UrlModels
{
    public class EditUserUrl : UserUrl
    {
        public EditUserUrl(string userName)
            : base(RouteFormats.UserEdit, userName)
        {
        }
    }
}