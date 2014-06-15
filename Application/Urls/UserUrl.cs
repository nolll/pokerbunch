namespace Application.Urls
{
    public abstract class UserUrl : Url
    {
        protected UserUrl(string format, string userName)
            : base(string.Format(format, userName))
        {
        }
    }
}