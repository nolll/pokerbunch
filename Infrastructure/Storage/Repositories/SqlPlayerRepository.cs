using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Storage.Classes;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage.Repositories
{
	public class SqlPlayerRepository : IPlayerRepository
    {
	    private readonly IPlayerStorage _playerStorage;
	    private readonly IUserRepository _userRepository;

	    public SqlPlayerRepository(
            IPlayerStorage playerStorage,
            IUserRepository userRepository)
	    {
	        _playerStorage = playerStorage;
	        _userRepository = userRepository;
	    }

        public IList<Player> GetList(int bunchId)
        {
            var ids = GetIds(bunchId);
            return GetList(ids);
        }

	    public IList<Player> GetList(IList<int> ids)
	    {
	        return GetListUncached(ids);
        }

        private IList<Player> GetListUncached(IList<int> ids)
        {
            var rawPlayers = _playerStorage.GetPlayerList(ids);
            return rawPlayers.Select(CreatePlayer).ToList();
        }

        private IList<int> GetIds(int bunchId)
        {
            return _playerStorage.GetPlayerIdList(bunchId);
        }

        public Player GetById(int id)
        {
            return GetByIdUncached(id);
        }

        private Player GetByIdUncached(int id)
        {
            var rawPlayer = _playerStorage.GetPlayerById(id);
            return rawPlayer != null ? CreatePlayer(rawPlayer) : null;
        }

        public Player GetByName(int bunchId, string name)
        {
            var playerId = GetIdByName(bunchId, name);
            return playerId.HasValue ? GetById(playerId.Value) : null;
        }

        private int? GetIdByName(int bunchId, string name)
        {
            return _playerStorage.GetPlayerIdByName(bunchId, name);
        }

	    public Player GetByUserId(int bunchId, int userId)
	    {
            var playerId = GetIdByUserId(bunchId, userId);
            return playerId.HasValue ? GetById(playerId.Value) : null;
	    }

        private int? GetIdByUserId(int bunchId, int userId)
        {
            return _playerStorage.GetPlayerIdByUserId(bunchId, userId);
        }

        public int Add(Player player)
        {
            var rawPlayer = RawPlayer.Create(player);
            return _playerStorage.AddPlayer(rawPlayer);
        }

		public bool JoinHomegame(Player player, Bunch bunch, int userId)
        {
            return _playerStorage.JoinHomegame(player.Id, (int)player.Role, bunch.Id, userId);
		}

		public bool Delete(int playerId)
        {
			return _playerStorage.DeletePlayer(playerId);
		}

	    private Player CreatePlayer(RawPlayer rawPlayer)
        {
            return new Player(
                rawPlayer.BunchId,
                rawPlayer.Id,
                rawPlayer.UserId,
                GetDisplayName(rawPlayer),
                (Role)rawPlayer.Role);
        }

        private string GetDisplayName(RawPlayer rawPlayer)
        {
            if (rawPlayer.IsUser && rawPlayer.DisplayName == null)
            {
                var user = _userRepository.Get(rawPlayer.UserId);
                return user.DisplayName;
            }
            return rawPlayer.DisplayName;
        }
	}
}