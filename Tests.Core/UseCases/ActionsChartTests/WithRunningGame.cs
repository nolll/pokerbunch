using System;
using NUnit.Framework;

namespace Tests.Core.UseCases.ActionsChartTests
{
    public class WithRunningGame : Arrange
    {
        protected override bool GameIsRunning => true;

        [Test]
        public void ActionsAreCorrectIncludingCurrentStack()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(3, result.CheckpointItems.Count);

            Assert.AreEqual(DateTime.Parse("2001-01-01 13:00:00"), result.CheckpointItems[0].Timestamp);
            Assert.AreEqual(0, result.CheckpointItems[0].AddedMoney);
            Assert.AreEqual(200, result.CheckpointItems[0].Stack);
            Assert.AreEqual(200, result.CheckpointItems[0].TotalBuyin);

            Assert.AreEqual(DateTime.Parse("2001-01-01 14:00:00"), result.CheckpointItems[1].Timestamp);
            Assert.AreEqual(0, result.CheckpointItems[1].AddedMoney);
            Assert.AreEqual(50, result.CheckpointItems[1].Stack);
            Assert.AreEqual(200, result.CheckpointItems[1].TotalBuyin);

            Assert.AreEqual(DateTime.Parse("2001-01-01 14:02:00"), result.CheckpointItems[2].Timestamp);
            Assert.AreEqual(0, result.CheckpointItems[1].AddedMoney);
            Assert.AreEqual(50, result.CheckpointItems[1].Stack);
            Assert.AreEqual(200, result.CheckpointItems[1].TotalBuyin);
        }
    }
}