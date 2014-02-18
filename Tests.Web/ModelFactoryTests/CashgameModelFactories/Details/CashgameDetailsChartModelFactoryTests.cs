using System.Collections.Generic;
using Application.Services;
using Core.Classes;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Details;
using Web.ModelFactories.ChartModelFactories;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Details
{
    public class CashgameDetailsChartModelFactoryTests : MockContainer
    {
        [Test]
        public void Create()
        {
            const int playerId1 = 1;
            const int playerId2 = 2;

            var homegame = new FakeHomegame();
            var cashgame = new FakeCashgame();
            var player1 = new FakePlayer(id: playerId1);
            var player2 = new FakePlayer(id: playerId2);
            var playerList = new List<Player> {player1, player2};

            GetMock<ICashgameService>().Setup(o => o.GetPlayers(cashgame)).Returns(playerList);

            var sut = GetSut();
            var result = sut.Create(homegame, cashgame);

            Assert.NotNull(result);
        }

        private CashgameDetailsChartModelFactory GetSut()
        {
            return new CashgameDetailsChartModelFactory(
                GetMock<ITimeProvider>().Object,
                GetMock<IChartValueModelFactory>().Object,
                GetMock<ICashgameService>().Object);
        }
    }
}
