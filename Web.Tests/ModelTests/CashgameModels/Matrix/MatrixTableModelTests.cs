using System;
using System.Collections.Generic;
using Core.Classes;
using NUnit.Framework;
using Web.Models.CashgameModels.Matrix;

namespace Web.Tests.ModelTests.CashgameModels.Matrix{

	public class MatrixTableModelTests {

		private Homegame _homegame;
		private List<Cashgame> _cashgames;

        [SetUp]
		public void SetUp(){
			_homegame = new Homegame();
			_cashgames = GetCashgames();
		}
        
		[Test]
		public void Results_IsCorrectLength(){
			var sut = GetSut();

			Assert.AreEqual(2, sut.RowModels.Count);
		}

		[Test]
		public void ShowYear_IsFalse(){
			var sut = GetSut();

			Assert.IsFalse(sut.ShowYear);
		}

		[Test]
		public void ShowYear_SpansMultipleYears_IsTrue(){
			_cashgames = GetCashgames(2010, 2011);
			var sut = GetSut();

			Assert.IsTrue(sut.ShowYear);
		}

		private MatrixTableModel GetSut(){
            var totalResult = new CashgameTotalResult();
			var suite = new CashgameSuite
			    {
			        Cashgames = _cashgames,
			        TotalResults = new List<CashgameTotalResult>
			            {
			                totalResult, totalResult
			            }
			    };
		    ;
			return new MatrixTableModel(_homegame, suite);
		}

		private List<Cashgame> GetCashgames(int yearOne = 2010, int yearTwo = 2010)
		{
		    return new List<Cashgame>
		        {
		            new Cashgame
		                {
		                    Status = GameStatus.Finished,
		                    StartTime = GetTestDate(yearOne)
		                },
		            new Cashgame
		                {
		                    Status = GameStatus.Published,
		                    StartTime = GetTestDate(yearOne)
		                },
		            new Cashgame
		                {
		                    Status = GameStatus.Published,
		                    StartTime = GetTestDate(yearTwo)
		                }
		        };
		}

		private DateTime GetTestDate(int year){
			return DateTime.Parse(year + "-01-01");
		}

	}

}