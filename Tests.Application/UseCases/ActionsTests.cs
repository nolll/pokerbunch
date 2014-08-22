﻿using System;
using System.Collections.Generic;
using Application.Services;
using Application.Urls;
using Application.UseCases.Actions;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class ActionsTests : MockContainer
    {
        private const string Slug = "a";
        private const string DateStr = "2001-01-01";
        private const int PlayerId = 1;
        private const string PlayerName = "b";
        private const string CheckPointType = "c";
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
            
            var result = Sut.Execute(request);

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

            var result = Sut.Execute(request);

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

            var result = Sut.Execute(request);

            Assert.IsTrue(result.CheckpointItems[0].CanEdit);
        }

        private void SetupGame()
        {
            var homegame = new HomegameInTest();
            var checkpoint1 = new CheckpointInTest(description: CheckPointType, stack: 1, timestamp: _checkpointTime);
            var checkpoint2 = new CheckpointInTest();
            var checkpoints = new List<Checkpoint> { checkpoint1, checkpoint2 };
            var cashgameResult = new CashgameResultInTest(checkpoints: checkpoints);
            var cashgame = new CashgameInTest(startTime: _date, results: new List<CashgameResult> { cashgameResult });
            var player = new PlayerInTest(displayName: PlayerName);
            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(Slug)).Returns(homegame);
            GetMock<ICashgameRepository>().Setup(o => o.GetByDateString(homegame, DateStr)).Returns(cashgame);
            GetMock<IPlayerRepository>().Setup(o => o.GetById(PlayerId)).Returns(player);
        }

        private void SetupManager()
        {
            GetMock<IAuth>().Setup(o => o.IsInRole(It.IsAny<string>(), It.IsAny<Role>())).Returns(true);
        }

        private ActionsInteractor Sut
        {
            get
            {
                return new ActionsInteractor(
                    GetMock<IHomegameRepository>().Object,
                    GetMock<ICashgameRepository>().Object,
                    GetMock<IPlayerRepository>().Object,
                    GetMock<IAuth>().Object);
            }
        }
    }
}