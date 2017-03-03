using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.EditCashgameTests
{
    public abstract class Arrange : UseCaseTest<EditCashgame>
    {
        protected string SavedLocationId;
        protected string SavedEventId;

        private const string ValidLocationId = LocationData.Id1;
        private const string ValidEventId = EventData.Id1;
        protected const string InvalidLocationId = "";
        protected const string InvalidEventId = "";

        protected virtual string LocationId => ValidLocationId;
        protected virtual string EventId => ValidEventId;

        protected override void Setup()
        {
            SavedLocationId = null;
            SavedEventId = null;

            var cashgame = CashgameData.GameWithTwoPlayers(Role.Manager);

            Mock<ICashgameRepository>().Setup(o => o.Update(CashgameData.Id1, ValidLocationId, ValidEventId)).Returns(cashgame)
                .Callback((string cashgameId, string locationId, string eventId) => { SavedLocationId = locationId; SavedEventId = eventId; });
            Mock<ICashgameRepository>().Setup(o => o.Update(CashgameData.Id1, ValidLocationId, InvalidEventId))
                .Throws(new ValidationException("validation-error"));
            Mock<ICashgameRepository>().Setup(o => o.Update(CashgameData.Id1, InvalidLocationId, ValidEventId))
                .Throws(new ValidationException("validation-error"));
        }

        protected override void Execute()
        {
            Subject.Execute(new EditCashgame.Request(CashgameData.Id1, LocationId, EventId));
        }
    }
}
