using Web.Routing;

namespace Web.Models.UrlModels
{
    public class UserListUrlModel : UrlModel
    {
        public UserListUrlModel()
            : base(RouteFormats.UserList)
        {
        }
    }
}