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

		public Player GetPlayerById(Homegame homegame, int id){
            var rawPlayer = _playerStorage.GetPlayerById(homegame.Id, id);
			return _playerFactory.Create(rawPlayer);
		}

		public Player GetByName(Homegame homegame, string name){
			var rawPlayer = _playerStorage.GetPlayerByName(homegame.Id, name);
            return _playerFactory.Create(rawPlayer);
		}

		public Player GetByUserName(Homegame homegame, string userName){
            var rawPlayer = _playerStorage.GetPlayerByUserName(homegame.Id, userName);
            return _playerFactory.Create(rawPlayer);
		}

		public int AddPlayer(Homegame homegame, string playerName){
			return _playerStorage.AddPlayer(homegame.Id, playerName);
		}

		public int AddPlayerWithUser(Homegame homegame, User user, Role role){
			return _playerStorage.AddPlayerWithUser(homegame.Id, user.Id, (int)role);
		}

		public bool JoinHomegame(Player player, Homegame homegame, User user){
			return _playerStorage.JoinHomegame(player.Id, (int)player.Role, homegame.Id, user.Id);
		}

		public bool DeletePlayer(Player player){
			return _playerStorage.DeletePlayer(player.Id);
		}

	}

}