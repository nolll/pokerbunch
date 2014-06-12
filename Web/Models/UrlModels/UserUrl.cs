namespace Web.Models.UrlModels
{
    public abstract class UserUrl : Url
    {
        protected UserUrl(string format, string userName)
            : base(string.Format(format, userName))
        {
        }
    }
}