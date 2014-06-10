using Application.Services;
using Core.Entities;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Running;
using Web.Models.UrlModels;
using Web.Services;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Running{

	public class BarModelFactoryTests : MockContainer
    {

		private Homegame _homegame;
		private Cashgame _runningGame;

        [SetUp]
		public void SetUp(){
			_homegame = new FakeHomegame();
			_runningGame = null;
		}

		[Test]
        public void GameIsRunning_NoRunningGame_IsFalse(){
			var sut = GetSut();
		    var result = sut.Create(_homegame, _runningGame);

			Assert.IsFalse(result.GameIsRunning);
		}

		[Test]
        public void GameIsRunning_WithRunningGame_IsTrue(){
			_runningGame = new FakeCashgame();

			var sut = GetSut();
            var result = sut.Create(_homegame, _runningGame);

			Assert.IsTrue(result.GameIsRunning);
		}

        [Test]
        public void Url_WithRunningGame_IsSetToRunningGameUrl()
        {
            _runningGame = new FakeCashgame();

            var sut = GetSut();
            var result = sut.Create(_homegame, _runningGame);

            Assert.IsInstanceOf<RunningCashgameUrl>(result.Url);
        }

        [Test]
        public void Url_WithoutRunningGame_IsSetToAddGameUrl()
        {
            _runningGame = null;

            var sut = GetSut();
            var result = sut.Create(_homegame, _runningGame);

            Assert.IsInstanceOf<AddCashgameUrlModel>(result.Url);
        }

		private BarModelFactory GetSut(){
			return new BarModelFactory();
		}

	}

}