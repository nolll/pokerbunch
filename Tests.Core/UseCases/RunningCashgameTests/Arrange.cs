using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.RunningCashgameTests
{
    public abstract class Arrange : UseCaseTest<RunningCashgame>
    {
        protected RunningCashgame.Result Result;

        protected const string BunchIdWithoutRunningGame = BunchData.Id1;
        protected const string BunchIdWithRunningGame = BunchData.Id2;
        protected virtual string BunchId => BunchIdWithRunningGame;

        protected override void Setup()
        {
            var bunch = new Bunch(BunchId, null, null, null, null, 100, null, Role.None, PlayerData.Id1);
            var cashgame = CashgameData.GameWithTwoPlayers(Role.Player, true);
            var players = PlayerData.TwoPlayers;

            Mock<IBunchRepository>().Setup(o => o.Get(BunchId)).Returns(bunch);
            Mock<ICashgameRepository>().Setup(o => o.GetCurrent(BunchIdWithRunningGame)).Returns(cashgame);
            Mock<IPlayerRepository>().Setup(o => o.List(BunchId)).Returns(players);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new RunningCashgame.Request(UserData.UserName1, BunchId));
        }
    }
}