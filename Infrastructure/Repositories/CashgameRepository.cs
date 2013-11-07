using System;
using System.Collections.Generic;
using System.Globalization;
using Core.Classes;
using Core.Repositories;
using Infrastructure.Caching;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Factories;
using System.Linq;

namespace Infrastructure.Repositories {
	
	public class CashgameRepository : ICashgameRepository{

        private const string CashgameCacheKey = "Cashgame";
        private const string CashgameIdCacheKey = "CashgameId";
        private const string CashgameYearsCacheKey = "CashgameYears";

	    private readonly ICashgameStorage _cashgameStorage;
	    private readonly ICashgameFactory _cashgameFactory;
	    private readonly IPlayerRepository _playerRepository;
	    private readonly ICashgameSuiteFactory _cashgameSuiteFactory;
	    private readonly ICashgameResultFactory _cashgameResultFactory;
	    private readonly IRawCashgameFactory _rawCashgameFactory;
	    private readonly ICheckpointFactory _checkpointFactory;
	    private readonly ICacheContainer _cacheContainer;
	    private readonly ICheckpointStorage _checkpointStorage;

	    public CashgameRepository(
            ICashgameStorage cashgameStorage,
			ICashgameFactory cashgameFactory,
            IPlayerRepository playerRepository,
			ICashgameSuiteFactory cashgameSuiteFactory,
			ICashgameResultFactory cashgameResultFactory,
            IRawCashgameFactory rawCashgameFactory,
            ICheckpointFactory checkpointFactory,
            ICacheContainer cacheContainer,
            ICheckpointStorage checkpointStorage)
	    {
	        _cashgameStorage = cashgameStorage;
	        _cashgameFactory = cashgameFactory;
	        _playerRepository = playerRepository;
	        _cashgameSuiteFactory = cashgameSuiteFactory;
	        _cashgameResultFactory = cashgameResultFactory;
	        _rawCashgameFactory = rawCashgameFactory;
	        _checkpointFactory = checkpointFactory;
	        _cacheContainer = cacheContainer;
	        _checkpointStorage = checkpointStorage;
	    }

        public IList<Cashgame> GetPublished(Homegame homegame, int? year = null)
        {
            return GetGames(homegame, GameStatus.Published, year);
        }

		public Cashgame GetRunning(Homegame homegame){
			var games = GetGames(homegame, GameStatus.Running, null);
			if(games.Count == 0){
				return null;
			}
			return games[0];
		}

        public IList<Cashgame> GetAll(Homegame homegame, int? year = null)
        {
			return GetGames(homegame, null, year);
		}

        public Cashgame GetByDateString(Homegame homegame, string dateString)
        {
            var cashgameId = GetCashgameId(homegame.Id, dateString);
            if (!cashgameId.HasValue)
                return null;
            return GetCashgameById(homegame, cashgameId.Value);
        }

        private Cashgame GetCashgameById(Homegame homegame, int id)
        {
            var cacheKey = _cacheContainer.ConstructCacheKey(CashgameCacheKey, id);
            var cached = _cacheContainer.Get<Cashgame>(cacheKey);
            if (cached != null)
            {
                return cached;
            }
            var rawGame = _cashgameStorage.GetGame(id);
            var rawCheckpoints = _checkpointStorage.GetCheckpoints(id);
            var players = _playerRepository.GetAll(homegame);
            var uncached = GetGameFromRawGame(rawGame, rawCheckpoints, players, homegame.Timezone);
            if (uncached != null)
            {
                _cacheContainer.Insert(cacheKey, uncached, TimeSpan.FromMinutes(CacheTime.Long));
            }
            return uncached;
        }

        private int? GetCashgameId(int homegameId, string dateString)
        {
            var cacheKey = _cacheContainer.ConstructCacheKey(CashgameIdCacheKey, homegameId, dateString);
            var cached = _cacheContainer.Get<string>(cacheKey);
            if (cached != null)
            {
                return int.Parse(cached);
            }
            var uncached = _cashgameStorage.GetCashgameId(homegameId, dateString);
            if (uncached.HasValue)
            {
                _cacheContainer.Insert(cacheKey, uncached.Value.ToString(CultureInfo.InvariantCulture), TimeSpan.FromMinutes(CacheTime.Long));
            }
            return uncached;
        }

		public CashgameSuite GetSuite(Homegame homegame, int? year = null){
			var players = _playerRepository.GetAll(homegame);
			var cashgames = GetPublished(homegame, year);
			return _cashgameSuiteFactory.Create(cashgames, players);
		}

        public IList<int> GetYears(Homegame homegame)
        {
            var cacheKey = _cacheContainer.ConstructCacheKey(CashgameYearsCacheKey, homegame.Slug);
            var cached = _cacheContainer.Get<List<int>>(cacheKey);
            if (cached != null)
            {
                return cached;
            }
            var uncached = _cashgameStorage.GetYears(homegame.Slug);
            if (uncached != null)
            {
                _cacheContainer.Insert(cacheKey, uncached, TimeSpan.FromMinutes(CacheTime.Long));
            }
            return uncached;
        }

		private IList<Cashgame> GetGames(Homegame homegame, GameStatus? status = null, int? year = null){
			var rawGames = _cashgameStorage.GetGames(homegame.Id, (int?)status, year);
			var players = _playerRepository.GetAll(homegame);
			return GetGamesFromRawGames(rawGames, players, homegame.Timezone);
		}

        private IList<Cashgame> GetGamesFromRawGames(IEnumerable<RawCashgameWithResults> rawGames, List<Player> players, TimeZoneInfo timeZone)
        {
            return rawGames.Select(rawGame => GetGameFromRawGame(rawGame, players, timeZone)).ToList();
        }

	    private Cashgame GetGameFromRawGame(RawCashgameWithResults rawGame, List<Player> players, TimeZoneInfo timeZone)
        {
            var results = new List<CashgameResult>();
            var rawResults = rawGame.Results;
            foreach (var rawResult in rawResults)
            {
                results.Add(GetResultFromRawResult(rawResult, players, timeZone));
            }
            return _cashgameFactory.Create(rawGame.Location, rawGame.Status, rawGame.Id, results);
        }

        private Cashgame GetGameFromRawGame(RawCashgame rawGame, IEnumerable<RawCheckpoint> rawCheckpoints, List<Player> players, TimeZoneInfo timeZone)
        {
            var results = new List<CashgameResult>();
            var rawResults = GetRawResults(rawCheckpoints);
            foreach (var rawResult in rawResults)
            {
                results.Add(GetResultFromRawResult(rawResult, players, timeZone));
            }
            return _cashgameFactory.Create(rawGame.Location, rawGame.Status, rawGame.Id, results);
        }

        private IEnumerable<RawCashgameResult> GetRawResults(IEnumerable<RawCheckpoint> rawCheckpoints)
        {
            var results = new List<RawCashgameResult>();
            RawCashgameResult currentResult = null;
            var currentPlayerId = -1;
            foreach (var rawCheckpoint in rawCheckpoints)
            {
                if (currentResult == null || rawCheckpoint.PlayerId != currentPlayerId)
                {
                    currentResult = new RawCashgameResult(rawCheckpoint.PlayerId);
                    currentPlayerId = currentResult.PlayerId;
                    results.Add(currentResult);
                }
                currentResult.AddCheckpoint(rawCheckpoint);
            }
            return results;
        }

		private CashgameResult GetResultFromRawResult(RawCashgameResult rawResult, IEnumerable<Player> players, TimeZoneInfo timeZone){
			var player = GetPlayer(players, rawResult.PlayerId);
			var checkpoints = rawResult.Checkpoints.Select(o => _checkpointFactory.Create(o, timeZone)).ToList();
			return _cashgameResultFactory.Create(player, checkpoints);
		}

		private Player GetPlayer(IEnumerable<Player> players, int playerId){
			foreach(var player in players){
				if(player.Id == playerId){
					return player;
				}
			}
			return null;
		}

        public IList<string> GetLocations(Homegame homegame)
        {
			return _cashgameStorage.GetLocations(homegame.Slug);
		}

		public bool DeleteGame(Cashgame cashgame){
			return _cashgameStorage.DeleteGame(cashgame.Id);
		}

		public int AddGame(Homegame homegame, Cashgame cashgame)
		{
		    var rawCashgame = _rawCashgameFactory.Create(cashgame);
			return _cashgameStorage.AddGame(homegame.Id, rawCashgame);
		}

		public bool UpdateGame(Cashgame cashgame){
            var rawCashgame = _rawCashgameFactory.Create(cashgame);
			return _cashgameStorage.UpdateGame(rawCashgame);
		}

		public bool StartGame(Cashgame cashgame){
            var rawCashgame = _rawCashgameFactory.Create(cashgame, GameStatus.Running);
			return _cashgameStorage.UpdateGame(rawCashgame);
		}

		public bool EndGame(Cashgame cashgame){
            var rawCashgame = _rawCashgameFactory.Create(cashgame, GameStatus.Published);
			return _cashgameStorage.UpdateGame(rawCashgame);
		}

		public bool HasPlayed(Player player){
			return _cashgameStorage.HasPlayed(player.Id);
		}

	}

}