using System.Collections.Generic;
using Core.Classes;
using NUnit.Framework;
using Web.Models.CashgameModels.Matrix;
using Web.Models.UrlModels;

namespace Web.Tests.ModelTests.CashgameModels.Matrix{

    public class CashgameMatrixTableRowModelTests {

		private Homegame _homegame;
		private CashgameTotalResult _result;
		private int _rank;

        [SetUp]
		public void SetUp(){
			_homegame = new Homegame();
            _result = new CashgameTotalResult
                {
                    Player = new Player
                        {
                            DisplayName = "player name"
                        }
                };
            _rank = 1;
		}

        [Test]
		public void TableRow_RankIsSet(){
			var sut = GetSut();

			Assert.AreEqual(1, sut.Rank);
		}

		[Test]
		public void TableRow_PlayerNameIsSet(){
			var sut = GetSut();

			Assert.AreEqual("player name", sut.Name);
			Assert.AreEqual("player%20name", sut.UrlEncodedName);
		}

		[Test]
		public void TableRow_TotalResultIsSet(){
			_result.Winnings = 1;

			var sut = GetSut();

			Assert.AreEqual("+$1", sut.TotalResult);
		}

		[Test]
		public void TableRow_WithPositiveResult_WinningsClassIsSetToPosResult(){
			_result.Winnings = 1;
			var sut = GetSut();

			Assert.AreEqual("pos-result", sut.ResultClass);
		}

		[Test]
		public void TableRow_WithPositiveResult_WinningsClassIsSetToNegResult(){
			_result.Winnings = -1;
			var sut = GetSut();

			Assert.AreEqual("neg-result", sut.ResultClass);
		}

		[Test]
		public void TableRow_PlayerUrlIsSet(){
			var sut = GetSut();

			Assert.IsInstanceOf<PlayerDetailsUrlModel>(sut.PlayerUrl);
		}

		[Test]
		public void TableRow_CellModelsAreSet(){
			var sut = GetSut();

			Assert.AreEqual(1, sut.CellModels.Count);
			Assert.IsInstanceOf<CashgameMatrixTableCellModel>(sut.CellModels[0]);
		}

		private CashgameMatrixTableRowModel GetSut(){
			var suite = new CashgameSuite
			    {
			        Cashgames = new List<Cashgame>
			            {
			                new Cashgame()
			            }
			    };
		    return new CashgameMatrixTableRowModel(_homegame, suite, _result, _rank);
		}

	}

}