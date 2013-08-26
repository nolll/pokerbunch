using Core.Classes;
using NUnit.Framework;
using Web.Models.CashgameModels.Matrix;

namespace Web.Tests.ModelTests.CashgameModels.Matrix{

	class MatrixPageModelTests {

        [Test]
		public void TableModel_IsSet(){
			var sut = GetSut();

			Assert.IsInstanceOf<MatrixTableModel>(sut.TableModel);
		}

		private MatrixPageModel GetSut(){
			var homegame = new Homegame();
			var suite = new CashgameSuite();
			return new MatrixPageModel(new User(), homegame, suite, null, null, null);
		}

	}

}