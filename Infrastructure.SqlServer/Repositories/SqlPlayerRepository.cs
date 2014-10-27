using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Infrastructure.Cache;
using Infrastructure.SqlServer.Interfaces;
using Infrastructure.Storage;

namespace Infrastructure.SqlServer.Repositories
{
	public class SqlPlayerRepository : IPlayerRepository
    {
	    private readonly IPlayerStorage _playerStorage;
	    private readonly ICacheContainer _cacheContainer;
	    private readonly ICacheBuster _cacheBuster;
	    private readonly IUserRepository _userRepository;

	    public SqlPlayerRepository(
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

        public IList<Player> GetList(int bunchId)
        {
            var ids = GetIds(bunchId);
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

        private IList<int> GetIds(int bunchId)
        {
            var cacheKey = CacheKeyProvider.PlayerIdsKey(bunchId);
            return _cacheContainer.GetAndStore(() => _playerStorage.GetPlayerIdList(bunchId), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        public Player GetById(int id)
        {
            var cacheKey = CacheKeyProvider.PlayerKey(id);
            return _cacheContainer.GetAndStore(() => GetByIdUncached(id), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
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
            var cacheKey = CacheKeyProvider.PlayerIdByNameKey(bunchId, name);
            return _cacheContainer.GetAndStore(() => _playerStorage.GetPlayerIdByName(bunchId, name), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
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

		public int Add(int bunchId, string playerName)
        {
            var playerId = _playerStorage.AddPlayer(bunchId, playerName);
            _cacheBuster.PlayerAdded(bunchId);
		    return playerId;
		}

		public int Add(Bunch bunch, User user, Role role)
        {
            var playerId = _playerStorage.AddPlayerWithUser(bunch.Id, user.Id, (int)role);
            _cacheBuster.PlayerAdded(bunch.Id);
		    return playerId;
		}

		public bool JoinHomegame(Player player, Bunch bunch, User user)
        {
            var success = _playerStorage.JoinHomegame(player.Id, (int)player.Role, bunch.Id, user.Id);
            _cacheBuster.PlayerUpdated(player.Id);
		    return success;
		}

		public bool Delete(int playerId)
        {
			var success = _playerStorage.DeletePlayer(playerId);
            _cacheBuster.PlayerDeleted(playerId);
            return success;
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
                var user = _userRepository.GetById(rawPlayer.UserId);
                return user.DisplayName;
            }
            return rawPlayer.DisplayName;
        }
	}
}