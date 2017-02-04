using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.EditCashgameFormTests
{
    public abstract class Arrange
    {
        protected abstract Role Role { get; }

        protected EditCashgameForm Sut;

        [SetUp]
        public void Setup()
        {
            var cashgame = CashgameData.EndedGameWithTwoPlayers(Role);
            var crm = new Mock<ICashgameRepository>();
            crm.Setup(o => o.GetDetailedById(CashgameData.Id1)).Returns(cashgame);

            var lrm = new Mock<ILocationRepository>();
            lrm.Setup(o => o.List(BunchData.Id1)).Returns(LocationData.TwoLocations);

            var erm = new Mock<IEventRepository>();
            erm.Setup(o => o.ListByBunch(BunchData.Id1)).Returns(EventData.TwoEvents);

            Sut = new EditCashgameForm(crm.Object, lrm.Object, erm.Object);
        }

        protected EditCashgameForm.Request Request => new EditCashgameForm.Request(CashgameData.Id1);
    }
}