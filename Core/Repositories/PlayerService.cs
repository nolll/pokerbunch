using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;

namespace Core.Repositories
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public IList<Player> GetList(string slug)
        {
            var ids = _playerRepository.Find(slug);
            return _playerRepository.Get(ids);
        }

        public IList<Player> Get(IList<string> ids)
        {
            return _playerRepository.Get(ids);
        }

        public Player Get(string id)
        {
            return _playerRepository.Get(id);
        }

        public Player GetByUserId(string slug, string userId)
        {
            var ids = _playerRepository.FindByUserId(slug, userId);
            if (!ids.Any())
                return null;
            return _playerRepository.Get(ids).First();
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
        }
    }
}