using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories.Running;
using Web.Models.UrlModels;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Running{

	public class BarModelFactoryTests : WebMockContainer
    {

		private Homegame _homegame;
		private Cashgame _runningGame;

        [SetUp]
		public void SetUp(){
			_homegame = new Homegame();
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
			_runningGame = new Cashgame();

			var sut = GetSut();
            var result = sut.Create(_homegame, _runningGame);

			Assert.IsTrue(result.GameIsRunning);
		}

		[Test]
        public void GameUrl_WithRunningGame_IsSet(){
			_runningGame = new Cashgame();

		    const string runningGameUrl = "a";
		    Mocks.UrlProviderMock.Setup(o => o.GetRunningCashgameUrl(_homegame)).Returns(runningGameUrl);

			var sut = GetSut();
            var result = sut.Create(_homegame, _runningGame);

            Assert.AreEqual(runningGameUrl, result.GameUrl);
		}

		private BarModelFactory GetSut(){
			return new BarModelFactory(
                Mocks.UrlProviderMock.Object);
		}

	}

}