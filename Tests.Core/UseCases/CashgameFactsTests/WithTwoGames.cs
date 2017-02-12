using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CashgameFactsTests
{
    public abstract class Arrange
    {
        protected CashgameFacts Sut;

        [SetUp]
        public void Setup()
        {
            var brm = new Mock<IBunchRepository>();
            var bunch = BunchData.Bunch(Role.Player);
            brm.Setup(o => o.Get(BunchData.Id1)).Returns(bunch);

            var crm = new Mock<ICashgameRepository>();
            var cashgames = CashgameData.TwoGamesWithTwoPlayers();
            crm.Setup(o => o.List(BunchData.Id1, null)).Returns(cashgames);

            var prm = new Mock<IPlayerRepository>();
            var players = PlayerData.TwoPlayers;
            prm.Setup(o => o.List(BunchData.Id1)).Returns(players);

            Sut = new CashgameFacts(brm.Object, crm.Object, prm.Object);
        }

        protected CashgameFacts.Request Request => new CashgameFacts.Request(BunchData.Id1, null);
    }

    public class WithTwoGames : Arrange
    {
        [Test]
        public void GetFactsResult_AllPropertiesAreSet()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(2, result.GameCount);
            Assert.AreEqual(124, result.TotalTimePlayed.Minutes);
            Assert.AreEqual(800, result.Turnover.Amount);
            Assert.AreEqual(-150, result.WorstResult.Amount.Amount);
            Assert.AreEqual(+150, result.BestResult.Amount.Amount);
            Assert.AreEqual(-300, result.WorstTotalResult.Amount.Amount);
            Assert.AreEqual(300, result.BestTotalResult.Amount.Amount);
            Assert.AreEqual(400, result.BiggestBuyin.Amount.Amount);
            Assert.AreEqual(700, result.BiggestCashout.Amount.Amount);
            Assert.AreEqual(124, result.MostTimePlayed.Time.Minutes);
        }
    }
}
