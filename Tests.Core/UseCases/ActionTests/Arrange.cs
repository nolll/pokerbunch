using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.ActionTests
{
    public abstract class Arrange
    {
        protected Actions Sut;

        protected abstract Role Role { get; }

        [SetUp]
        public void Setup()
        {
            var crm = new Mock<ICashgameRepository>();
            var cashgame = CashgameData.EndedGameWithTwoPlayers(Role);
            crm.Setup(o => o.GetDetailedById(CashgameData.Id1)).Returns(cashgame);

            Sut = new Actions(crm.Object);
        }

        protected Actions.Request Request => new Actions.Request(CashgameData.Id1, PlayerData.Id1);
    }
}
