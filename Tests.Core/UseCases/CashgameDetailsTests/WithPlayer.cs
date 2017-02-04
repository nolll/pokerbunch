using System;
using Core.Entities;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CashgameDetailsTests
{
    public class WithPlayer : Arrange
    {
        protected override Role Role => Role.Player;

        [Test]
        public void AllBaseValuesAreSet()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual("2001-01-01", result.Date.IsoString);
            Assert.AreEqual(LocationData.Name1, result.LocationName);
            Assert.AreEqual(62, result.Duration.Minutes);
            Assert.AreEqual(DateTime.Parse("2001-01-01 11:00:00"), result.StartTime);
            Assert.AreEqual(DateTime.Parse("2001-01-01 12:02:00"), result.EndTime);
            Assert.IsFalse(result.CanEdit);
            Assert.AreEqual(CashgameData.Id1, result.CashgameId);
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

            Assert.AreEqual(PlayerData.Name1, result.PlayerItems[0].Name);
            Assert.AreEqual(CashgameData.Id1, result.PlayerItems[0].CashgameId);
            Assert.AreEqual(PlayerData.Id1, result.PlayerItems[0].PlayerId);
            Assert.AreEqual(200, result.PlayerItems[0].Buyin.Amount);
            Assert.AreEqual(350, result.PlayerItems[0].Cashout.Amount);
            Assert.AreEqual(150, result.PlayerItems[0].Winnings.Amount);
            Assert.AreEqual(145, result.PlayerItems[0].WinRate.Amount);
        }
    }
}