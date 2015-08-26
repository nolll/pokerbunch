namespace Web.Urls
{
    public abstract class UserUrl : Url
    {
        protected UserUrl(string format, string userName)
            : base(RouteParams.ReplaceUserName(format, userName))
        {
        }
    }
}