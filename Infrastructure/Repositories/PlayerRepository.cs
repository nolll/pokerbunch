using System;
using System.Collections.Generic;
using Core.Classes;
using Core.Repositories;
using Infrastructure.Caching;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Factories;
using System.Linq;

namespace Infrastructure.Repositories {

	public class PlayerRepository : IPlayerRepository
    {
        private const string PlayerCacheKey = "Player";
        private const string HomegamePlayersCacheKey = "HomegamePlayers";

	    private readonly IPlayerStorage _playerStorage;
	    private readonly IPlayerFactory _playerFactory;
	    private readonly ICacheContainer _cacheContainer;

	    public PlayerRepository(
            IPlayerStorage playerStorage,
            IPlayerFactory playerFactory,
            ICacheContainer cacheContainer)
	    {
	        _playerStorage = playerStorage;
	        _playerFactory = playerFactory;
	        _cacheContainer = cacheContainer;
	    }

	    public List<Player> GetAll(Homegame homegame)
	    {
            var players = new List<Player>();
            var playerIds = GetPlayerIds(homegame);
            var uncachedIds = new List<int>();
            foreach (var id in playerIds)
            {
                var cacheKey = _cacheContainer.ConstructCacheKey(PlayerCacheKey, id);
                var cached = _cacheContainer.Get<Player>(cacheKey);
                if (cached != null)
                {
                    players.Add(cached);
                }
                else
                {
                    uncachedIds.Add(id);
                }
            }

            if (uncachedIds.Count > 0)
            {
                var rawPlayers = _playerStorage.GetPlayers(uncachedIds);
                var newPlayers = rawPlayers.Select(_playerFactory.Create).ToList();
                foreach (var player in newPlayers)
                {
                    _cacheContainer.Insert(_cacheContainer.ConstructCacheKey(PlayerCacheKey, player.Id), player, TimeSpan.FromMinutes(CacheTime.Long));
                }
                players.AddRange(newPlayers);
            }

            return players.OrderBy(o => o.DisplayName).ToList();
		}

        private IEnumerable<int> GetPlayerIds(Homegame homegame)
        {
            var cacheKey = _cacheContainer.ConstructCacheKey(HomegamePlayersCacheKey, homegame.Id);
            var cached = _cacheContainer.Get<List<int>>(cacheKey);
            if (cached != null)
            {
                return cached;
            }
            var uncached = _playerStorage.GetPlayerIds(homegame.Id);
            if (uncached != null)
            {
                _cacheContainer.Insert(cacheKey, uncached, TimeSpan.FromMinutes(CacheTime.Long));
            }
            return uncached;
        }

        public Player GetPlayerById(int id)
        {
            var cacheKey = _cacheContainer.ConstructCacheKey(PlayerCacheKey, id);
            var cached = _cacheContainer.Get<Player>(cacheKey);
            if (cached != null)
            {
                return cached;
            }
            var rawUser = _playerStorage.GetPlayerById(id);
            var uncached = rawUser != null ? _playerFactory.Create(rawUser) : null;
            if (uncached != null)
            {
                _cacheContainer.Insert(cacheKey, uncached, TimeSpan.FromMinutes(CacheTime.Long));
            }
            return uncached;
        }

		public Player GetByName(Homegame homegame, string name){
			var playerId = _playerStorage.GetPlayerIdByName(homegame.Id, name);
		    return playerId.HasValue ? GetPlayerById(playerId.Value) : null;
		}

		public Player GetByUserName(Homegame homegame, string userName){
            var playerId = _playerStorage.GetPlayerIdByUserName(homegame.Id, userName);
            return playerId.HasValue ? GetPlayerById(playerId.Value) : null;
		}

		public int AddPlayer(Homegame homegame, string playerName){
            var playerId = _playerStorage.AddPlayer(homegame.Id, playerName);
            ClearPlayerListFromCache(homegame.Id);
		    return playerId;
		}

		public int AddPlayerWithUser(Homegame homegame, User user, Role role){
            var playerId = _playerStorage.AddPlayerWithUser(homegame.Id, user.Id, (int)role);
            ClearPlayerListFromCache(homegame.Id);
		    return playerId;
		}

		public bool JoinHomegame(Player player, Homegame homegame, User user){
            var success = _playerStorage.JoinHomegame(player.Id, (int)player.Role, homegame.Id, user.Id);
            ClearPlayerFromCache(player.Id);
            ClearPlayerListFromCache(homegame.Id);
		    return success;
		}

		public bool DeletePlayer(Homegame homegame, Player player){
			var success = _playerStorage.DeletePlayer(player.Id);
            ClearPlayerFromCache(player.Id);
            ClearPlayerListFromCache(homegame.Id);
            return success;
		}

        private void ClearPlayerFromCache(int playerId)
        {
            var cacheKey = _cacheContainer.ConstructCacheKey(PlayerCacheKey, playerId);
            _cacheContainer.Remove(cacheKey);
        }

        private void ClearPlayerListFromCache(int homegameId)
        {
            var cacheKey = _cacheContainer.ConstructCacheKey(HomegamePlayersCacheKey, homegameId);
            _cacheContainer.Remove(cacheKey);
        }

	}

}