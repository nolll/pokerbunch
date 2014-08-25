namespace Application.UseCases.LoginForm
{
    public interface ILoginFormInteractor
    {
        LoginFormResult Execute(LoginFormRequest request);
    }
}