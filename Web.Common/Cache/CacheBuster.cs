using Core.Entities;
using Core.Services;
using Plumbing;

namespace Web.Common.Cache
{
    public class CacheBuster
    {
        private readonly Dependencies _deps;
        private readonly ICacheContainer _cache;

        public CacheBuster(Dependencies deps, ICacheContainer cacheContainer)
        {
            _deps = deps;
            _cache = cacheContainer;
        }

        public void UserAdded()
        {
            var key = CacheKeyProvider.UserIdsKey();
            _cache.Remove(key);
        }

        public void UserUpdated(int userId)
        {
            var user = _deps.UserRepository.Get(userId);

            var singleUserKey = CacheKeyProvider.ConstructCacheKey(typeof(User), userId);
            _cache.Remove(singleUserKey);

            var nameKey = CacheKeyProvider.UserIdByNameOrEmailKey(user.UserName);
            _cache.Remove(nameKey);

            var emailKey = CacheKeyProvider.UserIdByNameOrEmailKey(user.Email);
            _cache.Remove(emailKey);
        }

        public void BunchAdded()
        {
            var key = CacheKeyProvider.BunchIdsKey();
            _cache.Remove(key);
        }

        public void BunchUpdated(int bunchId)
        {
            var bunch = _deps.BunchService.Get(bunchId);

            var singleBunchKey = CacheKeyProvider.ConstructCacheKey(typeof(Bunch), bunchId);
            _cache.Remove(singleBunchKey);

            var slugKey = CacheKeyProvider.BunchIdBySlugKey(bunch.Slug);
            _cache.Remove(slugKey);
        }

        public void PlayerAdded(int playerId)
        {
            var player = _deps.PlayerRepository.GetById(playerId);
            var bunch = _deps.BunchService.Get(player.BunchId);

            var key = CacheKeyProvider.PlayerIdsKey(bunch.Id);
            _cache.Remove(key);
        }

        public void PlayerUpdated(int playerId)
        {
            var singlePlayerKey = CacheKeyProvider.ConstructCacheKey(typeof(Bunch), playerId);
            _cache.Remove(singlePlayerKey);
        }

        public void PlayerDeleted(int playerId)
        {
            var player = _deps.PlayerRepository.GetById(playerId);

            var key = CacheKeyProvider.PlayerIdsKey(player.BunchId);
            _cache.Remove(key);

            var singlePlayerKey = CacheKeyProvider.ConstructCacheKey(typeof(Player), playerId);
            _cache.Remove(singlePlayerKey);
        }

        public void CashgameStarted(int cashgameId)
        {
            var cashgame = _deps.CashgameRepository.GetById(cashgameId);

            ClearRunningCashgame(cashgame.BunchId);
        }

        public void CashgameUpdated(int cashgameId)
        {
            var cashgame = _deps.CashgameRepository.GetById(cashgameId);

            ClearRunningCashgame(cashgame.BunchId);
            ClearCashgame(cashgameId);
            ClearCashgameYears(cashgame.BunchId);
            ClearCashgameList(cashgame);
        }

        private void ClearCashgame(int cashgameId)
        {
            var singleCashgameKey = CacheKeyProvider.ConstructCacheKey(typeof(Cashgame), cashgameId);
            _cache.Remove(singleCashgameKey);
        }

        private void ClearRunningCashgame(int bunchId)
        {
            var runningCashgameKey = CacheKeyProvider.CashgameIdByRunningKey(bunchId);
            _cache.Remove(runningCashgameKey);
        }

        private void ClearCashgameList(Cashgame cashgame)
        {
            var allTimeCacheKey = CacheKeyProvider.CashgameIdsKey(cashgame.BunchId, GameStatus.Finished);
            _cache.Remove(allTimeCacheKey);

            if (cashgame.StartTime.HasValue)
            {
                var year = cashgame.StartTime.Value.Year;
                var currentYearCacheKey = CacheKeyProvider.CashgameIdsKey(cashgame.BunchId, GameStatus.Finished, year);
                _cache.Remove(currentYearCacheKey);
            }
        }

        private void ClearCashgameYears(int bunchId)
        {
            var cacheKey = CacheKeyProvider.CashgameYearsKey(bunchId);
            _cache.Remove(cacheKey);
        }
    }
}