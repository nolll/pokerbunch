namespace Core.Services
{
    public interface IAuthService
    {
        string SignIn(string userNameOrEmail, string password);
    }
}