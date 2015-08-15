namespace Core.Urls
{
    public abstract class UserUrl : Url
    {
        protected UserUrl(string format, string userName)
            : base(format.Replace("{userName}", userName))
        {
        }
    }
}