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

        public void BunchAdded()
        {
            var key = _cacheKeyProvider.BunchIdsKey();
            _cacheContainer.Remove(key);
        }

        public void BunchUpdated(Bunch bunch)
        {
            var singleHomegameKey = _cacheKeyProvider.BunchKey(bunch.Id);
            _cacheContainer.Remove(singleHomegameKey);

            var slugKey = _cacheKeyProvider.BunchIdBySlugKey(bunch.Slug);
            _cacheContainer.Remove(slugKey);
        }

        public void PlayerAdded(Bunch bunch)
        {
            var key = _cacheKeyProvider.PlayerIdsKey(bunch.Id);
            _cacheContainer.Remove(key);
        }

        public void PlayerUpdated(Player player)
        {
            var singleUserKey = _cacheKeyProvider.PlayerKey(player.Id);
            _cacheContainer.Remove(singleUserKey);
        }

        public void PlayerDeleted(Bunch bunch, Player player)
        {
            var key = _cacheKeyProvider.PlayerIdsKey(bunch.Id);
            _cacheContainer.Remove(key);

            var singleUserKey = _cacheKeyProvider.PlayerKey(player.Id);
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
            var singleCashgameKey = _cacheKeyProvider.CashgameKey(cashgameId);
            _cacheContainer.Remove(singleCashgameKey);
        }

        private void ClearRunningCashgame(int homegameId)
        {
            var runningCashgameKey = _cacheKeyProvider.CashgameIdByRunningKey(homegameId);
            _cacheContainer.Remove(runningCashgameKey);
        }

        private void ClearCashgameList(Cashgame cashgame)
        {
            var allTimeCacheKey = _cacheKeyProvider.CashgameIdsKey(cashgame.BunchId, GameStatus.Finished);
            _cacheContainer.Remove(allTimeCacheKey);
            if (cashgame.StartTime.HasValue)
            {
                var currentYearCacheKey = _cacheKeyProvider.CashgameIdsKey(cashgame.BunchId, GameStatus.Finished, cashgame.StartTime.Value.Year);
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