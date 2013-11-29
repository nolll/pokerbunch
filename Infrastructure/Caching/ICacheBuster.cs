using Core.Classes;

namespace Infrastructure.Caching
{
    public interface ICacheBuster
    {
        void UserAdded();
        void UserUpdated(User user);
        void HomegameAdded();
        void HomegameUpdated(Homegame homegame);
        void PlayerAdded(Homegame homegame);
        void PlayerUpdated(Player player);
        void PlayerDeleted(Homegame homegame, Player player);
    }
}