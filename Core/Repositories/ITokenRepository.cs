namespace Core.Repositories
{
    public interface ITokenRepository
    {
        string Get(string userName, string password);
    }
}