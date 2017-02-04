using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CashgameDetailsTests
{
    public abstract class Arrange
    {
        protected CashgameDetails Sut;

        protected abstract Role Role { get; }

        [SetUp]
        public void Setup()
        {
            var crm = new Mock<ICashgameRepository>();
            var cashgame = CashgameData.EndedGameWithTwoPlayers(Role);
            crm.Setup(o => o.GetDetailedById(CashgameData.Id1)).Returns(cashgame);

            Sut = new CashgameDetails(crm.Object);
        }

        protected CashgameDetails.Request Request => new CashgameDetails.Request(CashgameData.Id1);
    }
}