namespace Application.UseCases.LoginForm
{
    public class LoginFormInteractor : ILoginFormInteractor
    {
        public LoginFormResult Execute(LoginFormRequest request)
        {
            return new LoginFormResult(request.ReturnUrl);
        }
    }
}