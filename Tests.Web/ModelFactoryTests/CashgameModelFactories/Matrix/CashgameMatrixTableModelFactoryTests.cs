using System;
using System.Collections.Generic;
using Core.Entities;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Matrix;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Matrix{

	public class CashgameMatrixTableModelFactoryTests : MockContainer
	{
		private readonly Homegame _homegame;
		private List<Cashgame> _cashgames;

	    public CashgameMatrixTableModelFactoryTests()
	    {
            _homegame = new HomegameInTest();
        }

        [SetUp]
		public void SetUp(){
		}

        private CashgameSuite GetSuite(IList<Cashgame> cashgames)
        {
            var totalResult = new CashgameTotalResultInTest();
            return new CashgameSuiteInTest
            (
                cashgames,
                new List<CashgameTotalResult>
			    {
			        totalResult, totalResult
			    }
            );
        }
        
		[Test]
		public void ShowYear_IsFalse(){
            _cashgames = GetCashgames();
            var suite = GetSuite(_cashgames);

            var sut = GetSut();
            var result = sut.Create(_homegame, suite);

			Assert.IsFalse(result.ShowYear);
		}

		[Test]
		public void ShowYear_SpansMultipleYears_IsTrue(){
			_cashgames = GetCashgames(2010, 2011);
            var suite = GetSuite(_cashgames);

			var sut = GetSut();
            var result = sut.Create(_homegame, suite);

			Assert.IsTrue(result.ShowYear);
		}

		private CashgameMatrixTableModelFactory GetSut(){
			return new CashgameMatrixTableModelFactory(
                GetMock<ICashgameMatrixTableColumnHeaderModelFactory>().Object,
                GetMock<ICashgameMatrixTableRowModelFactory>().Object);
		}

		private List<Cashgame> GetCashgames(int yearOne = 2010, int yearTwo = 2010)
		{
		    return new List<Cashgame>
		        {
		            new CashgameInTest
		                (
		                    status: GameStatus.Finished,
		                    startTime: GetTestDate(yearOne)
		                ),
		            new CashgameInTest
		                (
		                    status: GameStatus.Published,
		                    startTime: GetTestDate(yearOne)
		                ),
		            new CashgameInTest
		                (
		                    status: GameStatus.Published,
		                    startTime: GetTestDate(yearTwo)
		                )
		        };
		}

		private DateTime GetTestDate(int year){
			return DateTime.Parse(year + "-01-01");
		}

	}

}