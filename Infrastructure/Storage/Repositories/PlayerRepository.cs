using System;
using System.Collections.Generic;
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

        public IList<string> Find(string bunchId)
        {
            return _playerDb.Find(bunchId);
        }

        public IList<string> FindByName(string bunchId, string name)
        {
            return _playerDb.FindByName(bunchId, name);
        }

        public IList<string> FindByUserId(string bunchId, string userId)
        {
            return _playerDb.FindByUserId(bunchId, userId);
        }

        public string Add(Player player)
        {
            return _playerDb.Add(player);
        }

        public bool JoinHomegame(Player player, Bunch bunch, string userId)
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