using System.Linq;
using Core.Entities;
using Core.Services;
using Infrastructure.Cache;
using Infrastructure.SqlServer.Interfaces;
using Infrastructure.Storage;

namespace Infrastructure.SqlServer
{
    public class CacheBuster : ICacheBuster
    {
        private readonly ICacheContainer _cacheContainer;
        private readonly IUserStorage _userStorage;
        private readonly IBunchStorage _bunchStorage;
        private readonly IPlayerStorage _playerStorage;
        private readonly ICashgameStorage _cashgameStorage;
        private readonly ICheckpointStorage _checkpointStorage;

        public CacheBuster(
            ICacheContainer cacheContainer,
            IUserStorage userStorage,
            IBunchStorage bunchStorage,
            IPlayerStorage playerStorage,
            ICashgameStorage cashgameStorage,
            ICheckpointStorage checkpointStorage)
        {
            _cacheContainer = cacheContainer;
            _userStorage = userStorage;
            _bunchStorage = bunchStorage;
            _playerStorage = playerStorage;
            _cashgameStorage = cashgameStorage;
            _checkpointStorage = checkpointStorage;
        }

        public void UserAdded()
        {
            var key = CacheKeyProvider.UserIdsKey();
            _cacheContainer.Remove(key);
        }

        public void UserUpdated(int userId)
        {
            var user = _userStorage.GetUserById(userId);

            var singleUserKey = CacheKeyProvider.UserKey(userId);
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

        public void BunchUpdated(int bunchId)
        {
            var bunch = _bunchStorage.GetById(bunchId);

            var singleHomegameKey = CacheKeyProvider.BunchKey(bunchId);
            _cacheContainer.Remove(singleHomegameKey);

            var slugKey = CacheKeyProvider.BunchIdBySlugKey(bunch.Slug);
            _cacheContainer.Remove(slugKey);
        }

        public void PlayerAdded(int playerId)
        {
            var player = _playerStorage.GetPlayerById(playerId);
            var bunch = _bunchStorage.GetById(player.BunchId);

            var key = CacheKeyProvider.PlayerIdsKey(bunch.Id);
            _cacheContainer.Remove(key);
        }

        public void PlayerUpdated(int playerId)
        {
            var singleUserKey = CacheKeyProvider.PlayerKey(playerId);
            _cacheContainer.Remove(singleUserKey);
        }

        public void PlayerDeleted(int playerId)
        {
            var player = _playerStorage.GetPlayerById(playerId);

            var key = CacheKeyProvider.PlayerIdsKey(player.BunchId);
            _cacheContainer.Remove(key);

            var singleUserKey = CacheKeyProvider.PlayerKey(playerId);
            _cacheContainer.Remove(singleUserKey);
        }

        public void CashgameStarted(int cashgameId)
        {
            var cashgame = _cashgameStorage.GetGame(cashgameId);

            ClearRunningCashgame(cashgame.BunchId);
        }

        public void CashgameUpdated(int cashgameId)
        {
            var cashgame = _cashgameStorage.GetGame(cashgameId);

            ClearRunningCashgame(cashgame.BunchId);
            ClearCashgame(cashgameId);
            ClearCashgameYears(cashgame.BunchId);
            ClearCashgameList(cashgame);
        }

        private void ClearCashgame(int cashgameId)
        {
            var singleCashgameKey = CacheKeyProvider.CashgameKey(cashgameId);
            _cacheContainer.Remove(singleCashgameKey);
        }

        private void ClearRunningCashgame(int bunchId)
        {
            var runningCashgameKey = CacheKeyProvider.CashgameIdByRunningKey(bunchId);
            _cacheContainer.Remove(runningCashgameKey);
        }

        private void ClearCashgameList(RawCashgame cashgame)
        {
            var allTimeCacheKey = CacheKeyProvider.CashgameIdsKey(cashgame.BunchId, GameStatus.Finished);
            _cacheContainer.Remove(allTimeCacheKey);

            var checkpoints = _checkpointStorage.GetCheckpoints(cashgame.Id);
            if (checkpoints.Any())
            {
                var year = checkpoints.First().Timestamp.Year;
                var currentYearCacheKey = CacheKeyProvider.CashgameIdsKey(cashgame.BunchId, GameStatus.Finished, year);
                _cacheContainer.Remove(currentYearCacheKey);
            }
        }

        private void ClearCashgameYears(int bunchId)
        {
            var cacheKey = CacheKeyProvider.CashgameYearsKey(bunchId);
            _cacheContainer.Remove(cacheKey);
        }
    }
}