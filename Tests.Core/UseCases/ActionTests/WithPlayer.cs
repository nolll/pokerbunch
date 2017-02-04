using System;
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
        public void Actions_ActionsResultIsReturned()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(TimeData.Swedish("2001-01-01 12:00:00"), result.Date);
            Assert.AreEqual(PlayerData.Name1, result.PlayerName);
            Assert.AreEqual(2, result.CheckpointItems.Count);
        }

        [Test]
        public void Actions_ItemPropertiesAreSet()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(BuyinDescription, result.CheckpointItems[0].Type);
            Assert.AreEqual(200, result.CheckpointItems[0].DisplayAmount.Amount);
            Assert.AreEqual(TimeData.Swedish("2001-01-01 13:00:00"), result.CheckpointItems[0].Time);
            Assert.IsFalse(result.CheckpointItems[0].CanEdit);
            Assert.AreEqual("1", result.CheckpointItems[0].CheckpointId);

            Assert.AreEqual(CashoutDescription, result.CheckpointItems[1].Type);
            Assert.AreEqual(50, result.CheckpointItems[1].DisplayAmount.Amount);
        }
    }

    public class WithManager : Arrange
    {
        protected override Role Role => Role.Manager;

        [Test]
        public void CanEditIsTrueOnItem()
        {
            var result = Sut.Execute(Request);

            Assert.IsTrue(result.CheckpointItems[0].CanEdit);
        }
    }
}