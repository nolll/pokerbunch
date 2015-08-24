using Core.Urls;

namespace Web.Urls
{
    public class LogoutUrl : Url
    {
        public LogoutUrl()
            : base(Routes.AuthLogout)
        {
        }
    }
}