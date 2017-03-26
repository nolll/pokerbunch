namespace Core.Services
{
    public interface ITokenService
    {
        string Get(string userName, string password);
    }
}