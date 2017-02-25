using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.MatrixTests
{
    public abstract class Arrange : UseCaseTest<BunchMatrix>
    {
        protected Matrix.Result Result;

        protected virtual bool DifferentYears => false;

        [SetUp]
        public void Setup()
        {
            var bunch = BunchData.Bunch1(Role.Player);
            var cashgames = DifferentYears ? CashgameData.TwoGamesOnDifferentYearWithTwoPlayers : CashgameData.TwoGamesOnSameYearWithTwoPlayers;
            var players = PlayerData.TwoPlayers;

            Mock<IBunchRepository>().Setup(o => o.Get(BunchData.Id1)).Returns(bunch);
            Mock<ICashgameRepository>().Setup(o => o.List(BunchData.Id1, null)).Returns(cashgames);
            Mock<IPlayerRepository>().Setup(o => o.List(BunchData.Id1)).Returns(players);

            Result = Sut.Execute(new BunchMatrix.Request(BunchData.Id1, null));
        }
    }
}