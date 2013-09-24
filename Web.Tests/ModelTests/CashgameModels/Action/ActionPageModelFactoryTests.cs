using System;
using System.Collections.Generic;
using Core.Classes;
using Core.Classes.Checkpoints;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories;
using Web.Models.UrlModels;

namespace Web.Tests.ModelTests.CashgameModels.Action{

	public class ActionPageModelFactoryTests : MockContainer {

		private Homegame _homegame;
		private Player _player;
		private Cashgame _cashgame;
		private CashgameResult _result;

        [SetUp]
		public void SetUp(){
			_homegame = new Homegame();
			_player = new Player();
			_cashgame = new Cashgame();
			_result = new CashgameResult {Player = _player};
		}

		[Test]
        public void Heading_IsSet(){
			_player.DisplayName = "a";
			_cashgame.StartTime = DateTime.Parse("2010-01-01 01:00:00");

			var sut = GetSut();
		    var result = sut.Create(new User(), _homegame, _cashgame, _player, _result, Role.Player);

			Assert.AreEqual(result.Heading, "Cashgame Jan 1 2010, a");
		}

		[Test]
        public void Checkpoints_WithOneCheckpoint_HasOneCheckpoint(){
			var timestamp = DateTime.Parse("2010-01-01 01:00:00");
			const int stack = 1;
			var checkpoint = new ReportCheckpoint(timestamp, stack);
			_result.Checkpoints = new List<Checkpoint>{checkpoint};

			var sut = GetSut();
            var result = sut.Create(new User(), _homegame, _cashgame, _player, _result, Role.Player);

			var checkpoints = result.Checkpoints;
			Assert.AreEqual(checkpoints.Count, 1);
		}

		[Test]
        public void ChartDataUrl_IsSet(){
			var sut = GetSut();
            var result = sut.Create(new User(), _homegame, _cashgame, _player, _result, Role.Player);

            Assert.IsInstanceOf<CashgameActionChartJsonUrlModel>(result.ChartDataUrl);
		}

        private ActionPageModelFactory GetSut(){
			return new ActionPageModelFactory(PagePropertiesFactoryMock.Object);
		}

	}

}