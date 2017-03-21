using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;

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

        public Bunch Get(string id)
        {
            var bunch = _list.FirstOrDefault(o => o.Id == id);
            if (bunch == null)
                throw new BunchNotFoundException(id);
            return bunch;
        }

        public IList<SmallBunch> List()
        {
            return _list.Select(o => (SmallBunch)o).ToList();
        }

        public IList<SmallBunch> ListForUser()
        {
            return _list.Select(o => (SmallBunch)o).ToList();
        }

        public Bunch Add(Bunch bunch)
        {
            Added = bunch;
            return bunch;
        }

        public Bunch Update(Bunch bunch)
        {
            Saved = bunch;
            return bunch;
        }

        public void Join(string id, string code)
        {
            throw new System.NotImplementedException();
        }

        public void SetupDefaultList()
        {
            _list = new List<Bunch>
            {
                TestData.BunchA,
                TestData.BunchB
            };
        }

        public void SetupOneBunchList()
        {
            _list = new List<Bunch>
            {
                TestData.BunchA
            };
        }

        public void ClearList()
        {
            _list.Clear();
        }
    }
}