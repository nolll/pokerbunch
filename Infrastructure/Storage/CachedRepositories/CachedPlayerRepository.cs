using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Infrastructure.Storage.CachedRepositories
{
    public class CachedPlayerRepository : IPlayerRepository
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ICacheContainer _cacheContainer;

        public CachedPlayerRepository(IPlayerRepository playerRepository, ICacheContainer cacheContainer)
        {
            _playerRepository = playerRepository;
            _cacheContainer = cacheContainer;
        }

        public Player Get(string id)
        {
            return _cacheContainer.GetAndStore(_playerRepository.Get, id, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<Player> Get(IList<string> ids)
        {
            return _cacheContainer.GetAndStore(_playerRepository.Get, ids, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<string> Find(string slug)
        {
            return _playerRepository.Find(slug);
        }

        public IList<string> FindByName(string slug, string name)
        {
            return _playerRepository.FindByName(slug, name);
        }

        public IList<string> FindByUserId(string slug, string userId)
        {
            return _playerRepository.FindByUserId(slug, userId);
        }

        public string Add(Player player)
        {
            return _playerRepository.Add(player);
        }

        public bool JoinHomegame(Player player, Bunch bunch, string userId)
        {
            return _playerRepository.JoinHomegame(player, bunch, userId);
        }

        public void Delete(string playerId)
        {
            _playerRepository.Delete(playerId);
            _cacheContainer.Remove<Player>(playerId);
        }
    }
}