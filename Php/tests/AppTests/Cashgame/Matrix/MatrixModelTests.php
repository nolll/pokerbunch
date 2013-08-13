<?php
namespace tests\AppTests\Cashgame\Matrix{

	use entities\CashgameSuite;
	use entities\Homegame;
	use Domain\Classes\User;
	use tests\UnitTestCase;
	use app\Cashgame\Matrix\MatrixModel;
	use tests\TestHelper;

	class MatrixModelTests extends UnitTestCase {

		function test_TableModel_IsSet(){
			$sut = $this->getSut();

			$this->assertIsA($sut->tableModel, 'app\Cashgame\Matrix\TableModel');
		}

		private function getSut(){
			$homegame = new Homegame();
			$suite = new CashgameSuite();
			return new MatrixModel(new User(), $homegame, $suite, null);
		}

	}

}