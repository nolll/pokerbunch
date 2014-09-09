using Core.Entities;
using Tests.Common.FakeClasses;

namespace Tests.Common.Builders
{
    public class CashgameBuilder
    {
        private int _id;
        private string _location;

        public CashgameBuilder()
        {
            _id = 1;
            _location = "Location";
        }

        public Cashgame Build()
        {
            return new CashgameInTest(_id, location: _location);
        }

        public CashgameBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public CashgameBuilder WithLocation(string location)
        {
            _location = location;
            return this;
        }
    }
}