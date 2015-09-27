using Web.Common.Routes;

namespace Web.Urls
{
    public class UserDetailsUrl : UserUrl
    {
        public UserDetailsUrl(string userName)
            : base(WebRoutes.UserDetails, userName)
        {
        }
    }
}