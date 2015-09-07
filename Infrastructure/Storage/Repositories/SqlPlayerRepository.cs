using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Infrastructure.Storage.Cache;
using Infrastructure.Storage.Classes;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage.Repositories
{
	public class SqlPlayerRepository : IPlayerRepository
    {
	    private readonly IPlayerStorage _playerStorage;
	    private readonly ICacheContainer _cacheContainer;
	    private readonly IUserRepository _userRepository;

	    public SqlPlayerRepository(
            IPlayerStorage playerStorage,
            ICacheContainer cacheContainer,
            IUserRepository userRepository)
	    {
	        _playerStorage = playerStorage;
	        _cacheContainer = cacheContainer;
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

	    public Player GetByUserId(int bunchId, int userId)
	    {
            var playerId = GetIdByUserId(bunchId, userId);
            return playerId.HasValue ? GetById(playerId.Value) : null;
	    }

        private int? GetIdByUserId(int bunchId, int userId)
        {
            var cacheKey = CacheKeyProvider.PlayerIdByUserIdKey(bunchId, userId);
            return _cacheContainer.GetAndStore(() => _playerStorage.GetPlayerIdByUserId(bunchId, userId), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
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
                var user = _userRepository.GetById(rawPlayer.UserId);
                return user.DisplayName;
            }
            return rawPlayer.DisplayName;
        }
	}
}