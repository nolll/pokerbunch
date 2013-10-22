using System;
using System.Collections.Generic;
using Core.Classes;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories.Details;
using Web.Models.CashgameModels.Details;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Details{

	public class CashgameDetailsPageModelFactoryTests : WebMockContainer {

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
		public void Heading_IsSet()
        {
            const string formattedDate = "a";

            var dateTime = DateTime.Parse("2010-01-01 01:00:00");
            _cashgame.StartTime = dateTime;

            Mocks.GlobalizationMock.Setup(o => o.FormatShortDate(dateTime, true)).Returns(formattedDate);

            var result = GetResult();

			Assert.AreEqual("Cashgame a", result.Heading);
		}

		[Test]
		public void Duration_IsSet()
		{
		    const string formattedDuration = "a";
			_cashgame.Duration = 1;

		    Mocks.GlobalizationMock.Setup(o => o.FormatDuration(_cashgame.Duration)).Returns(formattedDuration);

			var result = GetResult();

            Assert.AreEqual(formattedDuration, result.Duration);
		}

		[Test]
		public void Duration_WithDurationLargerThanZero_IsEnabled(){
			_cashgame.Duration = 1;

			var result = GetResult();

			Assert.IsTrue(result.DurationEnabled);
		}

		[Test]
		public void StartTime_WithRunningGame_IsSet()
		{
		    const string formattedTime = "a";
		    var startTime = DateTime.Parse("2010-01-01 01:00:00");
			_cashgame.StartTime = startTime;
			_cashgame.Status = GameStatus.Running;

		    Mocks.GlobalizationMock.Setup(o => o.FormatTime(startTime)).Returns(formattedTime);

			var result = GetResult();

            Assert.AreEqual(formattedTime, result.StartTime);
		}

		[Test]
		public void EndTime_WithFinishedGame_IsSet(){
            const string formattedTime = "a";
            var endTime = DateTime.Parse("2010-01-01 01:00:00");
            _cashgame.EndTime = endTime;
			_cashgame.Status = GameStatus.Finished;

            Mocks.GlobalizationMock.Setup(o => o.FormatTime(endTime)).Returns(formattedTime);

			var result = GetResult();

			Assert.AreEqual(formattedTime, result.EndTime);
		}

		[Test]
		public void Location_IsSet(){
			_cashgame.Location = "a";

			var result = GetResult();

			Assert.AreEqual("a", result.Location);
		}

		[Test]
		public void EditUrl_IsCorrectType(){
            const string editUrl = "a";
            Mocks.UrlProviderMock.Setup(o => o.GetCashgameEditUrl(_homegame, _cashgame)).Returns(editUrl);

			var result = GetResult();

			Assert.AreEqual(editUrl, result.EditUrl);
		}

		[Test]
		public void CheckpointsUrl_IsCorrectType()
		{
		    const string actionUrl = "a";
		    Mocks.UrlProviderMock.Setup(o => o.GetCashgameActionUrl(_homegame, _cashgame, _player)).Returns(actionUrl);

			var result = GetResult();

			Assert.AreEqual(actionUrl, result.CheckpointsUrl);
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
		public void ResultTableModel_WithCreatedGame_IsCorrectType()
		{
		    Mocks.CashgameDetailsTableModelFactoryMock.Setup(o => o.Create(It.IsAny<Homegame>(), It.IsAny<Cashgame>()))
		         .Returns(new CashgameDetailsTableModel());
            
            var result = GetResult();

			Assert.IsNotNull(result.CashgameDetailsTableModel);
		}

		[Test]
		public void ChartDataUrl_IsSet()
		{
		    const string chartDataUrl = "a";
		    Mocks.UrlProviderMock.Setup(o => o.GetCashgameDetailsChartJsonUrl(_homegame, _cashgame)).Returns(chartDataUrl);

			var result = GetResult();

            Assert.AreEqual(chartDataUrl, result.ChartDataUrl);
		}

		private CashgameDetailsPageModel GetResult(){
			return GetSut().Create(new User(), _homegame, _cashgame, _player, null, _isManager);
		}

        private CashgameDetailsPageModelFactory GetSut()
        {
            return new CashgameDetailsPageModelFactory(
                Mocks.PagePropertiesFactoryMock.Object, 
                Mocks.CashgameDetailsTableModelFactoryMock.Object,
                Mocks.UrlProviderMock.Object,
                Mocks.GlobalizationMock.Object);
        }

	}

}