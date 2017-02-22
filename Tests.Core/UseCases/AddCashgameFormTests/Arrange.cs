using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AddCashgameFormTests
{
    public abstract class Arrange
    {
        protected AddCashgameForm Sut;

        private const string BunchIdWithoutRunningGame = BunchData.Id1;
        protected const string BunchIdWithRunningGame = BunchData.Id2;
        protected virtual string BunchId => BunchIdWithoutRunningGame;

        [SetUp]
        public void Setup()
        {
            var bunch = new Bunch(BunchId);
            var brm = new Mock<IBunchRepository>();
            brm.Setup(o => o.Get(BunchId)).Returns(bunch);

            var cashgame = CashgameData.GameWithoutPlayers(Role.Player);
            var crm = new Mock<ICashgameRepository>();
            crm.Setup(o => o.GetCurrent(BunchIdWithRunningGame)).Returns(cashgame);

            var locations = LocationData.TwoLocations;
            var lrm = new Mock<ILocationRepository>();
            lrm.Setup(o => o.List(BunchId)).Returns(locations);

            var events = EventData.TwoEvents;
            var erm = new Mock<IEventRepository>();
            erm.Setup(o => o.ListByBunch(BunchId)).Returns(events);

            Sut = new AddCashgameForm(brm.Object, crm.Object, lrm.Object, erm.Object);
        }

        protected AddCashgameForm.Request Request => new AddCashgameForm.Request(BunchId);
    }
}