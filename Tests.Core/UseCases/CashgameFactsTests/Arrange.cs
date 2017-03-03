using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CashgameFactsTests
{
    public abstract class Arrange : UseCaseTest<CashgameFacts>
    {
        protected CashgameFacts.Result Result;

        protected override void Setup()
        {
            var bunch = BunchData.Bunch1(Role.Player);
            var cashgames = CashgameData.TwoGamesOnSameYearWithTwoPlayers;
            var players = PlayerData.TwoPlayers;

            Mock<IBunchRepository>().Setup(o => o.Get(BunchData.Id1)).Returns(bunch);
            Mock<ICashgameRepository>().Setup(o => o.List(BunchData.Id1, null)).Returns(cashgames);
            Mock<IPlayerRepository>().Setup(o => o.List(BunchData.Id1)).Returns(players);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new CashgameFacts.Request(BunchData.Id1, null));
        }
    }
}