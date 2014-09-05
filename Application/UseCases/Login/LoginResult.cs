using Application.Urls;

namespace Application.UseCases.Login
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