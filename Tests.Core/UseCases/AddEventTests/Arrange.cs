using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AddEventTests
{
    public class Arrange : UseCaseTest<AddEvent>
    {
        protected AddEvent.Result Result;

        protected Event Added;

        protected string BunchId = BunchData.Id1;
        protected string EventName = EventData.Name1;

        protected override void Setup()
        {
            Mock<IEventRepository>().Setup(o => o.Add(It.IsAny<Event>()))
                .Callback((Event @event) => Added = @event);
        }

        protected override void Execute()
        {
            Result = Sut.Execute(new AddEvent.Request(BunchId, EventName));
        }
    }
}