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

        public IList<Player> GetList(int bunchId)
        {
            throw new System.NotImplementedException();
        }

        public IList<Player> GetList(IList<int> ids)
        {
            throw new System.NotImplementedException();
        }

        public Player GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Player GetByName(int bunchId, string name)
        {
            throw new System.NotImplementedException();
        }

        public Player GetByUserId(int bunchId, int userId)
        {
            throw new System.NotImplementedException();
        }

        public int Add(Player player)
        {
            throw new System.NotImplementedException();
        }

        public bool JoinHomegame(Player player, Bunch bunch, int userId)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int playerId)
        {
            throw new System.NotImplementedException();
        }
    }
}