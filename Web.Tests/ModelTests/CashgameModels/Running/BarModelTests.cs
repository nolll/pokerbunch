using Core.Classes;
using NUnit.Framework;
using Web.Models.CashgameModels.Running;
using Web.Models.UrlModels;

namespace Web.Tests.ModelTests.CashgameModels.Running{

	public class BarModelTests {

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

			Assert.IsFalse(sut.GameIsRunning);
		}

		[Test]
        public void GameIsRunning_WithRunningGame_IsTrue(){
			_runningGame = new Cashgame();

			var sut = GetSut();

			Assert.IsTrue(sut.GameIsRunning);
		}

		[Test]
        public void GameUrl_WithRunningGame_IsSet(){
			_runningGame = new Cashgame();

			var sut = GetSut();

            Assert.IsInstanceOf<RunningCashgameUrlModel>(sut.GameUrl);
		}

		private BarModel GetSut(){
			return new BarModel(_homegame, _runningGame);
		}

	}

}