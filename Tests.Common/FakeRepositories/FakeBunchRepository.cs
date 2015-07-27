using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Tests.Common.Builders;

namespace Tests.Common.FakeRepositories
{
    public class FakeBunchRepository : IBunchRepository
    {
        public Bunch Added { get; private set; }
        public Bunch Saved { get; private set; }
        private IList<Bunch> _list; 

        public FakeBunchRepository()
        {
            SetupDefaultList();
        }

        public Bunch GetById(int id)
        {
            return _list.First(o => o.Id == id);
        }

        public Bunch GetBySlug(string slug)
        {
            var bunch = _list.FirstOrDefault(o => o.Slug == slug);
            if(bunch == null)
                throw new BunchNotFoundException(slug);
            return bunch;
        }

        public IList<Bunch> GetList()
        {
            return _list;
        }

        public IList<Bunch> GetByUserId(int userId)
        {
            return _list;
        }

        public int Add(Bunch bunch)
        {
            Added = bunch;
            return 1;
        }

        public bool Save(Bunch bunch)
        {
            Saved = bunch;
            return true;
        }

        public void SetupDefaultList()
        {
            _list = new List<Bunch>
            {
                CreateFirstBunch(),
                CreateSecondBunch()
            };
        }

        public void SetupOneBunchList()
        {
            _list = new List<Bunch>
            {
                CreateFirstBunch()
            };
        }

        private Bunch CreateFirstBunch()
        {
            return new Bunch(Constants.BunchIdA, Constants.SlugA, Constants.BunchNameA, Constants.DescriptionA, Constants.HouseRulesA, Constants.TimeZoneUtc, Constants.DefaultBuyinA, Currency.Default);
        }

        private Bunch CreateSecondBunch()
        {
            return new Bunch(Constants.BunchIdB, Constants.SlugB, Constants.BunchNameB, Constants.DescriptionB, Constants.HouseRulesB, Constants.TimeZoneLocal, Constants.DefaultBuyinB, Currency.Default);
        }

        public void ClearList()
        {
            _list.Clear();
        }
    }
}