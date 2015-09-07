using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Infrastructure.Storage.Cache;
using Infrastructure.Storage.Classes;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage.Repositories
{
    public class CashgameSearchCriteria
    {
        public int BunchId { get; private set; }
        public GameStatus? Status { get; private set; }
        public int? Year { get; private set; }
    }

	public class SqlCashgameRepository : ICashgameRepository
    {
	    private readonly ICashgameStorage _cashgameStorage;
	    private readonly ICacheContainer _cacheContainer;
	    private readonly ICheckpointStorage _checkpointStorage;
	    private readonly ICacheBuster _cacheBuster;

	    public SqlCashgameRepository(
            ICashgameStorage cashgameStorage,
            ICacheContainer cacheContainer,
            ICheckpointStorage checkpointStorage,
            ICacheBuster cacheBuster)
	    {
	        _cashgameStorage = cashgameStorage;
	        _cacheContainer = cacheContainer;
	        _checkpointStorage = checkpointStorage;
	        _cacheBuster = cacheBuster;
	    }

        public IList<Cashgame> Search(CashgameSearchCriteria searchCriteria)
        {
            var ids = GetIds(searchCriteria.BunchId, searchCriteria.Status, searchCriteria.Year);
            return GetList(ids);
        }

        public IList<Cashgame> GetFinished(int bunchId, int? year = null)
        {
            var ids = GetIds(bunchId, GameStatus.Finished, year);
            return GetList(ids);
        }

	    public IList<Cashgame> GetByEvent(int eventId)
	    {
            var ids = GetIdsByEvent(eventId);
            return GetList(ids);
	    } 

        public Cashgame GetRunning(int bunchId)
        {
            var id = GetIdByRunning(bunchId);
            return id.HasValue ? GetById(id.Value) : null;
        }

        public Cashgame GetByDateString(int bunchId, string dateString)
        {
            var id = GetIdByDateString(bunchId, dateString);
            if(!id.HasValue)
                throw new CashgameNotFoundException(bunchId, dateString);
            var cashgame = GetById(id.Value);
            if (cashgame == null)
                throw new CashgameNotFoundException(bunchId, dateString);
            
            return cashgame;
        }

        public Cashgame GetById(int id)
        {
            var cacheKey = CacheKeyProvider.CashgameKey(id);
            return _cacheContainer.GetAndStore(() => GetByIdUncached(id), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        private int? GetIdByDateString(int bunchId, string dateString)
        {
            var cacheKey = CacheKeyProvider.CashgameIdByDateStringKey(bunchId, dateString);
            return _cacheContainer.GetAndStore(() => _cashgameStorage.GetCashgameId(bunchId, dateString), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        private int? GetIdByRunning(int bunchId)
        {
            var cacheKey = CacheKeyProvider.CashgameIdByRunningKey(bunchId);
            return _cacheContainer.GetAndStore(() => _cashgameStorage.GetRunningCashgameId(bunchId), TimeSpan.FromMinutes(CacheTime.Long), cacheKey, true);
        }

        public IList<int> GetYears(int bunchId)
        {
            var cacheKey = CacheKeyProvider.CashgameYearsKey(bunchId);
            return _cacheContainer.GetAndStore(() => _cashgameStorage.GetYears(bunchId), TimeSpan.FromMinutes(CacheTime.Long), cacheKey, true);
        }

        private IList<Cashgame> GetList(IList<int> ids)
        {
            var cashgames = _cacheContainer.GetEachAndStore(GetListUncached, TimeSpan.FromMinutes(CacheTime.Long), ids);
            return cashgames.OrderBy(o => o.Id).ToList();
        }

        private IList<Cashgame> GetListUncached(IList<int> ids)
        {
            var rawCashgames = _cashgameStorage.GetGames(ids);
            var rawCheckpoints = _checkpointStorage.GetCheckpoints(ids);
            return CreateCashgameList(rawCashgames, rawCheckpoints);
        }

        private Cashgame GetByIdUncached(int cashgameId)
        {
            var rawGame = _cashgameStorage.GetGame(cashgameId);
            var rawCheckpoints = _checkpointStorage.GetCheckpoints(cashgameId);
            return CreateCashgame(rawGame, rawCheckpoints);
        }

        private IList<int> GetIds(int bunchId, GameStatus? status = null, int? year = null)
        {
            var cacheKey = CacheKeyProvider.CashgameIdsKey(bunchId, status, year);
            return _cacheContainer.GetAndStore(() => _cashgameStorage.GetGameIds(bunchId, (int?)status, year), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        private IList<int> GetIdsByEvent(int eventId)
        {
            var cacheKey = CacheKeyProvider.EventCashgameIdsKey(eventId);
            return _cacheContainer.GetAndStore(() => _cashgameStorage.GetGameIdsByEvent(eventId), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        public IList<string> GetLocations(int bunchId)
        {
			return _cashgameStorage.GetLocations(bunchId);
		}

		public bool DeleteGame(Cashgame cashgame){
			return _cashgameStorage.DeleteGame(cashgame.Id);
		}

		public int AddGame(Bunch bunch, Cashgame cashgame)
		{
		    var rawCashgame = CreateRawCashgame(cashgame);
            return _cashgameStorage.AddGame(bunch, rawCashgame);
		}

		public bool UpdateGame(Cashgame cashgame)
        {
            var rawCashgame = CreateRawCashgame(cashgame);
            return _cashgameStorage.UpdateGame(rawCashgame);
		}

		public bool EndGame(Bunch bunch, Cashgame cashgame)
        {
            var rawCashgame = CreateRawCashgame(cashgame, GameStatus.Finished);
            return _cashgameStorage.UpdateGame(rawCashgame);
		}

		public bool HasPlayed(int playerId)
        {
			return _cashgameStorage.HasPlayed(playerId);
		}

	    private RawCashgame CreateRawCashgame(Cashgame cashgame, GameStatus? status = null)
	    {
	        var rawStatus = status.HasValue ? (int) status.Value : (int) cashgame.Status;
	        var date = cashgame.StartTime.HasValue ? cashgame.StartTime.Value : DateTime.UtcNow;
            
            return new RawCashgame(cashgame.Id, cashgame.BunchId, cashgame.Location, rawStatus, date);
        }

	    private static Cashgame CreateCashgame(RawCashgame rawGame, IEnumerable<RawCheckpoint> rawCheckpoints)
	    {
            var checkpoints = CreateCheckpoints(rawCheckpoints);
            return new Cashgame(rawGame.BunchId, rawGame.Location, (GameStatus)rawGame.Status, rawGame.Id, checkpoints);
        }

        private static IList<Checkpoint> CreateCheckpoints(IEnumerable<RawCheckpoint> checkpoints)
	    {
            return checkpoints.Select(RawCheckpoint.CreateReal).ToList();
	    } 

	    private static IList<Cashgame> CreateCashgameList(IEnumerable<RawCashgame> rawGames, IEnumerable<RawCheckpoint> checkpoints)
        {
            var checkpointMap = GetGameCheckpointMap(checkpoints);

            var cashgames = new List<Cashgame>();
            foreach (var rawGame in rawGames)
            {
                IList<RawCheckpoint> gameCheckpoints;
                if (!checkpointMap.TryGetValue(rawGame.Id, out gameCheckpoints))
                {
                    continue;
                }
                var cashgame = CreateCashgame(rawGame, gameCheckpoints);
                cashgames.Add(cashgame);
            }
            return cashgames;
        }

        private static IDictionary<int, IList<RawCheckpoint>> GetGameCheckpointMap(IEnumerable<RawCheckpoint> checkpoints)
        {
            var checkpointMap = new Dictionary<int, IList<RawCheckpoint>>();
            foreach (var checkpoint in checkpoints)
            {
                IList<RawCheckpoint> checkpointList;
                if (!checkpointMap.TryGetValue(checkpoint.CashgameId, out checkpointList))
                {
                    checkpointList = new List<RawCheckpoint>();
                    checkpointMap.Add(checkpoint.CashgameId, checkpointList);
                }
                checkpointList.Add(checkpoint);
            }
            return checkpointMap;
        }
	}
}