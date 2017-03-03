using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.EventListTests
{
    public abstract class Arrange : UseCaseTest<EventList>
    {
        protected EventList.Result Result;

        private string BunchId = BunchData.Id1;
        private readonly Date _startDate1 = new Date(2001, 1, 1);
        private readonly Date _endDate1 = new Date(2001, 1, 2);
        private readonly Date _startDate2 = new Date(2001, 2, 1);
        private readonly Date _endDate2 = new Date(2001, 2, 2);

        protected override void Setup()
        {
            var location = new SmallLocation(LocationData.Id1, LocationData.Name1);
            var events = new List<Event>
            {
                new Event(EventData.Id1, BunchData.Id1, EventData.Name1, location, _startDate1, _endDate1),
                new Event(EventData.Id2, BunchData.Id1, EventData.Name2, location, _startDate2, _endDate2)
            };

            Mock<IEventRepository>().Setup(o => o.ListByBunch(BunchId)).Returns(events);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new EventList.Request(BunchData.Id1));
        }
    }
}