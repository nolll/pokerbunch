using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.MatrixTests
{
    public abstract class Arrange
    {
        protected virtual bool DifferentYears => false;

        protected Matrix Sut;

        [SetUp]
        public void Setup()
        {
            var brm = new Mock<IBunchRepository>();
            var bunch = BunchData.Bunch(Role.Player);
            brm.Setup(o => o.Get(BunchData.Id1)).Returns(bunch);

            var crm = new Mock<ICashgameRepository>();
            var cashgames = DifferentYears ?
                CashgameData.TwoGamesOnDifferentYearWithTwoPlayers :
                CashgameData.TwoGamesOnSameYearWithTwoPlayers;
            crm.Setup(o => o.List(BunchData.Id1, null)).Returns(cashgames);

            var prm = new Mock<IPlayerRepository>();
            var players = PlayerData.TwoPlayers;
            prm.Setup(o => o.List(BunchData.Id1)).Returns(players);

            var erm = new Mock<IEventRepository>();

            Sut = new Matrix(brm.Object, crm.Object, prm.Object, erm.Object);
        }

        protected Matrix.Request Request => new Matrix.Request(BunchData.Id1, null);
    }
}