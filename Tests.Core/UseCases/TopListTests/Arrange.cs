using Core.Entities;
using Core.Services;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.TopListTests
{
    public abstract class Arrange : UseCaseTest<TopList>
    {
        protected TopList.Result Result;

        protected override void Setup()
        {
            var bunch = BunchData.Bunch1(Role.Player);
            var cashgames = CashgameData.TwoGamesOnSameYearWithTwoPlayers;
            var players = PlayerData.TwoPlayers;

            Mock<IBunchService>().Setup(o => o.Get(BunchData.Id1)).Returns(bunch);
            Mock<ICashgameService>().Setup(o => o.List(BunchData.Id1, null)).Returns(cashgames);
            Mock<IPlayerService>().Setup(o => o.List(BunchData.Id1)).Returns(players);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new TopList.Request(BunchData.Id1, null));
        }
    }
}