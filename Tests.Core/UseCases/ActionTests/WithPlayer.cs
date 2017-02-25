using Core.Entities;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.ActionTests
{
    public class WithPlayer : Arrange
    {
        protected override Role Role => Role.Player;

        private const string BuyinDescription = "Buyin";
        private const string CashoutDescription = "Cashout";

        [Test]
        public void ActionsResultIsReturned()
        {
            Assert.AreEqual(TimeData.Swedish("2001-01-01 12:00:00"), Result.Date);
            Assert.AreEqual(PlayerData.Name1, Result.PlayerName);
            Assert.AreEqual(2, Result.CheckpointItems.Count);
        }

        [Test]
        public void ItemPropertiesAreSet()
        {
            Assert.AreEqual(BuyinDescription, Result.CheckpointItems[0].Type);
            Assert.AreEqual(200, Result.CheckpointItems[0].DisplayAmount.Amount);
            Assert.AreEqual(TimeData.Swedish("2001-01-01 13:00:00"), Result.CheckpointItems[0].Time);
            Assert.IsFalse(Result.CheckpointItems[0].CanEdit);
            Assert.AreEqual("1", Result.CheckpointItems[0].CheckpointId);

            Assert.AreEqual(CashoutDescription, Result.CheckpointItems[1].Type);
            Assert.AreEqual(50, Result.CheckpointItems[1].DisplayAmount.Amount);
        }
    }
}