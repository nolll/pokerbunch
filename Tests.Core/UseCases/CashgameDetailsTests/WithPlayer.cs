using System;
using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameDetailsTests
{
    public class WithPlayer : Arrange
    {
        [Test]
        public void AllBaseValuesAreSet()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual("2001-01-01", result.Date.IsoString);
            Assert.AreEqual(LocationName, result.LocationName);
            Assert.AreEqual(62, result.Duration.Minutes);
            Assert.AreEqual(DateTime.Parse("2001-01-01 11:00:00"), result.StartTime);
            Assert.AreEqual(DateTime.Parse("2001-01-01 12:02:00"), result.EndTime);
            Assert.IsFalse(result.CanEdit);
            Assert.AreEqual("cashgame-id", result.CashgameId);
            Assert.AreEqual(2, result.PlayerItems.Count);
        }

        [Test]
        public void PlayerResultItemsCountAndOrderIsCorrect()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(2, result.PlayerItems.Count);
            Assert.AreEqual(150, result.PlayerItems[0].Winnings.Amount);
            Assert.AreEqual(-150, result.PlayerItems[1].Winnings.Amount);
        }

        [Test]
        public void AllResultItemPropertiesAreSet()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual("player-1-name", result.PlayerItems[0].Name);
            Assert.AreEqual(Id, result.PlayerItems[0].CashgameId);
            Assert.AreEqual("player-1-id", result.PlayerItems[0].PlayerId);
            Assert.AreEqual(200, result.PlayerItems[0].Buyin.Amount);
            Assert.AreEqual(350, result.PlayerItems[0].Cashout.Amount);
            Assert.AreEqual(150, result.PlayerItems[0].Winnings.Amount);
            Assert.AreEqual(145, result.PlayerItems[0].WinRate.Amount);
        }
    }
}