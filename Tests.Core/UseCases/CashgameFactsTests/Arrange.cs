using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CashgameFactsTests
{
    public abstract class Arrange : UseCaseTest<CashgameFacts>
    {
        protected CashgameFacts.Result Result;

        [SetUp]
        public void Setup()
        {
            var bunch = BunchData.Bunch1(Role.Player);
            var cashgames = CashgameData.TwoGamesOnSameYearWithTwoPlayers;
            var players = PlayerData.TwoPlayers;

            Mock<IBunchRepository>().Setup(o => o.Get(BunchData.Id1)).Returns(bunch);
            Mock<ICashgameRepository>().Setup(o => o.List(BunchData.Id1, null)).Returns(cashgames);
            Mock<IPlayerRepository>().Setup(o => o.List(BunchData.Id1)).Returns(players);

            Result = Sut.Execute(new CashgameFacts.Request(BunchData.Id1, null));
        }
    }
}