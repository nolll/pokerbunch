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

        public Player Get(int id)
        {
            return _cacheContainer.GetAndStore(_playerRepository.Get, id, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<Player> Get(IList<int> ids)
        {
            return _cacheContainer.GetAndStore(_playerRepository.Get, ids, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<int> Find(string slug)
        {
            return _playerRepository.Find(slug);
        }

        public IList<int> Find(string slug, string name)
        {
            return _playerRepository.Find(slug, name);
        }

        public IList<int> Find(string slug, int userId)
        {
            return _playerRepository.Find(slug, userId);
        }

        public int Add(Player player)
        {
            return _playerRepository.Add(player);
        }

        public bool JoinHomegame(Player player, Bunch bunch, int userId)
        {
            return _playerRepository.JoinHomegame(player, bunch, userId);
        }

        public void Delete(int playerId)
        {
            _playerRepository.Delete(playerId);
            _cacheContainer.Remove<Player>(playerId);
        }
    }
}