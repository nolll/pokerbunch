using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.TopListTests
{
    public abstract class Arrange
    {
        protected TopList Sut;

        [SetUp]
        public void Setup()
        {
            var brm = new Mock<IBunchRepository>();
            brm.Setup(o => o.Get(BunchData.Id1))
                .Returns(BunchData.Bunch(Role.Player));

            var crm = new Mock<ICashgameRepository>();
            var cashgames = CashgameData.TwoGamesOnSameYearWithTwoPlayers;
            crm.Setup(o => o.List(BunchData.Id1, null)).Returns(cashgames);

            var prm = new Mock<IPlayerRepository>();
            var players = PlayerData.TwoPlayers;
            prm.Setup(o => o.List(BunchData.Id1)).Returns(players);

            Sut = new TopList(brm.Object, crm.Object, prm.Object);
        }

        protected TopList.Request Request => new TopList.Request(BunchData.Id1, null);
    }

    public class WithTwoGames : Arrange
    {
        [Test]
        public void TopList_ReturnsTopListItems()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(2, result.Items.Count);
            Assert.AreEqual(null, result.Year);
            Assert.AreEqual(BunchData.Id1, result.Slug);
        }

        [Test]
        public void TopList_ItemHasCorrectValues()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(1, result.Items[0].Rank);
            Assert.AreEqual(400, result.Items[0].Buyin.Amount);
            Assert.AreEqual(700, result.Items[0].Cashout.Amount);
            Assert.AreEqual(2, result.Items[0].GamesPlayed);
            Assert.AreEqual(124, result.Items[0].TimePlayed.Minutes);
            Assert.AreEqual(PlayerData.Name1, result.Items[0].Name);
            Assert.AreEqual(PlayerData.Id1, result.Items[0].PlayerId);
            Assert.AreEqual(300, result.Items[0].Winnings.Amount);
            Assert.AreEqual(145, result.Items[0].WinRate.Amount);
        }

        [Test]
        public void TopList_HighestWinningsIsFirst()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(300, result.Items[0].Winnings.Amount);
            Assert.AreEqual(-300, result.Items[1].Winnings.Amount);
        }
    }
}