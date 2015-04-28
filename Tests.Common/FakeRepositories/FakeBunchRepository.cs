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
            return new BunchBuilder()
                .WithId(Constants.BunchIdA)
                .WithSlug(Constants.SlugA)
                .WithDisplayName(Constants.BunchNameA)
                .WithDescription(Constants.DescriptionA)
                .WithHouseRules(Constants.HouseRulesA)
                .WithDefaultBuyin(Constants.DefaultBuyinA)
                .WithUtcTimeZone()
                .Build();
        }

        private Bunch CreateSecondBunch()
        {
            return new BunchBuilder()
                .WithId(Constants.BunchIdB)
                .WithSlug(Constants.SlugB)
                .WithDisplayName(Constants.BunchNameB)
                .WithDescription(Constants.DescriptionB)
                .WithHouseRules(Constants.HouseRulesB)
                .WithDefaultBuyin(Constants.DefaultBuyinB)
                .WithLocalTimeZone()
                .Build();
        }

        public void ClearList()
        {
            _list.Clear();
        }
    }
}