using Core.Classes;

namespace Infrastructure.Data.Cache
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
        void CashgameStarted(Homegame cashgame);
        void CashgameEnded(Homegame homegame, Cashgame cashgame);
        void CashgameUpdated(Cashgame cashgame);
        void ClearCashgameYears(int homegameId);
    }
}