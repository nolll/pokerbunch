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

        public IList<Player> List(string slug)
        {
            return _playerRepository.List(slug);
        }

        public IList<Player> Get(IList<string> ids)
        {
            return _playerRepository.Get(ids);
        }

        public Player Get(string id)
        {
            return _playerRepository.Get(id);
        }

        public Player GetByUser(string slug, string userId)
        {
            return _playerRepository.GetByUser(slug, userId);
        }

        public string Add(Player player)
        {
            return _playerRepository.Add(player);
        }

        public bool JoinBunch(Player player, Bunch bunch, string userId)
        {
            return _playerRepository.JoinBunch(player, bunch, userId);
        }

        public void Delete(string playerId)
        {
            _playerRepository.Delete(playerId);
        }
    }
}