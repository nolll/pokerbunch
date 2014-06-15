using System;
using System.Collections.Generic;
using Application.Services;
using Application.UseCases.Actions;
using Application.UseCases.CashgameContext;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Action;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.UrlModels;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Action
{
    public class ActionPageModelFactoryTests : MockContainer
    {

        private Homegame _homegame;
        private Cashgame _cashgame;

        [SetUp]
        public void SetUp()
        {
            _homegame = new HomegameInTest();
            _cashgame = new CashgameInTest();
        }

        /*
        [Test]
        public void Heading_IsSet()
        {
            var player = new PlayerInTest(displayName: "b");
            var cashgameResult = new CashgameResultInTest();
            var dateTime = DateTime.Parse("2010-01-01 01:00:00");
            _cashgame = new CashgameInTest(startTime: dateTime);

            GetMock<IGlobalization>().Setup(o => o.FormatShortDate(dateTime, true)).Returns("a");

            var sut = GetSut();
            var result = sut.Build(_homegame, _cashgame, player, cashgameResult, Role.Player);

            Assert.AreEqual(result.Heading, "Cashgame a, b");
        }

        [Test]
        public void Checkpoints_WithOneCheckpoint_HasOneCheckpoint()
        {
            var timestamp = DateTime.Parse("2010-01-01 01:00:00");
            const int stack = 1;
            var checkpoint = new CheckpointInTest(timestamp, stack: stack);
            var player = new PlayerInTest(displayName: "b");
            var cashgameResult = new CashgameResultInTest(checkpoints: new List<Checkpoint> { checkpoint });

            var sut = GetSut();
            var result = sut.Build(_homegame, _cashgame, player, cashgameResult, Role.Player);

            var checkpoints = result.Checkpoints;
            Assert.AreEqual(checkpoints.Count, 1);
        }

        [Test]
        public void ChartDataUrl_IsSet()
        {
            var player = new PlayerInTest(displayName: "b");
            var cashgameResult = new CashgameResultInTest();

            var sut = GetSut();
            var result = sut.Build(_homegame, _cashgame, player, cashgameResult, Role.Player);

            Assert.IsInstanceOf<CashgameActionChartJsonUrl>(result.ChartDataUrl);
        }
        */

        private ActionPageBuilder GetSut()
        {
            return new ActionPageBuilder(
                GetMock<ICheckpointModelFactory>().Object,
                GetMock<IGlobalization>().Object,
                GetMock<IHomegameRepository>().Object,
                GetMock<ICashgameRepository>().Object,
                GetMock<IPlayerRepository>().Object,
                GetMock<IAuth>().Object,
                GetMock<ICashgameContextInteractor>().Object,
                GetMock<IActionsInteractor>().Object);
        }
    }
}