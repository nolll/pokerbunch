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
			$this->homegame = new Homegame();
			$this->cashgame = new Cashgame();
		}

		function test_ColumnHeader_DateIsSet(){
			$this->cashgame->setStartTime(new DateTime('2010-01-01'));

			$sut = new ColumnHeaderModel($this->homegame, $this->cashgame);

			$this->assertEqual("Jan 1", $sut->date);
		}

		function test_ColumnHeader_ShowYearIsTrue_DateWithYearIsSet(){
			$this->cashgame->setStartTime(new DateTime('2010-01-01'));

			$sut = new ColumnHeaderModel($this->homegame, $this->cashgame, true);

			$this->assertEqual("Jan 1 2010", $sut->date);
		}

		function test_ColumnHeader_CashgameUrlIsSet(){
			$this->cashgame->setStartTime(new DateTime());

			$sut = new ColumnHeaderModel($this->homegame, $this->cashgame);

			$this->assertIsA($sut->cashgameUrl, 'app\Urls\CashgameDetailsUrlModel');
		}

	}

}