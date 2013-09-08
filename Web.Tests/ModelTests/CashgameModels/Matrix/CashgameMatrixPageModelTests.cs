using Core.Classes;
using NUnit.Framework;
using Web.Models.CashgameModels.Matrix;

namespace Web.Tests.ModelTests.CashgameModels.Matrix{

	class CashgameMatrixPageModelTests {

        [Test]
		public void TableModel_IsSet(){
			var sut = GetSut();

			Assert.IsInstanceOf<CashgameMatrixTableModel>(sut.TableModel);
		}

		private CashgameMatrixPageModel GetSut(){
			var homegame = new Homegame();
			var suite = new CashgameSuite();
			return new CashgameMatrixPageModel(new User(), homegame, suite, null, null, null);
		}

	}

}