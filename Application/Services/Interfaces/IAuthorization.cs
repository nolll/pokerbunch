using Core.Classes;

namespace App.Services.Interfaces
{
    public interface IAuthorization
    {
        Role GetRole(Homegame homegame);
        bool IsInRole(Homegame homegame, Role roleToCheck);
        void RequirePlayer(string bunchName);
        void RequireManager(string bunchName);
        bool CanActAsPlayer(string slug, string playerName);
    }
}