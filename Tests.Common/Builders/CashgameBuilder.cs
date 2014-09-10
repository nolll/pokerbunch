using System;
using Core.Entities;
using Tests.Common.FakeClasses;

namespace Tests.Common.Builders
{
    public class CashgameBuilder
    {
        private int _id;
        private string _location;
        private string _dateString;
        private DateTime _startTime;
        private DateTime _endTime;

        public CashgameBuilder()
        {
            _id = 1;
            _location = "Location";
            _dateString = "2001-01-01";
            _startTime = new DateTime(2001, 1, 1, 1, 1, 1);
            _endTime = new DateTime(2001, 1, 1, 1, 2, 1);
        }

        public Cashgame Build()
        {
            return new CashgameInTest(_id, location: _location, dateString: _dateString, startTime: _startTime, endTime: _endTime);
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

        public CashgameBuilder WithDateString(string dateString)
        {
            _dateString = dateString;
            return this;
        }
    }
}