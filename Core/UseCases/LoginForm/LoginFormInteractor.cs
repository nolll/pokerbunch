namespace Core.UseCases.LoginForm
{
    public static class LoginFormInteractor
    {
        public static LoginFormResult Execute(LoginFormRequest request)
        {
            return new LoginFormResult(request.ReturnUrl);
        }
    }
}