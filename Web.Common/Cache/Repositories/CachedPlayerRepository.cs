using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Web.Common.Cache.Repositories
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

        public IList<int> Find(int bunchId)
        {
            return _playerRepository.Find(bunchId);
        }

        public IList<int> Find(int bunchId, string name)
        {
            return _playerRepository.Find(bunchId, name);
        }

        public IList<int> Find(int bunchId, int userId)
        {
            return _playerRepository.Find(bunchId, userId);
        }

        public IList<Player> GetList(int bunchId)
        {
            return _playerRepository.GetList(bunchId);
        }

        public IList<Player> Get(IList<int> ids)
        {
            return _playerRepository.Get(ids);
        }

        public Player Get(int id)
        {
            return _playerRepository.Get(id);
        }

        public int Add(Player player)
        {
            return _playerRepository.Add(player);
        }

        public bool JoinHomegame(Player player, Bunch bunch, int userId)
        {
            return _playerRepository.JoinHomegame(player, bunch, userId);
        }

        public bool Delete(int playerId)
        {
            return _playerRepository.Delete(playerId);
        }
    }
}