using Web.Routing;

namespace Web.Models.UrlModels
{
    public class UserDetailsUrl : UserUrl
    {
        public UserDetailsUrl(string userName)
            : base(RouteFormats.UserDetails, userName)
        {
        }
    }
}