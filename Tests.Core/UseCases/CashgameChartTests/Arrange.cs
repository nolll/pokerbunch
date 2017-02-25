using Core.Repositories;
using Core.UseCases;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CashgameChartTests
{
    public abstract class Arrange : UseCaseTest<CashgameChart>
    {
        protected CashgameChart.Result Result;

        [SetUp]
        public void Setup()
        {
            var cashgames = CashgameData.TwoGamesOnSameYearWithTwoPlayers;
            var players = PlayerData.TwoPlayers;

            Mock<ICashgameRepository>().Setup(o => o.List(BunchData.Id1, null)).Returns(cashgames);
            Mock<IPlayerRepository>().Setup(o => o.List(BunchData.Id1)).Returns(players);

            Result = Sut.Execute(new CashgameChart.Request(BunchData.Id1, null));
        }
    }
}