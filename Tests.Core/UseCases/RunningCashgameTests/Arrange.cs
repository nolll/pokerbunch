using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.RunningCashgameTests
{
    public abstract class Arrange : UseCaseTest<PlayerList>
    {
        protected const string BunchIdWithoutRunningGame = BunchData.Id1;
        protected const string BunchIdWithRunningGame = BunchData.Id2;
        protected virtual string BunchId => BunchIdWithRunningGame;

        [SetUp]
        public void Setup()
        {
            var bunch = new Bunch(BunchId, null, null, null, null, 100);
            var brm = new Mock<IBunchRepository>();
            brm.Setup(o => o.Get(BunchId)).Returns(bunch);

            var cashgame = CashgameData.GameWithTwoPlayers(Role.Player, true);
            var crm = new Mock<ICashgameRepository>();
            crm.Setup(o => o.GetCurrent(BunchIdWithRunningGame)).Returns(cashgame);

            var players = PlayerData.TwoPlayers;
            var player = players.First();
            var prm = new Mock<IPlayerRepository>();
            prm.Setup(o => o.List(BunchId)).Returns(players);
            prm.Setup(o => o.GetByUser(BunchId, UserData.Id1)).Returns(player);

            var user = new User(UserData.Id1, UserData.UserName1);
            var urm = new Mock<IUserRepository>();
            urm.Setup(o => o.GetByNameOrEmail(UserData.UserName1)).Returns(user);

            Sut = new RunningCashgame(brm.Object, crm.Object, prm.Object, urm.Object);
        }

        protected RunningCashgame.Request Request => new RunningCashgame.Request(UserData.UserName1, BunchId);
    }
}