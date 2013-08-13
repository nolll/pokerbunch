namespace tests\AppTests\Cashgame\Matrix{

	use DateTime;
	use entities\Cashgame;
	use entities\Homegame;
	use tests\UnitTestCase;
	use app\Cashgame\Matrix\ColumnHeaderModel;
	use tests\TestHelper;

	class ColumnHeaderModelTests extends UnitTestCase {

		/** @var Homegame */
		private $homegame;
		/** @var Cashgame */
		private $cashgame;

		function setUp(){
			homegame = new Homegame();
			cashgame = new Cashgame();
		}

		function test_ColumnHeader_DateIsSet(){
			cashgame.setStartTime(new DateTime('2010-01-01'));

			$sut = new ColumnHeaderModel(homegame, cashgame);

			assertEqual("Jan 1", $sut.date);
		}

		function test_ColumnHeader_ShowYearIsTrue_DateWithYearIsSet(){
			cashgame.setStartTime(new DateTime('2010-01-01'));

			$sut = new ColumnHeaderModel(homegame, cashgame, true);

			assertEqual("Jan 1 2010", $sut.date);
		}

		function test_ColumnHeader_CashgameUrlIsSet(){
			cashgame.setStartTime(new DateTime());

			$sut = new ColumnHeaderModel(homegame, cashgame);

			assertIsA($sut.cashgameUrl, 'app\Urls\CashgameDetailsUrlModel');
		}

	}

}