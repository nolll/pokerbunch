using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.RunningCashgameTests
{
    public abstract class Arrange : UseCaseTest<RunningCashgame>
    {
        protected const string BunchIdWithoutRunningGame = BunchData.Id1;
        protected const string BunchIdWithRunningGame = BunchData.Id2;
        protected virtual string BunchId => BunchIdWithRunningGame;

        [SetUp]
        public void Setup()
        {
            var bunch = new Bunch(BunchId, null, null, null, null, 100);
            var cashgame = CashgameData.GameWithTwoPlayers(Role.Player, true);
            var players = PlayerData.TwoPlayers;
            var player = players.First();
            var user = new User(UserData.Id1, UserData.UserName1);

            Mock<IBunchRepository>().Setup(o => o.Get(BunchId)).Returns(bunch);
            Mock<ICashgameRepository>().Setup(o => o.GetCurrent(BunchIdWithRunningGame)).Returns(cashgame);
            Mock<IPlayerRepository>().Setup(o => o.List(BunchId)).Returns(players);
            Mock<IPlayerRepository>().Setup(o => o.GetByUser(BunchId, UserData.Id1)).Returns(player);
            Mock<IUserRepository>().Setup(o => o.GetByNameOrEmail(UserData.UserName1)).Returns(user);
        }

        protected RunningCashgame.Result Execute()
        {
            return Sut.Execute(new RunningCashgame.Request(UserData.UserName1, BunchId));
        }
    }
}