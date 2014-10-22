using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Infrastructure.Cache;
using Infrastructure.SqlServer.Factories.Interfaces;
using Infrastructure.SqlServer.Interfaces;
using Infrastructure.SqlServer.Mappers;

namespace Infrastructure.SqlServer.Repositories
{
    public class CashgameSearchCriteria
    {
        public Bunch Bunch { get; private set; }
        public GameStatus? Status { get; private set; }
        public int? Year { get; private set; }
    }

	public class CashgameRepository : ICashgameRepository
    {
	    private readonly ICashgameStorage _cashgameStorage;
	    private readonly IRawCashgameFactory _rawCashgameFactory;
	    private readonly ICacheContainer _cacheContainer;
	    private readonly ICheckpointStorage _checkpointStorage;
	    private readonly ICacheBuster _cacheBuster;

	    public CashgameRepository(
            ICashgameStorage cashgameStorage,
            IRawCashgameFactory rawCashgameFactory,
            ICacheContainer cacheContainer,
            ICheckpointStorage checkpointStorage,
            ICacheBuster cacheBuster)
	    {
	        _cashgameStorage = cashgameStorage;
	        _rawCashgameFactory = rawCashgameFactory;
	        _cacheContainer = cacheContainer;
	        _checkpointStorage = checkpointStorage;
	        _cacheBuster = cacheBuster;
	    }

        public IList<Cashgame> Search(CashgameSearchCriteria searchCriteria)
        {
            var ids = GetIds(searchCriteria.Bunch.Id, searchCriteria.Status, searchCriteria.Year);
            return GetList(ids);
        }

        public IList<Cashgame> GetPublished(Bunch bunch, int? year = null)
        {
            var ids = GetIds(bunch.Id, GameStatus.Finished, year);
            return GetList(ids);
        }

	    public IList<Cashgame> GetByEvent(int eventId)
	    {
            var ids = GetIds(eventId);
            return GetList(ids);
	    } 

        public Cashgame GetRunning(Bunch bunch)
        {
            return GetRunning(bunch.Id);
        }

        public Cashgame GetRunning(int bunchId)
        {
            var id = GetIdByRunning(bunchId);
            return id.HasValue ? GetById(id.Value) : null;
        }

        public Cashgame GetByDateString(Bunch bunch, string dateString)
        {
            var id = GetIdByDateString(bunch.Id, dateString);
            if(!id.HasValue)
                throw new CashgameNotFoundException(bunch.Slug, dateString);
            var cashgame = GetById(id.Value);
            if (cashgame == null)
                throw new CashgameNotFoundException(bunch.Slug, dateString);
            
            return cashgame;
        }

        private Cashgame GetById(int id)
        {
            var cacheKey = CacheKeyProvider.CashgameKey(id);
            return _cacheContainer.GetAndStore(() => GetByIdUncached(id), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        private int? GetIdByDateString(int homegameId, string dateString)
        {
            var cacheKey = CacheKeyProvider.CashgameIdByDateStringKey(homegameId, dateString);
            return _cacheContainer.GetAndStore(() => _cashgameStorage.GetCashgameId(homegameId, dateString), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        private int? GetIdByRunning(int homegameId)
        {
            var cacheKey = CacheKeyProvider.CashgameIdByRunningKey(homegameId);
            return _cacheContainer.GetAndStore(() => _cashgameStorage.GetRunningCashgameId(homegameId), TimeSpan.FromMinutes(CacheTime.Long), cacheKey, true);
        }

        public IList<int> GetYears(Bunch bunch)
        {
            return GetYears(bunch.Id);
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
            return CashgameDataMapper.MapList(rawCashgames, rawCheckpoints);
        }

        private Cashgame GetByIdUncached(int cashgameId)
        {
            var rawGame = _cashgameStorage.GetGame(cashgameId);
            var rawCheckpoints = _checkpointStorage.GetCheckpoints(cashgameId);
            return CashgameDataMapper.Map(rawGame, rawCheckpoints);
        }

        private IList<int> GetIds(int homegameId, GameStatus? status = null, int? year = null)
        {
            var cacheKey = CacheKeyProvider.CashgameIdsKey(homegameId, status, year);
            return _cacheContainer.GetAndStore(() => _cashgameStorage.GetGameIds(homegameId, (int?)status, year), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        private IList<int> GetIdsByEvent(int eventId)
        {
            var cacheKey = CacheKeyProvider.EventCashgameIdsKey(eventId);
            return _cacheContainer.GetAndStore(() => _cashgameStorage.GetGameIdsByEvent(eventId), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        public IList<string> GetLocations(Bunch bunch)
        {
			return _cashgameStorage.GetLocations(bunch.Slug);
		}

		public bool DeleteGame(Cashgame cashgame){
			return _cashgameStorage.DeleteGame(cashgame.Id);
		}

		public int AddGame(Bunch bunch, Cashgame cashgame)
		{
		    var rawCashgame = _rawCashgameFactory.Create(cashgame);
            var id = _cashgameStorage.AddGame(bunch, rawCashgame);
            _cacheBuster.CashgameStarted(bunch);
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

		public bool EndGame(Bunch bunch, Cashgame cashgame)
        {
            var rawCashgame = _rawCashgameFactory.Create(cashgame, GameStatus.Finished);
            var success = _cashgameStorage.UpdateGame(rawCashgame);
            _cacheBuster.CashgameUpdated(cashgame);
			return success;
		}

		public bool HasPlayed(int playerId)
        {
			return _cashgameStorage.HasPlayed(playerId);
		}
	}
}