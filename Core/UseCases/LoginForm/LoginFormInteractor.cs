namespace Core.UseCases.LoginForm
{
    public class LoginFormInteractor
    {
        public static LoginFormResult Execute(LoginFormRequest request)
        {
            return new LoginFormResult(request.ReturnUrl);
        }
    }
}