using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CashgameFactsTests
{
    public abstract class Arrange
    {
        protected CashgameFacts Sut;

        [SetUp]
        public void Setup()
        {
            var crm = new Mock<ICashgameRepository>();
            var cashgames = CashgameData.TwoGamesOnSameYearWithTwoPlayers;
            crm.Setup(o => o.List(BunchData.Id1, null)).Returns(cashgames);

            var prm = new Mock<IPlayerRepository>();
            var players = PlayerData.TwoPlayers;
            prm.Setup(o => o.List(BunchData.Id1)).Returns(players);

            Sut = new CashgameFacts(crm.Object, prm.Object);
        }

        protected CashgameFacts.Request Request => new CashgameFacts.Request(BunchData.Id1, null);
    }
}