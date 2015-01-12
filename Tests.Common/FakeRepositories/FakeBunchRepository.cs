using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Tests.Common.Builders;

namespace Tests.Common.FakeRepositories
{
    public class FakeBunchRepository : IBunchRepository
    {
        public Bunch Added { get; private set; }
        public Bunch Saved { get; private set; }
        private readonly IList<Bunch> _list; 

        public FakeBunchRepository()
        {
            _list = CreateList();
        }

        public Bunch GetById(int id)
        {
            return _list.First(o => o.Id == id);
        }

        public Bunch GetBySlug(string slug)
        {
            return _list.FirstOrDefault(o => o.Slug == slug);
        }

        public IList<Bunch> GetList()
        {
            return _list;
        }

        public IList<Bunch> GetByUserId(int userId)
        {
            throw new System.NotImplementedException();
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

        private IList<Bunch> CreateList()
        {
            return new List<Bunch>
            {
                new BunchBuilder()
                .WithId(Constants.BunchIdA)
                .WithSlug(Constants.SlugA)
                .WithDisplayName(Constants.BunchNameA)
                .WithDescription(Constants.DescriptionA)
                .WithHouseRules(Constants.HouseRulesA)
                .WithDefaultBuyin(Constants.DefaultBuyinA)
                .WithUtcTimeZone()
                .Build(),
                new BunchBuilder()
                .WithId(Constants.BunchIdB)
                .WithSlug(Constants.SlugB)
                .WithDisplayName(Constants.BunchNameB)
                .WithDescription(Constants.DescriptionB)
                .WithHouseRules(Constants.HouseRulesB)
                .WithDefaultBuyin(Constants.DefaultBuyinB)
                .WithLocalTimeZone()
                .Build()
            };
        }
    }
}