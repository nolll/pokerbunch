using System;
using NUnit.Framework;

namespace Tests.Core.UseCases.ActionsChartTests
{
    public class WithEndedGame : Arrange
    {
        [Test]
        public void ActionsAreCorrect()
        {
            Assert.AreEqual(2, Result.CheckpointItems.Count);

            Assert.AreEqual(DateTime.Parse("2001-01-01 13:00:00"), Result.CheckpointItems[0].Timestamp);
            Assert.AreEqual(0, Result.CheckpointItems[0].AddedMoney);
            Assert.AreEqual(200, Result.CheckpointItems[0].Stack);
            Assert.AreEqual(200, Result.CheckpointItems[0].TotalBuyin);

            Assert.AreEqual(DateTime.Parse("2001-01-01 14:00:00"), Result.CheckpointItems[1].Timestamp);
            Assert.AreEqual(0, Result.CheckpointItems[1].AddedMoney);
            Assert.AreEqual(50, Result.CheckpointItems[1].Stack);
            Assert.AreEqual(200, Result.CheckpointItems[1].TotalBuyin);
        }
    }
}