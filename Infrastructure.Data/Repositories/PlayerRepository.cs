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

        public IList<Player> GetList(Homegame homegame)
        {
            var ids = GetIds(homegame);
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

        private IList<int> GetIds(Homegame homegame)
        {
            var cacheKey = _cacheKeyProvider.PlayerIdsKey(homegame.Id);
            return _cacheContainer.GetAndStore(() => _playerStorage.GetPlayerIdList(homegame.Id), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
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

        public Player GetByName(Homegame homegame, string name)
        {
            var playerId = GetIdByName(homegame, name);
            return playerId.HasValue ? GetById(playerId.Value) : null;
        }

        private int? GetIdByName(Homegame homegame, string name)
        {
            var cacheKey = _cacheKeyProvider.PlayerIdByNameKey(homegame.Id, name);
            return _cacheContainer.GetAndStore(() => _playerStorage.GetPlayerIdByName(homegame.Id, name), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        public Player GetByUserName(Homegame homegame, string userName)
        {
            var playerId = GetIdByUserName(homegame, userName);
            return playerId.HasValue ? GetById(playerId.Value) : null;
        }

        private int? GetIdByUserName(Homegame homegame, string userName)
        {
            var cacheKey = _cacheKeyProvider.PlayerIdByUserNameKey(homegame.Id, userName);
            return _cacheContainer.GetAndStore(() => _playerStorage.GetPlayerIdByUserName(homegame.Id, userName), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

		public int Add(Homegame homegame, string playerName)
        {
            var playerId = _playerStorage.AddPlayer(homegame.Id, playerName);
            _cacheBuster.PlayerAdded(homegame);
		    return playerId;
		}

		public int Add(Homegame homegame, User user, Role role)
        {
            var playerId = _playerStorage.AddPlayerWithUser(homegame.Id, user.Id, (int)role);
            _cacheBuster.PlayerAdded(homegame);
		    return playerId;
		}

		public bool JoinHomegame(Player player, Homegame homegame, User user)
        {
            var success = _playerStorage.JoinHomegame(player.Id, (int)player.Role, homegame.Id, user.Id);
            _cacheBuster.PlayerUpdated(player);
		    return success;
		}

		public bool Delete(Homegame homegame, Player player)
        {
			var success = _playerStorage.DeletePlayer(player.Id);
            _cacheBuster.PlayerDeleted(homegame, player);
            return success;
		}

	}

}