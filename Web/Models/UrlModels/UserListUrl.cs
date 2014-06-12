using Web.Routing;

namespace Web.Models.UrlModels
{
    public class UserListUrl : Url
    {
        public UserListUrl()
            : base(RouteFormats.UserList)
        {
        }
    }
}