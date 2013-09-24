using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories;
using Web.Models.CashgameModels.Facts;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories{

	public class CashgameFactsPageModelFactoryTests : MockContainer
    {
        private CashgameSuite _suite;

        [SetUp]
		public void SetUp(){
			_suite = new CashgameSuite();
		}

		[Test]
        public void GameCount_SuiteHasGameCount_IsSet(){
			_suite.GameCount = 1;
			
            var result = GetResult();

			Assert.AreEqual(1, result.GameCount);
		}

		[Test]
        public void TotalGameTime_SuiteHasTotalGameTime_IsSet(){
			_suite.TotalGameTime = 1;
			
            var result = GetResult();

			Assert.AreEqual("1m", result.TotalGameTime);
		}

		[Test]
        public void BestResultAmount_SuiteHasBestResult_IsSet(){
			var cashgameResult = new CashgameResult {Winnings = 1};
		    _suite.BestResult = cashgameResult;

			var result = GetResult();

			Assert.AreEqual("+$1", result.BestResultAmount);
		}

		[Test]
        public void BestResultName_SuiteHasBestResult_IsSet(){
			var player = new Player {DisplayName = "a"};
		    var cashgameResult = new CashgameResult {Player = player};
		    _suite.BestResult = cashgameResult;
			
            var result = GetResult();

			Assert.AreEqual("a", result.BestResultName);
		}

		[Test]
        public void WorstResultAmount_SuiteHasWorstResult_IsSet(){
			var cashgameResult = new CashgameResult {Winnings = 1};
		    _suite.WorstResult = cashgameResult;
			
            var result = GetResult();

            Assert.AreEqual("+$1", result.WorstResultAmount);
		}

		[Test]
        public void WorstResultName_SuiteHasWorstResult_IsSet(){
			var player = new Player {DisplayName = "a"};
		    var cashgameResult = new CashgameResult {Player = player};
		    _suite.WorstResult = cashgameResult;

			var result = GetResult();

			Assert.AreEqual("a", result.WorstResultName);
		}

		[Test]
        public void MostTimeDuration_SuiteHasBestResult_IsSet(){
			var cashgameResult = new CashgameTotalResult {TimePlayed = 1};
		    _suite.MostTimeResult = cashgameResult;
			
            var result = GetResult();

			Assert.AreEqual("1m", result.MostTimeDuration);
		}

		[Test]
        public void MostTimeName_SuiteHasBestResult_IsSet(){
			var player = new Player {DisplayName = "a"};
		    var cashgameResult = new CashgameTotalResult {Player = player};
		    _suite.MostTimeResult = cashgameResult;
			
            var result = GetResult();

			Assert.AreEqual("a", result.MostTimeName);
		}

        private CashgameFactsPageModel GetResult()
        {
            return GetSut().Create(new User(), new Homegame(), _suite);
        }

		private CashgameFactsPageModelFactory GetSut(){
			return new CashgameFactsPageModelFactory(PagePropertiesFactoryMock.Object);
		}

	}

}