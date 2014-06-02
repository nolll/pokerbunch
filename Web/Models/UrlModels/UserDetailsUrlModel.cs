using Web.Routing;

namespace Web.Models.UrlModels
{
    public class UserDetailsUrlModel : UserUrlModel
    {
        public UserDetailsUrlModel(string userName)
            : base(RouteFormats.UserDetails, userName)
        {
        }
    }
}