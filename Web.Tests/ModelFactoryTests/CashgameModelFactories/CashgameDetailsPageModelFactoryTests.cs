using System;
using System.Collections.Generic;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories;
using Web.Models.CashgameModels.Details;
using Web.Models.UrlModels;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories{

	public class CashgameDetailsPageModelFactoryTests : MockContainer {

		private Homegame _homegame;
		private Cashgame _cashgame;
		private Player _player;
		private bool _isManager;

		[SetUp]
        public void SetUp(){
			_isManager = false;
			_homegame = new Homegame();
			_cashgame = new Cashgame();
			_player = new Player();
		}

        [Test]
		public void Heading_IsSet(){
			_cashgame.StartTime = DateTime.Parse("2010-01-01 01:00:00");

            var result = GetResult();

			Assert.AreEqual("Cashgame Jan 1 2010", result.Heading);
		}

		[Test]
		public void Duration_IsSet(){
			_cashgame.Duration = 1;

			var result = GetResult();

			Assert.AreEqual("1m", result.Duration);
		}

		[Test]
		public void Duration_WithDurationLargerThanZero_IsEnabled(){
			_cashgame.Duration = 1;

			var result = GetResult();

			Assert.IsTrue(result.DurationEnabled);
		}

		[Test]
		public void StartTime_WithRunningGame_IsSet(){
			_cashgame.StartTime = DateTime.Parse("2010-01-01 01:00:00");
			_cashgame.Status = GameStatus.Running;

			var result = GetResult();

			Assert.AreEqual("01:00", result.StartTime);
		}

		[Test]
		public void EndTime_WithFinishedGame_IsSet(){
			_cashgame.EndTime = DateTime.Parse("2010-01-01 01:00:00");
			_cashgame.Status = GameStatus.Finished;

			var result = GetResult();

			Assert.AreEqual("01:00", result.EndTime);
		}

		[Test]
		public void Location_IsSet(){
			_cashgame.Location = "a";

			var result = GetResult();

			Assert.AreEqual("a", result.Location);
		}

		[Test]
		public void EditUrl_IsCorrectType(){
			var result = GetResult();

			Assert.IsInstanceOf<CashgameEditUrlModel>(result.EditUrl);
		}

		[Test]
		public void CheckpointsUrl_IsCorrectType(){
			var result = GetResult();

			Assert.IsInstanceOf<CashgameActionUrlModel>(result.CheckpointsUrl);
		}

		[Test]
		public void ShowStartTime_WithCreatedGame_IsFalse(){
			_cashgame.Status = GameStatus.Created;

			var result = GetResult();

			Assert.IsFalse(result.ShowStartTime);
		}

		[Test]
		public void ShowEndTime_WithCreatedGame_IsFalse(){
			_cashgame.Status = GameStatus.Created;

			var result = GetResult();

			Assert.IsFalse(result.ShowEndTime);
		}

		[Test]
		public void EnableCheckpointsButton_WithPlayerNotInGame_IsFalse(){
			var result = GetResult();

			Assert.IsFalse(result.EnableCheckpointsButton);
		}

		[Test]
		public void EnableCheckpointsButton_WithPlayerInGame_IsTrue(){
			_player.Id = 1;
            var cashgameResult = new CashgameResult {Player = _player};
            _cashgame.Results = new List<CashgameResult>{cashgameResult};

			var result = GetResult();

			Assert.IsTrue(result.EnableCheckpointsButton);
		}

		[Test]
		public void EnableCheckpointsButton_WithFinishedGame_IsFalse(){
			_cashgame.Status = GameStatus.Finished;

			var result = GetResult();

			Assert.IsFalse(result.EnableCheckpointsButton);
		}

		[Test]
		public void ShowEndTime_WithRunningGame_IsFalse(){
			_cashgame.Status = GameStatus.Running;

			var result = GetResult();

			Assert.IsFalse(result.ShowEndTime);
		}

		[Test]
		public void EnableEdit_WithPlayerRights_IsFalse(){
			var result = GetResult();

			Assert.IsFalse(result.EnableEdit);
		}

		[Test]
		public void EnableEdit_WithManagerRights_IsTrue(){
			_isManager = true;

			var result = GetResult();

			Assert.IsTrue(result.EnableEdit);
		}

		[Test]
		public void Status_IsNotNull(){
			var result = GetResult();

			Assert.IsNotNull(result.Status);
		}

		[Test]
		public void ResultTableModel_WithCreatedGame_IsCorrectType(){
			var result = GetResult();

			Assert.IsInstanceOf<CashgameDetailsTableModel>(result.CashgameDetailsTableModel);
		}

		[Test]
		public void ChartDataUrl_IsSet(){
			var result = GetResult();

            Assert.IsInstanceOf<CashgameDetailsChartJsonUrlModel>(result.ChartDataUrl);
		}

		private CashgameDetailsPageModel GetResult(){
			return GetSut().Create(new User(), _homegame, _cashgame, _player, null, _isManager);
		}

        private CashgameDetailsPageModelFactory GetSut()
        {
            return new CashgameDetailsPageModelFactory(PagePropertiesFactoryMock.Object);
        }

	}

}