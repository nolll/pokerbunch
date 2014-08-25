namespace Application.UseCases.LoginForm
{
    public class LoginFormRequest
    {
        public string ReturnUrl { get; private set; }

        public LoginFormRequest(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }
    }
}