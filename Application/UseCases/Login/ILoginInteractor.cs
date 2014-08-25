namespace Application.UseCases.Login
{
    public interface ILoginInteractor
    {
        LoginResult Execute(LoginRequest request);
    }
}