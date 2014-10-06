using Core.Urls;

namespace Core.UseCases.LoginForm
{
    public class LoginFormResult
    {
        public Url AddUserUrl { get; private set; }
        public Url ForgotPasswordUrl { get; private set; }
        public Url ReturnUrl { get; private set; }

        public LoginFormResult(string returnUrl)
        {
            ReturnUrl = string.IsNullOrEmpty(returnUrl) ? new HomeUrl() : new Url(returnUrl);
            AddUserUrl = new AddUserUrl();
            ForgotPasswordUrl = new ForgotPasswordUrl();
        }
    }
}