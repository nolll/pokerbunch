using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.EventDetailsTests
{
    public abstract class Arrange : UseCaseTest<EventDetails>
    {
        protected EventDetails.Result Result;

        private string EventId = EventData.Id1;

        protected override void Setup()
        {
            var @event = new Event(EventId, BunchData.Id1, EventData.Name1);

            Mock<IEventRepository>().Setup(o => o.Get(EventId)).Returns(@event);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new EventDetails.Request(EventId));
        }
    }
}