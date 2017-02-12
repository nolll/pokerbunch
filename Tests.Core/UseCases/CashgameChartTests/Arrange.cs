using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CashgameChartTests
{
    public abstract class Arrange
    {
        protected CashgameChart Sut;

        [SetUp]
        public void Setup()
        {
            var crm = new Mock<ICashgameRepository>();
            var cashgames = CashgameData.TwoGamesWithTwoPlayers();
            crm.Setup(o => o.List(BunchData.Id1, null)).Returns(cashgames);

            var prm = new Mock<IPlayerRepository>();
            var players = PlayerData.TwoPlayers;
            prm.Setup(o => o.List(BunchData.Id1)).Returns(players);

            Sut = new CashgameChart(crm.Object, prm.Object);
        }

        protected CashgameChart.Request Request => new CashgameChart.Request(BunchData.Id1, null);
    }
}