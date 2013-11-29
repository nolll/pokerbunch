using System;
using System.Collections.Generic;
using Core.Classes;
using Core.Services;
using Infrastructure.System;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Details;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Details;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Details{

	public class CashgameDetailsPageModelFactoryTests : MockContainer {

		private Homegame _homegame;
		private bool _isManager;

		[SetUp]
        public void SetUp(){
			_isManager = false;
			_homegame = new FakeHomegame();
		}

        [Test]
		public void Heading_IsSet()
        {
            const string formattedDate = "a";

            var dateTime = DateTime.Parse("2010-01-01 01:00:00");
            var cashgame = new FakeCashgame(startTime: dateTime);
            var player = new FakePlayer();

            GetMock<IGlobalization>().Setup(o => o.FormatShortDate(dateTime, true)).Returns(formattedDate);

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), _homegame, cashgame, player, _isManager);

			Assert.AreEqual("Cashgame a", result.Heading);
		}

		[Test]
		public void Duration_IsSet()
		{
		    const string formattedDuration = "a";
		    const int duration = 1;
            var cashgame = new FakeCashgame(duration: duration);
            var player = new FakePlayer();

		    GetMock<IGlobalization>().Setup(o => o.FormatDuration(duration)).Returns(formattedDuration);

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), _homegame, cashgame, player, _isManager);

            Assert.AreEqual(formattedDuration, result.Duration);
		}

		[Test]
		public void Duration_WithDurationLargerThanZero_IsEnabled(){
            var cashgame = new FakeCashgame(duration: 1);
            var player = new FakePlayer();

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), _homegame, cashgame, player, _isManager);

			Assert.IsTrue(result.DurationEnabled);
		}

		[Test]
		public void StartTime_WithRunningGame_IsSet()
		{
		    const string formattedTime = "a";
		    var startTime = DateTime.Parse("2010-01-01 01:00:00");
            var cashgame = new FakeCashgame(startTime: startTime, status: GameStatus.Running);
            var player = new FakePlayer();

		    GetMock<IGlobalization>().Setup(o => o.FormatTime(It.IsAny<DateTime>())).Returns(formattedTime);

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), _homegame, cashgame, player, _isManager);

            Assert.AreEqual(formattedTime, result.StartTime);
		}

		[Test]
		public void EndTime_WithFinishedGame_IsSet(){
            const string formattedTime = "a";
            var endTime = DateTime.Parse("2010-01-01 01:00:00");
            var cashgame = new FakeCashgame(endTime: endTime, status: GameStatus.Finished);
            var player = new FakePlayer();

            GetMock<IGlobalization>().Setup(o => o.FormatTime(It.IsAny<DateTime>())).Returns(formattedTime);

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), _homegame, cashgame, player, _isManager);

			Assert.AreEqual(formattedTime, result.EndTime);
		}

		[Test]
		public void Location_IsSet()
		{
		    const string location = "a";
            var cashgame = new FakeCashgame(location: location);
            var player = new FakePlayer();

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), _homegame, cashgame, player, _isManager);

			Assert.AreEqual(location, result.Location);
		}

		[Test]
		public void EditUrl_IsCorrectType(){
            const string editUrl = "a";
            var cashgame = new FakeCashgame();
            var player = new FakePlayer();

            GetMock<IUrlProvider>().Setup(o => o.GetCashgameEditUrl(_homegame, cashgame)).Returns(editUrl);

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), _homegame, cashgame, player, _isManager);

			Assert.AreEqual(editUrl, result.EditUrl);
		}

		[Test]
		public void CheckpointsUrl_IsCorrectType()
		{
		    const string actionUrl = "a";
            var cashgame = new FakeCashgame();
            var player = new FakePlayer();

		    GetMock<IUrlProvider>().Setup(o => o.GetCashgameActionUrl(_homegame, cashgame, player)).Returns(actionUrl);

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), _homegame, cashgame, player, _isManager);

			Assert.AreEqual(actionUrl, result.CheckpointsUrl);
		}

		[Test]
		public void ShowStartTime_WithCreatedGame_IsFalse(){
            var cashgame = new FakeCashgame();
            var player = new FakePlayer();

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), _homegame, cashgame, player, _isManager);

			Assert.IsFalse(result.ShowStartTime);
		}

		[Test]
		public void ShowEndTime_WithCreatedGame_IsFalse(){
            var cashgame = new FakeCashgame();
            var player = new FakePlayer();

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), _homegame, cashgame, player, _isManager);

			Assert.IsFalse(result.ShowEndTime);
		}

		[Test]
		public void EnableCheckpointsButton_WithPlayerNotInGame_IsFalse(){
            var cashgame = new FakeCashgame();
            var player = new FakePlayer();

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), _homegame, cashgame, player, _isManager);

			Assert.IsFalse(result.EnableCheckpointsButton);
		}

		[Test]
		public void EnableCheckpointsButton_WithPlayerInGame_IsTrue()
		{
		    const int playerId = 1;
            var player = new FakePlayer(playerId);
            var cashgameResult = new FakeCashgameResult(playerId);
            var cashgame = new FakeCashgame(results: new List<CashgameResult> { cashgameResult });

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), _homegame, cashgame, player, _isManager);

			Assert.IsTrue(result.EnableCheckpointsButton);
		}

		[Test]
		public void EnableCheckpointsButton_WithFinishedGame_IsFalse(){
            var cashgame = new FakeCashgame(status: GameStatus.Finished);
            var player = new FakePlayer();

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), _homegame, cashgame, player, _isManager);

			Assert.IsFalse(result.EnableCheckpointsButton);
		}

		[Test]
		public void ShowEndTime_WithRunningGame_IsFalse(){
            var cashgame = new FakeCashgame(status: GameStatus.Running);
            var player = new FakePlayer();

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), _homegame, cashgame, player, _isManager);

			Assert.IsFalse(result.ShowEndTime);
		}

		[Test]
		public void EnableEdit_WithPlayerRights_IsFalse(){
            var cashgame = new FakeCashgame();
            var player = new FakePlayer();

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), _homegame, cashgame, player, _isManager);

			Assert.IsFalse(result.EnableEdit);
		}

		[Test]
		public void EnableEdit_WithManagerRights_IsTrue(){
			_isManager = true;
            var cashgame = new FakeCashgame();
            var player = new FakePlayer();

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), _homegame, cashgame, player, _isManager);

			Assert.IsTrue(result.EnableEdit);
		}

		[Test]
		public void Status_IsNotNull(){
            var cashgame = new FakeCashgame();
            var player = new FakePlayer();
            
            var sut = GetSut();
            var result = sut.Create(new FakeUser(), _homegame, cashgame, player, _isManager);

			Assert.IsNotNull(result.Status);
		}

		[Test]
		public void ResultTableModel_WithCreatedGame_IsCorrectType()
		{
            var cashgame = new FakeCashgame();
            var player = new FakePlayer();

		    GetMock<ICashgameDetailsTableModelFactory>().Setup(o => o.Create(It.IsAny<Homegame>(), It.IsAny<Cashgame>()))
		         .Returns(new CashgameDetailsTableModel());

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), _homegame, cashgame, player, _isManager);

			Assert.IsNotNull(result.CashgameDetailsTableModel);
		}

		[Test]
		public void ChartDataUrl_IsSet()
		{
		    const string chartDataUrl = "a";
            var cashgame = new FakeCashgame();
            var player = new FakePlayer();

		    GetMock<IUrlProvider>().Setup(o => o.GetCashgameDetailsChartJsonUrl(_homegame, cashgame)).Returns(chartDataUrl);

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), _homegame, cashgame, player, _isManager);

            Assert.AreEqual(chartDataUrl, result.ChartDataUrl);
		}

        private CashgameDetailsPageModelFactory GetSut()
        {
            return new CashgameDetailsPageModelFactory(
                GetMock<IPagePropertiesFactory>().Object, 
                GetMock<ICashgameDetailsTableModelFactory>().Object,
                GetMock<IUrlProvider>().Object,
                GetMock<IGlobalization>().Object);
        }

	}

}