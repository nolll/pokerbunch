using Core.Entities;
using Core.Services;

namespace Infrastructure.Data.Cache
{
    public class CacheBuster : ICacheBuster
    {
        private readonly ICacheContainer _cacheContainer;

        public CacheBuster(ICacheContainer cacheContainer)
        {
            _cacheContainer = cacheContainer;
        }

        public void UserAdded()
        {
            var key = CacheKeyProvider.UserIdsKey();
            _cacheContainer.Remove(key);
        }

        public void UserUpdated(User user)
        {
            var singleUserKey = CacheKeyProvider.UserKey(user.Id);
            _cacheContainer.Remove(singleUserKey);

            var nameKey = CacheKeyProvider.UserIdByNameOrEmailKey(user.UserName);
            _cacheContainer.Remove(nameKey);

            var emailKey = CacheKeyProvider.UserIdByNameOrEmailKey(user.Email);
            _cacheContainer.Remove(emailKey);
        }

        public void BunchAdded()
        {
            var key = CacheKeyProvider.BunchIdsKey();
            _cacheContainer.Remove(key);
        }

        public void BunchUpdated(Bunch bunch)
        {
            var singleHomegameKey = CacheKeyProvider.BunchKey(bunch.Id);
            _cacheContainer.Remove(singleHomegameKey);

            var slugKey = CacheKeyProvider.BunchIdBySlugKey(bunch.Slug);
            _cacheContainer.Remove(slugKey);
        }

        public void PlayerAdded(Bunch bunch)
        {
            var key = CacheKeyProvider.PlayerIdsKey(bunch.Id);
            _cacheContainer.Remove(key);
        }

        public void PlayerUpdated(Player player)
        {
            var singleUserKey = CacheKeyProvider.PlayerKey(player.Id);
            _cacheContainer.Remove(singleUserKey);
        }

        public void PlayerDeleted(Bunch bunch, Player player)
        {
            var key = CacheKeyProvider.PlayerIdsKey(bunch.Id);
            _cacheContainer.Remove(key);

            var singleUserKey = CacheKeyProvider.PlayerKey(player.Id);
            _cacheContainer.Remove(singleUserKey);
        }

        public void CashgameStarted(Bunch bunch)
        {
            ClearRunningCashgame(bunch.Id);
        }

        public void CashgameUpdated(Cashgame cashgame)
        {
            ClearRunningCashgame(cashgame.BunchId);
            ClearCashgame(cashgame.Id);
            ClearCashgameList(cashgame);
            ClearCashgameYears(cashgame.BunchId);
        }

        private void ClearCashgame(int cashgameId)
        {
            var singleCashgameKey = CacheKeyProvider.CashgameKey(cashgameId);
            _cacheContainer.Remove(singleCashgameKey);
        }

        private void ClearRunningCashgame(int homegameId)
        {
            var runningCashgameKey = CacheKeyProvider.CashgameIdByRunningKey(homegameId);
            _cacheContainer.Remove(runningCashgameKey);
        }

        private void ClearCashgameList(Cashgame cashgame)
        {
            var allTimeCacheKey = CacheKeyProvider.CashgameIdsKey(cashgame.BunchId, GameStatus.Finished);
            _cacheContainer.Remove(allTimeCacheKey);
            if (cashgame.StartTime.HasValue)
            {
                var currentYearCacheKey = CacheKeyProvider.CashgameIdsKey(cashgame.BunchId, GameStatus.Finished, cashgame.StartTime.Value.Year);
                _cacheContainer.Remove(currentYearCacheKey);
            }
        }

        private void ClearCashgameYears(int homegameId)
        {
            var cacheKey = CacheKeyProvider.CashgameYearsKey(homegameId);
            _cacheContainer.Remove(cacheKey);
        }
    }
}