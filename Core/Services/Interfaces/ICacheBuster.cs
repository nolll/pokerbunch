using Core.Entities;

namespace Core.Services
{
    public interface ICacheBuster
    {
        void UserAdded();
        void UserUpdated(User user);
        void BunchAdded();
        void BunchUpdated(Bunch bunch);
        void PlayerAdded(Bunch bunch);
        void PlayerUpdated(Player player);
        void PlayerDeleted(Bunch bunch, Player player);
        void CashgameStarted(Bunch bunch);
        void CashgameUpdated(Cashgame cashgame);
    }
}