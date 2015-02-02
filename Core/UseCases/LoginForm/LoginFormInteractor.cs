namespace Core.UseCases.LoginForm
{
    public class LoginFormInteractor
    {
        public LoginFormResult Execute(LoginFormRequest request)
        {
            return new LoginFormResult(request.ReturnUrl);
        }
    }
}