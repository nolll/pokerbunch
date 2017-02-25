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
            Assert.AreEqual("2001-01-01", Result.Date.IsoString);
            Assert.AreEqual(LocationData.Name1, Result.LocationName);
            Assert.AreEqual(62, Result.Duration.Minutes);
            Assert.AreEqual(TimeData.Swedish("2001-01-01 13:00:00"), Result.StartTime);
            Assert.AreEqual(TimeData.Swedish("2001-01-01 14:02:00"), Result.EndTime);
            Assert.IsFalse(Result.CanEdit);
            Assert.AreEqual(CashgameData.Id1, Result.CashgameId);
            Assert.AreEqual(2, Result.PlayerItems.Count);
        }

        [Test]
        public void PlayerResultItemsCountAndOrderIsCorrect()
        {
            Assert.AreEqual(2, Result.PlayerItems.Count);
            Assert.AreEqual(150, Result.PlayerItems[0].Winnings.Amount);
            Assert.AreEqual(-150, Result.PlayerItems[1].Winnings.Amount);
        }

        [Test]
        public void AllResultItemPropertiesAreSet()
        {
            Assert.AreEqual(PlayerData.Name1, Result.PlayerItems[0].Name);
            Assert.AreEqual(CashgameData.Id1, Result.PlayerItems[0].CashgameId);
            Assert.AreEqual(PlayerData.Id1, Result.PlayerItems[0].PlayerId);
            Assert.AreEqual(200, Result.PlayerItems[0].Buyin.Amount);
            Assert.AreEqual(350, Result.PlayerItems[0].Cashout.Amount);
            Assert.AreEqual(150, Result.PlayerItems[0].Winnings.Amount);
            Assert.AreEqual(145, Result.PlayerItems[0].WinRate.Amount);
        }
    }
}