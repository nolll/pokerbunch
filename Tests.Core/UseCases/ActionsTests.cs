using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Urls;
using Core.UseCases.Actions;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class ActionsTests : TestBase
    {
        private const string DateStr = "2001-01-01";
        private const string ReportDescription = "Report";
        private const string BuyinDescription = "Buyin";
        private const string CashoutDescription = "Cashout";
        private DateTime _date;
        private DateTime _checkpointTime;

        [SetUp]
        public virtual void SetUp()
        {
            _date = DateTime.Now;
            _checkpointTime = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
        }

        [Test]
        public void Actions_ActionsResultIsReturned()
        {
            var request = new ActionsInput(Constants.SlugA, DateStr, Constants.PlayerIdA);

            SetupGame();
            
            var result = Execute(request);

            Assert.AreEqual(_date, result.Date);
            Assert.AreEqual(Constants.PlayerNameA, result.PlayerName);
            Assert.AreEqual(3, result.CheckpointItems.Count);
        }

        [Test]
        public void Actions_ItemPropertiesAreSet()
        {
            var request = new ActionsInput(Constants.SlugA, DateStr, Constants.PlayerIdA);

            SetupGame();

            var result = Execute(request);

            Assert.AreEqual(ReportDescription, result.CheckpointItems[0].Type);
            Assert.AreEqual(1, result.CheckpointItems[0].DisplayAmount.Amount);
            Assert.AreEqual(_checkpointTime, result.CheckpointItems[0].Time);
            Assert.IsFalse(result.CheckpointItems[0].CanEdit);
            Assert.IsInstanceOf<EditCheckpointUrl>(result.CheckpointItems[0].EditUrl);

            Assert.AreEqual(BuyinDescription, result.CheckpointItems[1].Type);
            Assert.AreEqual(2, result.CheckpointItems[1].DisplayAmount.Amount);
        
            Assert.AreEqual(CashoutDescription, result.CheckpointItems[2].Type);
            Assert.AreEqual(1, result.CheckpointItems[2].DisplayAmount.Amount);
        }

        [Test]
        public void Actions_WithManager_CanEditIsTrueOnItem()
        {
            var request = new ActionsInput(Constants.SlugA, DateStr, Constants.PlayerIdA);

            SetupGame();
            Services.Auth.SetCurrentRole(Role.Manager);
            
            var result = Execute(request);

            Assert.IsTrue(result.CheckpointItems[0].CanEdit);
        }

        private void SetupGame()
        {
            var checkpoint1 = A.Checkpoint.WithStack(1).WithTimestamp(_checkpointTime).Build();
            var checkpoint2 = A.Checkpoint.WithStack(1).WithAmount(2).OfType(CheckpointType.Buyin).Build();
            var checkpoint3 = A.Checkpoint.WithStack(1).OfType(CheckpointType.Cashout).Build();
            var checkpoints = new List<Checkpoint> { checkpoint1, checkpoint2, checkpoint3 };
            var cashgameResult = A.CashgameResult.WithPlayerId(Constants.PlayerIdA).WithCheckpoints(checkpoints).Build();
            var cashgame = A.Cashgame.WithStartTime(_date).WithResults(new List<CashgameResult> { cashgameResult }).Build();
            GetMock<ICashgameRepository>().Setup(o => o.GetByDateString(It.IsAny<Bunch>(), DateStr)).Returns(cashgame);
        }

        private ActionsOutput Execute(ActionsInput input)
        {
            return ActionsInteractor.Execute(
                Repos.Bunch,
                GetMock<ICashgameRepository>().Object,
                Repos.Player,
                Services.Auth,
                input);
        }
    }
}