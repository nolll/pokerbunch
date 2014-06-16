using System;
using System.Collections.Generic;
using Application.Services;
using Application.Urls;
using Application.UseCases.Actions;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class ActionsResultTests
    {
        [Test]
        public void Construct_AllPropertiesAreSet()
        {
            var date = DateTime.Now;
            const string playerName = "a";

            var homegame = new HomegameInTest();
            var cashgame = new CashgameInTest(startTime: date);
            var player = new PlayerInTest(displayName: playerName);
            const Role role = Role.Player;
            var checkpoints = new List<Checkpoint> {new CheckpointInTest(), new CheckpointInTest()};
            var cashgameResult = new CashgameResultInTest(checkpoints: checkpoints);

            var result = new ActionsResult(homegame, cashgame, player, role, cashgameResult);

            Assert.AreEqual(date, result.Date);
            Assert.AreEqual(playerName, result.PlayerName);
            Assert.IsInstanceOf<CashgameActionChartJsonUrl>(result.ChartDataUrl);
            Assert.AreEqual(2, result.CheckpointItems.Count);
        }
    }

    class ActionsInteractorTests : MockContainer
    {
        [Test]
        public void Execute_ActionsResultIsReturned()
        {
            const string slug = "a";
            const string dateStr = "2001-01-01";
            const int playerId = 1;
            var request = new ActionsRequest(slug, dateStr, playerId);
            var homegame = new HomegameInTest();
            var cashgameResult = new CashgameResultInTest();
            var cashgame = new CashgameInTest(results: new List<CashgameResult> { cashgameResult });
            var player = new PlayerInTest();

            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);
            GetMock<ICashgameRepository>().Setup(o => o.GetByDateString(homegame, dateStr)).Returns(cashgame);
            GetMock<IPlayerRepository>().Setup(o => o.GetById(playerId)).Returns(player);

            var result = Sut.Execute(request);

            Assert.IsInstanceOf<ActionsResult>(result);
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