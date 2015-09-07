using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Infrastructure.Storage.Cache;
using Infrastructure.Storage.Classes;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage.Repositories
{
	public class SqlBunchRepository : IBunchRepository
	{
	    private readonly IBunchStorage _bunchStorage;
	    private readonly ICacheContainer _cacheContainer;

	    public SqlBunchRepository(
            IBunchStorage bunchStorage, 
            ICacheContainer cacheContainer)
	    {
	        _bunchStorage = bunchStorage;
	        _cacheContainer = cacheContainer;
	    }

        public Bunch GetBySlug(string slug)
        {
            var id = GetIdBySlug(slug);
            if (!id.HasValue)
                throw new BunchNotFoundException(slug);
            var bunch = GetById(id.Value);
            if (bunch == null)
                throw new BunchNotFoundException(slug);
            return GetById(id.Value);
        }

        public Bunch GetById(int id)
        {
            var cacheKey = CacheKeyProvider.BunchKey(id);
            return _cacheContainer.GetAndStore(() => GetByIdUncached(id), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        public IList<Bunch> GetByUserId(int userId)
        {
            var ids = _bunchStorage.GetBunchIdsByUserId(userId);
            var bunches = _cacheContainer.GetEachAndStore(GetByIdsUncached, TimeSpan.FromMinutes(CacheTime.Long), ids);
            return bunches.OrderBy(o => o.DisplayName).ToList();
        }

        public IList<Bunch> GetList()
        {
            var ids = GetAllIds();
            var bunches = _cacheContainer.GetEachAndStore(GetByIdsUncached, TimeSpan.FromMinutes(CacheTime.Long), ids);
            return bunches.OrderBy(o => o.DisplayName).ToList();
        }

        public int Add(Bunch bunch)
        {
            var rawHomegame = RawBunch.Create(bunch);
            return _bunchStorage.AddBunch(rawHomegame);
        }

        public bool Save(Bunch bunch)
        {
            var rawHomegame = RawBunch.Create(bunch);
            return _bunchStorage.UpdateBunch(rawHomegame);
        }

        private Bunch GetByIdUncached(int id)
        {
            var rawHomegame = _bunchStorage.GetById(id);
            return rawHomegame != null ? CreateBunch(rawHomegame) : null;
        }

        private int? GetIdBySlug(string slug)
        {
            var cacheKey = CacheKeyProvider.BunchIdBySlugKey(slug);
            return _cacheContainer.GetAndStore(() => _bunchStorage.GetIdBySlug(slug), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        private IList<Bunch> GetByIdsUncached(IList<int> ids)
        {
            var rawHomegames = _bunchStorage.GetBunches(ids);
            return rawHomegames.Select(CreateBunch).ToList();
        }

        private IList<int> GetAllIds()
        {
            var cacheKey = CacheKeyProvider.BunchIdsKey();
            return _cacheContainer.GetAndStore(() => _bunchStorage.GetAllIds(), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

	    private static Bunch CreateBunch(RawBunch rawBunch)
        {
            var culture = CultureInfo.CreateSpecificCulture("sv-SE");
            var currency = new Currency(rawBunch.CurrencySymbol, rawBunch.CurrencyLayout, culture);

            return new Bunch(
                rawBunch.Id,
                rawBunch.Slug,
                rawBunch.DisplayName,
                rawBunch.Description,
                rawBunch.HouseRules,
                TimeZoneInfo.FindSystemTimeZoneById(rawBunch.TimezoneName),
                rawBunch.DefaultBuyin,
                currency);
        }
	}
}