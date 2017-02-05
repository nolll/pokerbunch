using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.EditCashgameTests
{
    public abstract class Arrange
    {
        protected string SavedLocationId;
        protected string SavedEventId;

        private const string ValidLocationId = LocationData.Id1;
        private const string ValidEventId = EventData.Id1;

        private const string InvalidLocationId = "";
        private const string InvalidEventId = "";

        protected EditCashgame Sut;

        [SetUp]
        public void Setup()
        {
            var crm = new Mock<ICashgameRepository>();

            var cashgame = CashgameData.EndedGameWithTwoPlayers(Role.Manager);
            crm.Setup(o => o.Update(CashgameData.Id1, ValidLocationId, ValidEventId))
                .Returns(cashgame)
                .Callback((string cashgameId, string locationId, string eventId) => { SavedLocationId = locationId; SavedEventId = eventId; });
            crm.Setup(o => o.Update(CashgameData.Id1, InvalidLocationId, InvalidEventId))
                .Throws(new ValidationException("validation-error"));


            Sut = new EditCashgame(crm.Object);
        }

        protected EditCashgame.Request ValidRequest => new EditCashgame.Request(CashgameData.Id1, ValidLocationId, ValidEventId);
        protected EditCashgame.Request InvalidRequest => new EditCashgame.Request(CashgameData.Id1, InvalidLocationId, InvalidEventId);
    }
}
