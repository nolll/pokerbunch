using Core.Services;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CashgameChartTests
{
    public abstract class Arrange : UseCaseTest<CashgameChart>
    {
        protected CashgameChart.Result Result;

        protected override void Setup()
        {
            var cashgames = CashgameData.TwoGamesOnSameYearWithTwoPlayers;
            var players = PlayerData.TwoPlayers;

            Mock<ICashgameService>().Setup(o => o.List(BunchData.Id1, null)).Returns(cashgames);
            Mock<IPlayerService>().Setup(o => o.List(BunchData.Id1)).Returns(players);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new CashgameChart.Request(BunchData.Id1, null));
        }
    }
}