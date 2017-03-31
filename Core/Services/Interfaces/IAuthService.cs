namespace Core.Services
{
    public interface IAuthService
    {
        string SignIn(string userNameOrPassword, string password);
    }
}