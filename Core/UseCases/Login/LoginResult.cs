using Core.Urls;

namespace Core.UseCases.Login
{
    public class LoginResult
    {
        public Url ReturnUrl { get; private set; }

        public LoginResult(Url returnUrl)
        {
            ReturnUrl = returnUrl;
        }
    }
}