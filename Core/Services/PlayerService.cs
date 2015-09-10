using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;

namespace Core.Services
{
    public class PlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public IList<Player> GetList(int bunchId)
        {
            return _playerRepository.GetList(bunchId);
        }

        public IList<Player> GetList(IList<int> ids)
        {
            return _playerRepository.GetList(ids);
        }

        public Player GetById(int id)
        {
            return _playerRepository.GetById(id);
        }

        public Player GetByName(int bunchId, string name)
        {
            return _playerRepository.GetByName(bunchId, name);
        }

        public Player GetByUserId(int bunchId, int userId)
        {
            return _playerRepository.GetByUserId(bunchId, userId);
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