using Core.Entities;

namespace Infrastructure.Data.Cache
{
    public class CacheBuster : ICacheBuster
    {
        private readonly ICacheContainer _cacheContainer;
        private readonly ICacheKeyProvider _cacheKeyProvider;

        public CacheBuster(ICacheContainer cacheContainer, ICacheKeyProvider cacheKeyProvider)
        {
            _cacheContainer = cacheContainer;
            _cacheKeyProvider = cacheKeyProvider;
        }

        public void UserAdded()
        {
            var key = _cacheKeyProvider.UserIdsKey();
            _cacheContainer.Remove(key);
        }

        public void UserUpdated(User user)
        {
            var singleUserKey = _cacheKeyProvider.UserKey(user.Id);
            _cacheContainer.Remove(singleUserKey);
            
            var nameKey = _cacheKeyProvider.UserIdByNameOrEmailKey(user.UserName);
            _cacheContainer.Remove(nameKey);

            var emailKey = _cacheKeyProvider.UserIdByNameOrEmailKey(user.Email);
            _cacheContainer.Remove(emailKey);
        }

        public void HomegameAdded()
        {
            var key = _cacheKeyProvider.HomegameIdsKey();
            _cacheContainer.Remove(key);
        }

        public void HomegameUpdated(Homegame homegame)
        {
            var singleHomegameKey = _cacheKeyProvider.HomegameKey(homegame.Id);
            _cacheContainer.Remove(singleHomegameKey);

            var slugKey = _cacheKeyProvider.HomegameIdBySlugKey(homegame.Slug);
            _cacheContainer.Remove(slugKey);
        }

        public void PlayerAdded(Homegame homegame)
        {
            var key = _cacheKeyProvider.PlayerIdsKey(homegame.Id);
            _cacheContainer.Remove(key);
        }

        public void PlayerUpdated(Player player)
        {
            var singleUserKey = _cacheKeyProvider.PlayerKey(player.Id);
            _cacheContainer.Remove(singleUserKey);
        }

        public void PlayerDeleted(Homegame homegame, Player player)
        {
            var key = _cacheKeyProvider.PlayerIdsKey(homegame.Id);
            _cacheContainer.Remove(key);

            var singleUserKey = _cacheKeyProvider.PlayerKey(player.Id);
            _cacheContainer.Remove(singleUserKey);
        }

        public void CashgameStarted(Homegame homegame)
        {
            ClearRunningCashgame(homegame.Id);
        }

        public void CashgameEnded(Homegame homegame, Cashgame cashgame)
        {
            ClearRunningCashgame(homegame.Id);
            ClearCashgame(cashgame.Id);
            ClearCashgameList(homegame.Id, cashgame);
            ClearCashgameYears(homegame.Id);
        }

        public void CashgameUpdated(Cashgame cashgame)
        {
            ClearCashgame(cashgame.Id);
        }

        private void ClearCashgame(int cashgameId)
        {
            var singleCashgameKey = _cacheKeyProvider.CashgameKey(cashgameId);
            _cacheContainer.Remove(singleCashgameKey);
        }

        private void ClearRunningCashgame(int homegameId)
        {
            var runningCashgameKey = _cacheKeyProvider.CashgameIdByRunningKey(homegameId);
            _cacheContainer.Remove(runningCashgameKey);
        }

        private void ClearCashgameList(int homegameId, Cashgame cashgame)
        {
            var allTimeCacheKey = _cacheKeyProvider.CashgameIdsKey(homegameId, GameStatus.Published);
            _cacheContainer.Remove(allTimeCacheKey);
            if (cashgame.StartTime.HasValue)
            {
                var currentYearCacheKey = _cacheKeyProvider.CashgameIdsKey(homegameId, GameStatus.Published, cashgame.StartTime.Value.Year);
                _cacheContainer.Remove(currentYearCacheKey);
            }
        }

        public void ClearCashgameYears(int homegameId)
        {
            var cacheKey = _cacheKeyProvider.CashgameYearsKey(homegameId);
            _cacheContainer.Remove(cacheKey);
        }

    }
}