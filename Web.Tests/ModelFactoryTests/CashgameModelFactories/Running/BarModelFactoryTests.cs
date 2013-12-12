using Core.Classes;
using Core.Services;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Running;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Running{

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

            const string runningGameUrl = "a";
            GetMock<IUrlProvider>().Setup(o => o.GetRunningCashgameUrl(_homegame.Slug)).Returns(runningGameUrl);

            var sut = GetSut();
            var result = sut.Create(_homegame, _runningGame);

            Assert.AreEqual(runningGameUrl, result.Url);
        }

        [Test]
        public void Url_WithoutRunningGame_IsSetToAddGameUrl()
        {
            _runningGame = null;

            const string addGameUrl = "a";
            GetMock<IUrlProvider>().Setup(o => o.GetCashgameAddUrl(_homegame)).Returns(addGameUrl);

            var sut = GetSut();
            var result = sut.Create(_homegame, _runningGame);

            Assert.AreEqual(addGameUrl, result.Url);
        }

		private BarModelFactory GetSut(){
			return new BarModelFactory(
                GetMock<IUrlProvider>().Object);
		}

	}

}