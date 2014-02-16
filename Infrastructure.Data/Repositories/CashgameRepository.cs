using System;
using System.Collections.Generic;
using Core.Classes;
using Core.Repositories;
using Infrastructure.Data.Cache;
using System.Linq;
using Infrastructure.Data.Factories.Interfaces;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Repositories
{
    public class CashgameSearchCriteria
    {
        public Homegame Homegame { get; set; }
        public GameStatus? Status { get; set; }
        public int? Year { get; set; }
    }

	public class CashgameRepository : ICashgameRepository
    {
	    private readonly ICashgameStorage _cashgameStorage;
	    private readonly ICashgameFactory _cashgameFactory;
	    private readonly IRawCashgameFactory _rawCashgameFactory;
	    private readonly ICacheContainer _cacheContainer;
	    private readonly ICheckpointStorage _checkpointStorage;
	    private readonly ICacheKeyProvider _cacheKeyProvider;
	    private readonly ICacheBuster _cacheBuster;

	    public CashgameRepository(
            ICashgameStorage cashgameStorage,
			ICashgameFactory cashgameFactory,
            IRawCashgameFactory rawCashgameFactory,
            ICacheContainer cacheContainer,
            ICheckpointStorage checkpointStorage,
            ICacheKeyProvider cacheKeyProvider,
            ICacheBuster cacheBuster)
	    {
	        _cashgameStorage = cashgameStorage;
	        _cashgameFactory = cashgameFactory;
	        _rawCashgameFactory = rawCashgameFactory;
	        _cacheContainer = cacheContainer;
	        _checkpointStorage = checkpointStorage;
	        _cacheKeyProvider = cacheKeyProvider;
	        _cacheBuster = cacheBuster;
	    }

        public IList<Cashgame> Search(CashgameSearchCriteria searchCriteria)
        {
            return GetList(searchCriteria.Homegame, searchCriteria.Status, searchCriteria.Year);
        }

        public IList<Cashgame> GetPublished(Homegame homegame, int? year = null)
        {
            return GetList(homegame, GameStatus.Published, year);
        }

		public Cashgame GetRunning(Homegame homegame)
		{
            var id = GetIdByRunning(homegame.Id);
            return id.HasValue ? GetById(id.Value) : null;
		}

        public Cashgame GetByDateString(Homegame homegame, string dateString)
        {
            var id = GetIdByDateString(homegame.Id, dateString);
            return id.HasValue ? GetById(id.Value) : null;
        }

        private Cashgame GetById(int id)
        {
            var cacheKey = _cacheKeyProvider.CashgameKey(id);
            return _cacheContainer.GetAndStore(() => GetByIdUncached(id), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        private int? GetIdByDateString(int homegameId, string dateString)
        {
            var cacheKey = _cacheKeyProvider.CashgameIdByDateStringKey(homegameId, dateString);
            return _cacheContainer.GetAndStore(() => _cashgameStorage.GetCashgameId(homegameId, dateString), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        private int? GetIdByRunning(int homegameId)
        {
            var cacheKey = _cacheKeyProvider.CashgameIdByRunningKey(homegameId);
            return _cacheContainer.GetAndStore(() => _cashgameStorage.GetRunningCashgameId(homegameId), TimeSpan.FromMinutes(CacheTime.Long), cacheKey, true);
        }

        public IList<int> GetYears(Homegame homegame)
        {
            var cacheKey = _cacheKeyProvider.CashgameYearsKey(homegame.Id);
            return _cacheContainer.GetAndStore(() => _cashgameStorage.GetYears(homegame.Id), TimeSpan.FromMinutes(CacheTime.Long), cacheKey, true);
        }

	    private IList<Cashgame> GetList(Homegame homegame, GameStatus? status = null, int? year = null)
        {
            var ids = GetIds(homegame.Id, status, year);
            var cashgames = _cacheContainer.GetEachAndStore(GetListUncached, TimeSpan.FromMinutes(CacheTime.Long), ids);
            return cashgames.OrderBy(o => o.Id).ToList();
        }

        private IList<Cashgame> GetListUncached(IList<int> ids)
        {
            var rawCashgames = _cashgameStorage.GetGames(ids);
            var rawCheckpoints = _checkpointStorage.GetCheckpoints(ids);
            return _cashgameFactory.CreateList(rawCashgames, rawCheckpoints);
        }

        private Cashgame GetByIdUncached(int cashgameId)
        {
            var rawGame = _cashgameStorage.GetGame(cashgameId);
            var rawCheckpoints = _checkpointStorage.GetCheckpoints(cashgameId);
            return _cashgameFactory.Create(rawGame, rawCheckpoints);
        }

        private IList<int> GetIds(int homegameId, GameStatus? status = null, int? year = null)
        {
            var cacheKey = _cacheKeyProvider.CashgameIdsKey(homegameId, status, year);
            return _cacheContainer.GetAndStore(() => _cashgameStorage.GetGameIds(homegameId, (int?)status, year), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
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
            var id = _cashgameStorage.AddGame(homegame, rawCashgame);
            _cacheBuster.CashgameStarted(homegame);
			return id;
		}

		public bool UpdateGame(Cashgame cashgame)
        {
            var rawCashgame = _rawCashgameFactory.Create(cashgame);
            var success = _cashgameStorage.UpdateGame(rawCashgame);
            _cacheBuster.CashgameUpdated(cashgame);
            return success;
		}

		public bool StartGame(Cashgame cashgame)
        {
            var rawCashgame = _rawCashgameFactory.Create(cashgame);
            var success = _cashgameStorage.UpdateGame(rawCashgame);
            _cacheBuster.CashgameUpdated(cashgame);
		    return success;
		}

		public bool EndGame(Homegame homegame, Cashgame cashgame)
        {
            var rawCashgame = _rawCashgameFactory.Create(cashgame, GameStatus.Published);
            var success = _cashgameStorage.UpdateGame(rawCashgame);
            _cacheBuster.CashgameEnded(homegame, cashgame);
			return success;
		}

		public bool HasPlayed(Player player)
        {
			return _cashgameStorage.HasPlayed(player.Id);
		}
	}
}