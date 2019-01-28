using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;

namespace Infrastructure.Api.FakeServices
{
    public class FakeBunchService : IBunchService
    {
        public Bunch Get(string id)
        {
            return FakeData.Bunches.FirstOrDefault(o => o.Id == id);
        }

        public IList<SmallBunch> List()
        {
            return FakeData.Bunches.Select(o => new SmallBunch(o.Id, o.DisplayName, o.Description)).ToList();
        }

        public Bunch Add(Bunch bunch)
        {
            FakeData.Bunches.Add(bunch);
            return bunch;
        }

        public Bunch Update(Bunch bunch)
        {
            var index = FakeData.Bunches.FindIndex(o => o.Id == bunch.Id);
            FakeData.Bunches[index] = bunch;
            return bunch;
        }

        public void Join(string bunchId, string code)
        {
            throw new NotImplementedException();
        }
    }
}