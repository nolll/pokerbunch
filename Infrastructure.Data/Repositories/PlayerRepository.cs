using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using System.Linq;
using Infrastructure.Data.Cache;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Repositories
{
	public class PlayerRepository : IPlayerRepository
    {
	    private readonly IPlayerStorage _playerStorage;
	    private readonly ICacheContainer _cacheContainer;
	    private readonly ICacheBuster _cacheBuster;
	    private readonly IUserRepository _userRepository;

	    public PlayerRepository(
            IPlayerStorage playerStorage,
            ICacheContainer cacheContainer,
            ICacheBuster cacheBuster,
            IUserRepository userRepository)
	    {
	        _playerStorage = playerStorage;
	        _cacheContainer = cacheContainer;
	        _cacheBuster = cacheBuster;
	        _userRepository = userRepository;
	    }

        public IList<Player> GetList(Bunch bunch)
        {
            var ids = GetIds(bunch);
            return GetList(ids);
        }

	    public IList<Player> GetList(IList<int> ids)
        {
            var players = _cacheContainer.GetEachAndStore(GetListUncached, TimeSpan.FromMinutes(CacheTime.Long), ids);
            return players.ToList();
        }

        private IList<Player> GetListUncached(IList<int> ids)
        {
            var rawPlayers = _playerStorage.GetPlayerList(ids);
            return rawPlayers.Select(CreatePlayer).ToList();
        }

        private IList<int> GetIds(Bunch bunch)
        {
            var cacheKey = CacheKeyProvider.PlayerIdsKey(bunch.Id);
            return _cacheContainer.GetAndStore(() => _playerStorage.GetPlayerIdList(bunch.Id), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        public Player GetById(int id)
        {
            var cacheKey = CacheKeyProvider.PlayerKey(id);
            return _cacheContainer.GetAndStore(() => GetByIdUncached(id), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        private Player GetByIdUncached(int id)
        {
            var rawUser = _playerStorage.GetPlayerById(id);
            return rawUser != null ? CreatePlayer(rawUser) : null;
        }

        public Player GetByName(Bunch bunch, string name)
        {
            var playerId = GetIdByName(bunch, name);
            return playerId.HasValue ? GetById(playerId.Value) : null;
        }

        private int? GetIdByName(Bunch bunch, string name)
        {
            var cacheKey = CacheKeyProvider.PlayerIdByNameKey(bunch.Id, name);
            return _cacheContainer.GetAndStore(() => _playerStorage.GetPlayerIdByName(bunch.Id, name), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        public Player GetByUserName(Bunch bunch, string userName)
        {
            var playerId = GetIdByUserName(bunch, userName);
            return playerId.HasValue ? GetById(playerId.Value) : null;
        }

        private int? GetIdByUserName(Bunch bunch, string userName)
        {
            var cacheKey = CacheKeyProvider.PlayerIdByUserNameKey(bunch.Id, userName);
            return _cacheContainer.GetAndStore(() => _playerStorage.GetPlayerIdByUserName(bunch.Id, userName), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

		public int Add(Bunch bunch, string playerName)
        {
            var playerId = _playerStorage.AddPlayer(bunch.Id, playerName);
            _cacheBuster.PlayerAdded(bunch);
		    return playerId;
		}

		public int Add(Bunch bunch, User user, Role role)
        {
            var playerId = _playerStorage.AddPlayerWithUser(bunch.Id, user.Id, (int)role);
            _cacheBuster.PlayerAdded(bunch);
		    return playerId;
		}

		public bool JoinHomegame(Player player, Bunch bunch, User user)
        {
            var success = _playerStorage.JoinHomegame(player.Id, (int)player.Role, bunch.Id, user.Id);
            _cacheBuster.PlayerUpdated(player);
		    return success;
		}

		public bool Delete(Bunch bunch, Player player)
        {
			var success = _playerStorage.DeletePlayer(player.Id);
            _cacheBuster.PlayerDeleted(bunch, player);
            return success;
		}

	    private Player CreatePlayer(RawPlayer rawPlayer)
        {
            return new Player(
                rawPlayer.Id,
                rawPlayer.UserId,
                GetDisplayName(rawPlayer),
                (Role)rawPlayer.Role);
        }

        private string GetDisplayName(RawPlayer rawPlayer)
        {
            if (rawPlayer.IsUser && rawPlayer.DisplayName == null)
            {
                var user = _userRepository.GetById(rawPlayer.UserId);
                return user.DisplayName;
            }
            return rawPlayer.DisplayName;
        }
	}
}