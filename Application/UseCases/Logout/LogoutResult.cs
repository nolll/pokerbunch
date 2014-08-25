using Application.Urls;

namespace Application.UseCases.Logout
{
    public class LogoutResult
    {
        public Url ReturnUrl
        {
            get { return new HomeUrl(); }
        }
    }
}