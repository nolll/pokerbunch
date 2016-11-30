using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Infrastructure.Storage.SqlDb;

namespace Infrastructure.Storage.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly SqlPlayerDb _playerDb;
        private readonly ICacheContainer _cacheContainer;

        public PlayerRepository(SqlServerStorageProvider db, ICacheContainer cacheContainer)
        {
            _playerDb = new SqlPlayerDb(db);
            _cacheContainer = cacheContainer;
        }

        public Player Get(string id)
        {
            return _cacheContainer.GetAndStore(_playerDb.Get, id, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<Player> Get(IList<string> ids)
        {
            return _cacheContainer.GetAndStore(_playerDb.Get, ids, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<Player> List(string bunchId)
        {
            var ids = _playerDb.Find(bunchId);
            return Get(ids);
        }

        public Player GetByUser(string bunchId, string userId)
        {
            var ids = _playerDb.FindByUserId(bunchId, userId);
            if (!ids.Any())
                return null;
            return Get(ids).First();
        }

        public string Add(Player player)
        {
            return _playerDb.Add(player);
        }

        public bool JoinBunch(Player player, Bunch bunch, string userId)
        {
            return _playerDb.JoinHomegame(player, bunch, userId);
        }

        public void Delete(string playerId)
        {
            _playerDb.Delete(playerId);
            _cacheContainer.Remove<Player>(playerId);
        }
    }
}