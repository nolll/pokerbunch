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
using Infrastructure.System;

namespace Infrastructure.Repositories {

    public class CashgameSearchCriteria
    {
        public Homegame Homegame { get; set; }
        public GameStatus? Status { get; set; }
        public int? Year { get; set; }
    }

	public class CashgameRepository : ICashgameRepository{

        private const string CashgameCacheKey = "Cashgame";
        private const string CashgameIdCacheKey = "CashgameId";
        private const string CashgameIdsCacheKey = "CashgameIds";
        private const string CashgameYearsCacheKey = "CashgameYears";

	    private readonly ICashgameStorage _cashgameStorage;
	    private readonly ICashgameFactory _cashgameFactory;
	    private readonly IPlayerRepository _playerRepository;
	    private readonly ICashgameResultFactory _cashgameResultFactory;
	    private readonly IRawCashgameFactory _rawCashgameFactory;
	    private readonly ICheckpointFactory _checkpointFactory;
	    private readonly ICacheContainer _cacheContainer;
	    private readonly ICheckpointStorage _checkpointStorage;
	    private readonly ITimeProvider _timeProvider;

	    public CashgameRepository(
            ICashgameStorage cashgameStorage,
			ICashgameFactory cashgameFactory,
            IPlayerRepository playerRepository,
			ICashgameResultFactory cashgameResultFactory,
            IRawCashgameFactory rawCashgameFactory,
            ICheckpointFactory checkpointFactory,
            ICacheContainer cacheContainer,
            ICheckpointStorage checkpointStorage,
            ITimeProvider timeProvider)
	    {
	        _cashgameStorage = cashgameStorage;
	        _cashgameFactory = cashgameFactory;
	        _playerRepository = playerRepository;
	        _cashgameResultFactory = cashgameResultFactory;
	        _rawCashgameFactory = rawCashgameFactory;
	        _checkpointFactory = checkpointFactory;
	        _cacheContainer = cacheContainer;
	        _checkpointStorage = checkpointStorage;
	        _timeProvider = timeProvider;
	    }

        public IList<Cashgame> Search(CashgameSearchCriteria searchCriteria)
        {
            return GetGames(searchCriteria.Homegame, searchCriteria.Status, searchCriteria.Year);
        }

        public IList<Cashgame> GetPublished(Homegame homegame, int? year = null)
        {
            return GetGames(homegame, GameStatus.Published, year);
        }

		public Cashgame GetRunning(Homegame homegame)
		{
		    var id = _cashgameStorage.GetRunningCashgameId(homegame.Id);
            if (!id.HasValue)
            {
                return null;
            }
		    return GetCashgameUncached(id.Value);
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
            return GetCashgameById(cashgameId.Value);
        }

        private Cashgame GetCashgameById(int id)
        {
            var cacheKey = _cacheContainer.ConstructCacheKey(CashgameCacheKey, id);
            var cached = _cacheContainer.Get<Cashgame>(cacheKey);
            if (cached != null)
            {
                return cached;
            }
            var uncached = GetCashgameUncached(id);
            if (uncached != null)
            {
                _cacheContainer.FakeInsert(cacheKey, uncached, TimeSpan.FromMinutes(CacheTime.Long));
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
                _cacheContainer.FakeInsert(cacheKey, uncached.Value.ToString(CultureInfo.InvariantCulture), TimeSpan.FromMinutes(CacheTime.Long));
            }
            return uncached;
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
                _cacheContainer.FakeInsert(cacheKey, uncached, TimeSpan.FromMinutes(CacheTime.Long));
            }
            return uncached;
        }

        private IList<Cashgame> GetGames(Homegame homegame, GameStatus? status = null, int? year = null)
        {
            var cashgames = new List<Cashgame>();
            var ids = GetGameIds(homegame, status, year);
            var uncachedIds = new List<int>();
            foreach (var id in ids)
            {
                var cacheKey = _cacheContainer.ConstructCacheKey(CashgameCacheKey, id);
                var cached = _cacheContainer.Get<Cashgame>(cacheKey);
                if (cached != null)
                {
                    cashgames.Add(cached);
                }
                else
                {
                    uncachedIds.Add(id);
                }
            }

            if (uncachedIds.Count > 0)
            {
                var rawCashgames = _cashgameStorage.GetGames(uncachedIds);
                var newCashgames = GetGamesFromRawGames(rawCashgames);
                foreach (var cashgame in newCashgames)
                {
                    _cacheContainer.FakeInsert(_cacheContainer.ConstructCacheKey(CashgameCacheKey, cashgame.Id), cashgame, TimeSpan.FromMinutes(CacheTime.Long));
                }
                cashgames.AddRange(newCashgames);
            }

            return cashgames.OrderBy(o => o.Id).ToList();
        }

        private Cashgame GetCashgameUncached(int cashgameId)
        {
            var rawGame = _cashgameStorage.GetGame(cashgameId);
            var rawCheckpoints = _checkpointStorage.GetCheckpoints(cashgameId);
            return GetGameFromRawGame(rawGame, rawCheckpoints);
        }

        private IEnumerable<int> GetGameIds(Homegame homegame, GameStatus? status = null, int? year = null)
        {
            var cacheKey = _cacheContainer.ConstructCacheKey(CashgameIdsCacheKey, status, year);
            var cached = _cacheContainer.Get<List<int>>(cacheKey);
            if (cached != null)
            {
                return cached;
            }
            var uncached = _cashgameStorage.GetGameIds(homegame.Id, (int?)status, year);
            if (uncached != null)
            {
                _cacheContainer.FakeInsert(cacheKey, uncached, TimeSpan.FromMinutes(CacheTime.Long));
            }
            return uncached;
        }

        private IList<Cashgame> GetGamesFromRawGames(IEnumerable<RawCashgameWithResults> rawGames)
        {
            return rawGames.Select(GetGameFromRawGame).ToList();
        }

	    private Cashgame GetGameFromRawGame(RawCashgameWithResults rawGame)
        {
            var results = new List<CashgameResult>();
            var rawResults = rawGame.Results;
            foreach (var rawResult in rawResults)
            {
                results.Add(GetResultFromRawResult(rawResult));
            }
            return _cashgameFactory.Create(rawGame.Location, rawGame.Status, rawGame.Id, results);
        }

        private Cashgame GetGameFromRawGame(RawCashgame rawGame, IEnumerable<RawCheckpoint> rawCheckpoints)
        {
            var results = new List<CashgameResult>();
            var rawResults = GetRawResults(rawCheckpoints);
            foreach (var rawResult in rawResults)
            {
                results.Add(GetResultFromRawResult(rawResult));
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

		private CashgameResult GetResultFromRawResult(RawCashgameResult rawResult){
			var checkpoints = rawResult.Checkpoints.Select(o => _checkpointFactory.Create(o)).ToList();
            return _cashgameResultFactory.Create(rawResult.PlayerId, checkpoints);
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
			return _cashgameStorage.AddGame(homegame, rawCashgame);
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

	    public void ClearCashgameFromCache(Cashgame cashgame)
        {
            var cacheKey = _cacheContainer.ConstructCacheKey(CashgameCacheKey, cashgame.Id);
            _cacheContainer.FakeRemove(cacheKey);
        }

	    public void ClearCashgameListFromCache(Homegame homegame, Cashgame cashgame)
        {
            var allTimeCacheKey = _cacheContainer.ConstructCacheKey(CashgameIdsCacheKey, homegame.Id);
            _cacheContainer.FakeRemove(allTimeCacheKey);
	        var yearToClear = cashgame.StartTime.HasValue ? cashgame.StartTime.Value.Year : _timeProvider.GetTime().Year;
            var currentYearCacheKey = _cacheContainer.ConstructCacheKey(CashgameIdsCacheKey, homegame.Id, yearToClear);
            _cacheContainer.FakeRemove(currentYearCacheKey);
        }

	}
}