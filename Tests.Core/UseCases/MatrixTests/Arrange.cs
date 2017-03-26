using Core.Entities;
using Core.Services;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.MatrixTests
{
    public abstract class Arrange : UseCaseTest<BunchMatrix>
    {
        protected Matrix.Result Result;

        protected virtual bool DifferentYears => false;

        protected override void Setup()
        {
            var bunch = BunchData.Bunch1(Role.Player);
            var cashgames = DifferentYears ? CashgameData.TwoGamesOnDifferentYearWithTwoPlayers : CashgameData.TwoGamesOnSameYearWithTwoPlayers;
            var players = PlayerData.TwoPlayers;

            Mock<IBunchService>().Setup(o => o.Get(BunchData.Id1)).Returns(bunch);
            Mock<ICashgameService>().Setup(o => o.List(BunchData.Id1, null)).Returns(cashgames);
            Mock<IPlayerService>().Setup(o => o.List(BunchData.Id1)).Returns(players);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new BunchMatrix.Request(BunchData.Id1, null));
        }
    }
}