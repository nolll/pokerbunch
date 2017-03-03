using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AddCashgameTests
{
    public class Arrange : UseCaseTest<AddCashgame>
    {
        protected AddCashgame.Result Result;
        protected Cashgame AddedCashgame;
        protected string CashgameIdAddedToEvent;
        protected string EventIdThatCashgameWasAddedTo;

        protected const string UserName = UserData.UserName1;
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

            var bunch = new Bunch(BunchId);
            Mock<IBunchRepository>().Setup(o => o.Get(BunchId)).Returns(bunch);

            Mock<ICashgameRepository>().Setup(o => o.Add(It.IsAny<Bunch>(), It.IsAny<Cashgame>()))
                .Returns(GeneratedCashgameId)
                .Callback((Bunch b, Cashgame cashgame) => AddedCashgame = cashgame);

            Mock<IEventRepository>().Setup(o => o.AddCashgame(It.IsAny<string>(), It.IsAny<string>()))
                .Callback((string eventId, string cashgameId) => { CashgameIdAddedToEvent = cashgameId; EventIdThatCashgameWasAddedTo = EventId; });
        }

        protected override void Execute()
        {
            Result = Sut.Execute(new AddCashgame.Request(BunchId, LocationId, EventId));
        }
    }
}