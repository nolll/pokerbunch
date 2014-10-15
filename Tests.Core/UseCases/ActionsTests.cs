﻿using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Services;
using Core.Urls;
using Core.UseCases.Actions;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class ActionsTests : TestBase
    {
        private const string Slug = "a";
        private const string DateStr = "2001-01-01";
        private const int PlayerId = 1;
        private const string PlayerName = "b";
        private const string CheckPointType = "Report";
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
            var request = new ActionsRequest(Slug, DateStr, PlayerId);

            SetupGame();
            
            var result = Execute(request);

            Assert.AreEqual(_date, result.Date);
            Assert.AreEqual(PlayerName, result.PlayerName);
            Assert.IsInstanceOf<CashgameActionChartJsonUrl>(result.ChartDataUrl);
            Assert.AreEqual(2, result.CheckpointItems.Count);
        }

        [Test]
        public void Actions_ItemPropertiesAreSet()
        {
            var request = new ActionsRequest(Slug, DateStr, PlayerId);

            SetupGame();

            var result = Execute(request);

            Assert.AreEqual(CheckPointType, result.CheckpointItems[0].Type);
            Assert.AreEqual(1, result.CheckpointItems[0].Stack.Amount);
            Assert.AreEqual(_checkpointTime, result.CheckpointItems[0].Time);
            Assert.IsFalse(result.CheckpointItems[0].CanEdit);
            Assert.IsInstanceOf<EditCheckpointUrl>(result.CheckpointItems[0].EditUrl);
        }

        [Test]
        public void Actions_WithManager_CanEditIsTrueOnItem()
        {
            var request = new ActionsRequest(Slug, DateStr, PlayerId);

            SetupGame();
            SetupManager();

            var result = Execute(request);

            Assert.IsTrue(result.CheckpointItems[0].CanEdit);
        }

        private void SetupGame()
        {
            var bunch = A.Bunch.Build();
            var checkpoint1 = A.Checkpoint.WithStack(1).WithTimestamp(_checkpointTime).Build();
            var checkpoint2 = A.Checkpoint.Build();
            var checkpoints = new List<Checkpoint> { checkpoint1, checkpoint2 };
            var cashgameResult = A.CashgameResult.WithCheckpoints(checkpoints).Build();
            var cashgame = A.Cashgame.WithStartTime(_date).WithResults(new List<CashgameResult> { cashgameResult }).Build();
            var player = A.Player.WithDisplayName(PlayerName).Build();
            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(Slug)).Returns(bunch);
            GetMock<ICashgameRepository>().Setup(o => o.GetByDateString(bunch, DateStr)).Returns(cashgame);
            GetMock<IPlayerRepository>().Setup(o => o.GetById(PlayerId)).Returns(player);
        }

        private void SetupManager()
        {
            GetMock<IAuth>().Setup(o => o.IsInRole(It.IsAny<string>(), It.IsAny<Role>())).Returns(true);
        }

        private ActionsResult Execute(ActionsRequest request)
        {
            return ActionsInteractor.Execute(GetMock<IBunchRepository>().Object, GetMock<ICashgameRepository>().Object, GetMock<IPlayerRepository>().Object, GetMock<IAuth>().Object, request);
        }
    }
}