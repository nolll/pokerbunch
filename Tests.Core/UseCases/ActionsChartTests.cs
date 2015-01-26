using System;
using Core.UseCases.ActionsChart;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class ActionsChartTests : TestBase
    {
        [Test]
        public void ActionsChart_GameAPlayerA_CheckpointItemsAreCorrect()
        {
            var request = new ActionsChartRequest(Constants.SlugA, Constants.DateStringA, Constants.PlayerIdA, DateTime.UtcNow);

            var result = Execute(request);

            Assert.AreEqual(2, result.CheckpointItems.Count);

            Assert.AreEqual(DateTime.Parse("2001-01-01 11:00:00"), result.CheckpointItems[0].Timestamp);
            Assert.AreEqual(0, result.CheckpointItems[0].AddedMoney);
            Assert.AreEqual(200, result.CheckpointItems[0].Stack);
            Assert.AreEqual(200, result.CheckpointItems[0].TotalBuyin);

            Assert.AreEqual(DateTime.Parse("2001-01-01 12:01:00"), result.CheckpointItems[1].Timestamp);
            Assert.AreEqual(0, result.CheckpointItems[1].AddedMoney);
            Assert.AreEqual(50, result.CheckpointItems[1].Stack);
            Assert.AreEqual(200, result.CheckpointItems[1].TotalBuyin);
        }

        [Test]
        public void ActionsChart_GameAPlayerB_CheckpointItemsAreCorrect()
        {
            var request = new ActionsChartRequest(Constants.SlugA, Constants.DateStringA, Constants.PlayerIdB, DateTime.UtcNow);

            var result = Execute(request);

            Assert.AreEqual(3, result.CheckpointItems.Count);

            Assert.AreEqual(DateTime.Parse("2001-01-01 11:01:00"), result.CheckpointItems[0].Timestamp);
            Assert.AreEqual(0, result.CheckpointItems[0].AddedMoney);
            Assert.AreEqual(200, result.CheckpointItems[0].Stack);
            Assert.AreEqual(200, result.CheckpointItems[0].TotalBuyin);

            Assert.AreEqual(DateTime.Parse("2001-01-01 11:30:00"), result.CheckpointItems[1].Timestamp);
            Assert.AreEqual(0, result.CheckpointItems[1].AddedMoney);
            Assert.AreEqual(250, result.CheckpointItems[1].Stack);
            Assert.AreEqual(200, result.CheckpointItems[1].TotalBuyin);

            Assert.AreEqual(DateTime.Parse("2001-01-01 12:02:00"), result.CheckpointItems[2].Timestamp);
            Assert.AreEqual(0, result.CheckpointItems[2].AddedMoney);
            Assert.AreEqual(350, result.CheckpointItems[2].Stack);
            Assert.AreEqual(200, result.CheckpointItems[2].TotalBuyin);
        }

        [Test]
        public void ActionsChart_GameBPlayerA_CheckpointItemsAreCorrect()
        {
            var request = new ActionsChartRequest(Constants.SlugA, Constants.DateStringB, Constants.PlayerIdA, DateTime.UtcNow);

            var result = Execute(request);

            Assert.AreEqual(3, result.CheckpointItems.Count);

            Assert.AreEqual(DateTime.Parse("2002-02-02 11:00:00"), result.CheckpointItems[0].Timestamp);
            Assert.AreEqual(0, result.CheckpointItems[0].AddedMoney);
            Assert.AreEqual(200, result.CheckpointItems[0].Stack);
            Assert.AreEqual(200, result.CheckpointItems[0].TotalBuyin);

            Assert.AreEqual(DateTime.Parse("2002-02-02 11:30:00"), result.CheckpointItems[1].Timestamp);
            Assert.AreEqual(0, result.CheckpointItems[1].AddedMoney);
            Assert.AreEqual(450, result.CheckpointItems[1].Stack);
            Assert.AreEqual(200, result.CheckpointItems[1].TotalBuyin);

            Assert.AreEqual(DateTime.Parse("2002-02-02 12:31:00"), result.CheckpointItems[2].Timestamp);
            Assert.AreEqual(0, result.CheckpointItems[2].AddedMoney);
            Assert.AreEqual(550, result.CheckpointItems[2].Stack);
            Assert.AreEqual(200, result.CheckpointItems[2].TotalBuyin);
        }

        [Test]
        public void ActionsChart_GameBPlayerB_CheckpointItemsAreCorrect()
        {
            var request = new ActionsChartRequest(Constants.SlugA, Constants.DateStringB, Constants.PlayerIdB, DateTime.UtcNow);

            var result = Execute(request);

            Assert.AreEqual(3, result.CheckpointItems.Count);

            Assert.AreEqual(DateTime.Parse("2002-02-02 11:01:00"), result.CheckpointItems[0].Timestamp);
            Assert.AreEqual(0, result.CheckpointItems[0].AddedMoney);
            Assert.AreEqual(200, result.CheckpointItems[0].Stack);
            Assert.AreEqual(200, result.CheckpointItems[0].TotalBuyin);

            Assert.AreEqual(DateTime.Parse("2002-02-02 11:02:00"), result.CheckpointItems[1].Timestamp);
            Assert.AreEqual(200, result.CheckpointItems[1].AddedMoney);
            Assert.AreEqual(200, result.CheckpointItems[1].Stack);
            Assert.AreEqual(400, result.CheckpointItems[1].TotalBuyin);

            Assert.AreEqual(DateTime.Parse("2002-02-02 12:32:00"), result.CheckpointItems[2].Timestamp);
            Assert.AreEqual(0, result.CheckpointItems[2].AddedMoney);
            Assert.AreEqual(50, result.CheckpointItems[2].Stack);
            Assert.AreEqual(400, result.CheckpointItems[2].TotalBuyin);
        }

        [Test]
        public void ActionsChart_RunningGame_CheckpointItemsAreCorrect()
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new ActionsChartRequest(Constants.SlugA, Constants.DateStringC, Constants.PlayerIdA, Constants.StartTimeC.AddHours(1));

            var result = Execute(request);

            Assert.AreEqual(2, result.CheckpointItems.Count);

            Assert.AreEqual(DateTime.Parse("2003-03-03 11:00:00"), result.CheckpointItems[0].Timestamp);
            Assert.AreEqual(0, result.CheckpointItems[0].AddedMoney);
            Assert.AreEqual(200, result.CheckpointItems[0].Stack);
            Assert.AreEqual(200, result.CheckpointItems[0].TotalBuyin);

            Assert.AreEqual(DateTime.Parse("2003-03-03 12:00:00"), result.CheckpointItems[1].Timestamp);
            Assert.AreEqual(0, result.CheckpointItems[1].AddedMoney);
            Assert.AreEqual(200, result.CheckpointItems[1].Stack);
            Assert.AreEqual(200, result.CheckpointItems[1].TotalBuyin);
        }

        private ActionsChartResult Execute(ActionsChartRequest request)
        {
            return ActionsChartInteractor.Execute(
                Repos.Bunch,
                Repos.Cashgame,
                request);
        }
    }
}