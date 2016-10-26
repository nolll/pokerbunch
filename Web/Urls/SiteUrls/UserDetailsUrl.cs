namespace Web.Urls.SiteUrls
{
    public class UserDetailsUrl : UserUrl
    {
        public UserDetailsUrl(string userName)
            : base(WebRoutes.User.Details, userName)
        {
        }
    }
}