namespace Web.Urls
{
    public class UserDetailsUrl : UserUrl
    {
        public UserDetailsUrl(string userName)
            : base(Routes.UserDetails, userName)
        {
        }
    }
}