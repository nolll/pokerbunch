using Core.Entities;
using Core.Services;
using Core.UseCases;
using Moq;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AddEventTests
{
    public abstract class Arrange : UseCaseTest<AddEvent>
    {
        protected AddEvent.Result Result;

        protected Event Added;

        protected const string BunchId = BunchData.Id1;
        protected const string EventName = EventData.Name1;

        protected override void Setup()
        {
            Mock<IEventService>().Setup(o => o.Add(It.IsAny<Event>()))
                .Callback((Event @event) => Added = @event);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new AddEvent.Request(BunchId, EventName));
        }
    }
}