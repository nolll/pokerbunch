using System;
using System.Collections.Generic;
using Application.Services;
using Core.Classes;
using Core.Classes.Checkpoints;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Action;
using Web.ModelFactories.PageBaseModelFactories;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Action{

	public class ActionPageModelFactoryTests : MockContainer {

		private Homegame _homegame;
		private Cashgame _cashgame;

        [SetUp]
		public void SetUp(){
			_homegame = new FakeHomegame();
			_cashgame = new FakeCashgame();
		}

		[Test]
        public void Heading_IsSet(){
            var player = new FakePlayer(displayName: "b");
            var cashgameResult = new FakeCashgameResult();
		    var dateTime = DateTime.Parse("2010-01-01 01:00:00");
		    _cashgame = new FakeCashgame(startTime: dateTime);

		    GetMock<IGlobalization>().Setup(o => o.FormatShortDate(dateTime, true)).Returns("a");

			var sut = GetSut();
		    var result = sut.Create(new FakeUser(), _homegame, _cashgame, player, cashgameResult, Role.Player);

			Assert.AreEqual(result.Heading, "Cashgame a, b");
		}

		[Test]
        public void Checkpoints_WithOneCheckpoint_HasOneCheckpoint(){
			var timestamp = DateTime.Parse("2010-01-01 01:00:00");
			const int stack = 1;
			var checkpoint = new FakeCheckpoint(timestamp, stack: stack);
            var player = new FakePlayer(displayName: "b");
            var cashgameResult = new FakeCashgameResult(checkpoints: new List<Checkpoint> { checkpoint });

			var sut = GetSut();
            var result = sut.Create(new FakeUser(), _homegame, _cashgame, player, cashgameResult, Role.Player);

			var checkpoints = result.Checkpoints;
			Assert.AreEqual(checkpoints.Count, 1);
		}

		[Test]
        public void ChartDataUrl_IsSet()
		{
		    const string chartDataUrl = "a";
            var player = new FakePlayer(displayName: "b");
            var cashgameResult = new FakeCashgameResult();
		    GetMock<IUrlProvider>().Setup(o => o.GetCashgameActionChartJsonUrl(_homegame.Slug, _cashgame.DateString, player.DisplayName)).Returns(chartDataUrl);

			var sut = GetSut();
            var result = sut.Create(new FakeUser(), _homegame, _cashgame, player, cashgameResult, Role.Player);

            Assert.AreEqual(chartDataUrl, result.ChartDataUrl);
		}

        private ActionPageModelFactory GetSut(){
            return new ActionPageModelFactory(
                GetMock<IPagePropertiesFactory>().Object,
                GetMock<IUrlProvider>().Object,
                GetMock<ICheckpointModelFactory>().Object,
                GetMock<IGlobalization>().Object);
		}

	}

}