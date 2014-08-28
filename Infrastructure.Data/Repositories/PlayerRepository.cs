using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using System.Linq;
using Infrastructure.Data.Cache;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Mappers;

namespace Infrastructure.Data.Repositories {

	public class PlayerRepository : IPlayerRepository
    {
	    private readonly IPlayerStorage _playerStorage;
	    private readonly IPlayerDataMapper _playerDataMapper;
	    private readonly ICacheContainer _cacheContainer;
	    private readonly ICacheKeyProvider _cacheKeyProvider;
	    private readonly ICacheBuster _cacheBuster;

	    public PlayerRepository(
            IPlayerStorage playerStorage,
            IPlayerDataMapper playerDataMapper,
            ICacheContainer cacheContainer,
            ICacheKeyProvider cacheKeyProvider,
            ICacheBuster cacheBuster)
	    {
	        _playerStorage = playerStorage;
	        _playerDataMapper = playerDataMapper;
	        _cacheContainer = cacheContainer;
	        _cacheKeyProvider = cacheKeyProvider;
	        _cacheBuster = cacheBuster;
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
            return rawPlayers.Select(_playerDataMapper.Create).ToList();
        }

        private IList<int> GetIds(Bunch bunch)
        {
            var cacheKey = _cacheKeyProvider.PlayerIdsKey(bunch.Id);
            return _cacheContainer.GetAndStore(() => _playerStorage.GetPlayerIdList(bunch.Id), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        public Player GetById(int id)
        {
            var cacheKey = _cacheKeyProvider.PlayerKey(id);
            return _cacheContainer.GetAndStore(() => GetByIdUncached(id), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        private Player GetByIdUncached(int id)
        {
            var rawUser = _playerStorage.GetPlayerById(id);
            return rawUser != null ? _playerDataMapper.Create(rawUser) : null;
        }

        public Player GetByName(Bunch bunch, string name)
        {
            var playerId = GetIdByName(bunch, name);
            return playerId.HasValue ? GetById(playerId.Value) : null;
        }

        private int? GetIdByName(Bunch bunch, string name)
        {
            var cacheKey = _cacheKeyProvider.PlayerIdByNameKey(bunch.Id, name);
            return _cacheContainer.GetAndStore(() => _playerStorage.GetPlayerIdByName(bunch.Id, name), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        public Player GetByUserName(Bunch bunch, string userName)
        {
            var playerId = GetIdByUserName(bunch, userName);
            return playerId.HasValue ? GetById(playerId.Value) : null;
        }

        private int? GetIdByUserName(Bunch bunch, string userName)
        {
            var cacheKey = _cacheKeyProvider.PlayerIdByUserNameKey(bunch.Id, userName);
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

	}

}