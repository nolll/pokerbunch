using Core.Classes;

namespace Core.Services
{
    public interface IAuthorization
    {
        Role GetRole(Homegame homegame);
        bool IsInRole(Homegame homegame, Role roleToCheck);
        void RequirePlayer(string bunchName);
        void RequireManager(string bunchName);
    }
}