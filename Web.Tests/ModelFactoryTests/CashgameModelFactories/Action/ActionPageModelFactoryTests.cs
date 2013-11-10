using System;
using System.Collections.Generic;
using Core.Classes;
using Core.Classes.Checkpoints;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Action;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Action{

	public class ActionPageModelFactoryTests : WebMockContainer {

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
            var cashgameResult = new CashgameResult { Player = player };
		    var dateTime = DateTime.Parse("2010-01-01 01:00:00");
		    _cashgame = new FakeCashgame(startTime: dateTime);

		    Mocks.GlobalizationMock.Setup(o => o.FormatShortDate(dateTime, true)).Returns("a");

			var sut = GetSut();
		    var result = sut.Create(new User(), _homegame, _cashgame, player, cashgameResult, Role.Player);

			Assert.AreEqual(result.Heading, "Cashgame a, b");
		}

		[Test]
        public void Checkpoints_WithOneCheckpoint_HasOneCheckpoint(){
			var timestamp = DateTime.Parse("2010-01-01 01:00:00");
			const int stack = 1;
			var checkpoint = new Checkpoint{Timestamp = timestamp, Stack = stack};
            var player = new FakePlayer(displayName: "b");
            var cashgameResult = new CashgameResult { Player = player };
            cashgameResult.Checkpoints = new List<Checkpoint>{checkpoint};

			var sut = GetSut();
            var result = sut.Create(new User(), _homegame, _cashgame, player, cashgameResult, Role.Player);

			var checkpoints = result.Checkpoints;
			Assert.AreEqual(checkpoints.Count, 1);
		}

		[Test]
        public void ChartDataUrl_IsSet()
		{
		    const string chartDataUrl = "a";
            var player = new FakePlayer(displayName: "b");
            var cashgameResult = new CashgameResult { Player = player };
		    Mocks.UrlProviderMock.Setup(o => o.GetCashgameActionChartJsonUrl(_homegame, _cashgame, player)).Returns(chartDataUrl);

			var sut = GetSut();
            var result = sut.Create(new User(), _homegame, _cashgame, player, cashgameResult, Role.Player);

            Assert.AreEqual(chartDataUrl, result.ChartDataUrl);
		}

        private ActionPageModelFactory GetSut(){
            return new ActionPageModelFactory(
                Mocks.PagePropertiesFactoryMock.Object,
                Mocks.UrlProviderMock.Object,
                Mocks.CheckpointModelFactoryMock.Object,
                Mocks.GlobalizationMock.Object);
		}

	}

}