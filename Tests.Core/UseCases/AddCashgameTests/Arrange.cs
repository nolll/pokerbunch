using Core.Entities;
using Core.Services;
using Core.UseCases;
using Moq;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AddCashgameTests
{
    public abstract class Arrange : UseCaseTest<AddCashgame>
    {
        protected AddCashgame.Result Result;
        protected string PostedLocationId;
        protected string CashgameIdAddedToEvent;
        protected string EventIdThatCashgameWasAddedTo;

        protected const string BunchId = BunchData.Id1;
        protected const string LocationId = LocationData.Id1;
        protected const string ExistingEventId = EventData.Id1;
        protected const string EmptyEventId = null;
        protected string GeneratedCashgameId = "cashgame-id";

        protected virtual string EventId => EmptyEventId;

        protected override void Setup()
        {
            CashgameIdAddedToEvent = null;
            EventIdThatCashgameWasAddedTo = null;

            Mock<ICashgameService>().Setup(o => o.Add(BunchId, It.IsAny<string>()))
                .Returns(GeneratedCashgameId)
                .Callback((string bunchId, string locationId) => PostedLocationId = locationId);

            Mock<IEventService>().Setup(o => o.AddCashgame(It.IsAny<string>(), It.IsAny<string>()))
                .Callback((string eventId, string cashgameId) => { CashgameIdAddedToEvent = cashgameId; EventIdThatCashgameWasAddedTo = EventId; });
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new AddCashgame.Request(BunchId, LocationId, EventId));
        }
    }
}