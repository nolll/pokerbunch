namespace tests\AppTests\Cashgame\Listing{

	use app\Cashgame\Listing\CashgameListingModel;
	use entities\Homegame;
	use entities\GameStatus;
	use Domain\Classes\User;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class CashgameListingModelTests extends UnitTestCase {

		function test_ListTableModel_IsSet(){
			$sut = new CashgameListingModel(new User(), new Homegame, array());

			assertIsA($sut.listTableModel, 'app\Cashgame\Listing\CashgameTable\CashgameTableModel');
		}

	}

}