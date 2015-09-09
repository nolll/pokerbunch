using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Storage.Classes;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage.Repositories
{
	public class SqlBunchRepository : IBunchRepository
	{
	    private readonly IBunchStorage _bunchStorage;

	    public SqlBunchRepository(
            IBunchStorage bunchStorage)
	    {
	        _bunchStorage = bunchStorage;
	    }

	    public IList<Bunch> Get(IList<int> ids)
	    {
	        return GetByIdsUncached(ids);
	    }

        public Bunch Get(int id)
        {
            return GetByIdUncached(id);
        }

	    public IList<int> Search()
	    {
            return GetAllIds();
	    }

	    public IList<int> Search(string slug)
	    {
            var id = GetIdBySlug(slug);
            if(id.HasValue)
                return new List<int>{id.Value};
            return new List<int>();
	    }

	    public IList<int> Search(int userId)
	    {
            return _bunchStorage.GetBunchIdsByUserId(userId);
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
            return _bunchStorage.GetIdBySlug(slug);
        }

        private IList<Bunch> GetByIdsUncached(IList<int> ids)
        {
            var rawHomegames = _bunchStorage.GetBunches(ids);
            return rawHomegames.Select(CreateBunch).ToList();
        }

        private IList<int> GetAllIds()
        {
            return _bunchStorage.GetAllIds();
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