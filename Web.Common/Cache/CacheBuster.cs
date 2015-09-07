using Core.Entities;
using Core.Services;
using Plumbing;

namespace Web.Common.Cache
{
    public class CacheBuster
    {
        private readonly Dependencies _deps;
        private readonly ICacheContainer _cache;

        public CacheBuster(Dependencies deps)
        {
            _deps = deps;
            _cache = _deps.CacheContainer;
        }

        public void UserAdded()
        {
            var key = CacheKeyProvider.UserIdsKey();
            _cache.Remove(key);
        }

        public void UserUpdated(int userId)
        {
            var user = _deps.UserRepository.GetById(userId);

            var singleUserKey = CacheKeyProvider.UserKey(userId);
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
            var bunch = _deps.BunchRepository.GetById(bunchId);

            var singleHomegameKey = CacheKeyProvider.BunchKey(bunchId);
            _cache.Remove(singleHomegameKey);

            var slugKey = CacheKeyProvider.BunchIdBySlugKey(bunch.Slug);
            _cache.Remove(slugKey);
        }

        public void PlayerAdded(int playerId)
        {
            var player = _deps.PlayerRepository.GetById(playerId);
            var bunch = _deps.BunchRepository.GetById(player.BunchId);

            var key = CacheKeyProvider.PlayerIdsKey(bunch.Id);
            _cache.Remove(key);
        }

        public void PlayerUpdated(int playerId)
        {
            var singleUserKey = CacheKeyProvider.PlayerKey(playerId);
            _cache.Remove(singleUserKey);
        }

        public void PlayerDeleted(int playerId)
        {
            var player = _deps.PlayerRepository.GetById(playerId);

            var key = CacheKeyProvider.PlayerIdsKey(player.BunchId);
            _cache.Remove(key);

            var singleUserKey = CacheKeyProvider.PlayerKey(playerId);
            _cache.Remove(singleUserKey);
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
            var singleCashgameKey = CacheKeyProvider.CashgameKey(cashgameId);
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