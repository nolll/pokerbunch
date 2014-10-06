using Core.Urls;

namespace Core.UseCases.Logout
{
    public class LogoutResult
    {
        public Url ReturnUrl
        {
            get { return new HomeUrl(); }
        }
    }
}