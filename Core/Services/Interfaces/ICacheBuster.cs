namespace Core.Services
{
    public interface ICacheBuster
    {
        void UserAdded();
        void UserUpdated(int userId);
        void BunchAdded();
        void BunchUpdated(int bunchId);
        void PlayerAdded(int playerId);
        void PlayerUpdated(int playerId);
        void PlayerDeleted(int playerId);
        void CashgameStarted(int cashgameId);
        void CashgameUpdated(int cashgameId);
    }
}