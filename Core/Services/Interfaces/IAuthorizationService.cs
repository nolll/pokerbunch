namespace Core.Services
{
    public interface IAuthorizationService
    {
        bool IsPlayer(string gameName);
    }
}