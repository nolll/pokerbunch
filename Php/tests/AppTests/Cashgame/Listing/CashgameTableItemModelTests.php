namespace tests\AppTests\Cashgame\Listing{

	use DateTime;
	use entities\Cashgame;
	use entities\Homegame;
	use tests\UnitTestCase;
	use app\Cashgame\Listing\CashgameTableItem\CashgameTableItemModel;
	use entities\GameStatus;
	use tests\TestHelper;

	class CashgameTableItemModelTests extends UnitTestCase {

		/** @var Homegame */
		private $homegame;
		/** @var Cashgame */
		private $cashgame;
		private $showYear;

		function setUp(){
			$this->homegame = new Homegame();
			$this->cashgame = new Cashgame();
			$this->showYear = false;
		}

		function test_TableItem_SetsPlayerCount(){
			$this->cashgame->setNumPlayers(2);

			$sut = $this->getSut();

			$this->assertIdentical(2, $sut->playerCount);
		}

		function test_TableItem_SetsLocation(){
			$this->cashgame->setLocation('a');

			$sut = $this->getSut();

			$this->assertIdentical('a', $sut->location);
		}

		function test_TableItem_WithDuration_SetsDuration(){
			$this->cashgame->setDuration(1);

			$sut = $this->getSut();

			$this->assertIdentical("1m", $sut->duration);
		}

		function test_TableItem_SetsTurnover(){
			$this->cashgame->setTurnOver(1);

			$sut = $this->getSut();

			$this->assertIdentical("$1", $sut->turnover);
		}

		function test_TableItem_SetsAvgBuyin(){
			$this->cashgame->setAverageBuyin(1);

			$sut = $this->getSut();

			$this->assertIdentical("$1", $sut->avgBuyin);
		}

		function test_TableItem_WithNoPlayers_DoesNotThrowDivisionByZeroException(){
			$this->cashgame = new Cashgame();
			$this->cashgame->setStartTime(new DateTime());

			$this->getSut();
		}

		function test_TableItem_SetsDetailsUrl(){
			$sut = $this->getSut();

			$this->assertIsA($sut->detailsUrl, 'app\Urls\CashgameDetailsUrlModel');
		}

		function test_TableItem_SetsDisplayDate(){
			$this->cashgame->setStartTime(new DateTime('2010-01-01 01:00:00'));

			$sut = $this->getSut();

			$this->assertIdentical('Jan 1', $sut->displayDate);
		}

		function test_TableItem_WithShowDateSetToTrue_SetsDisplayDate(){
			$this->showYear = true;
			$this->cashgame->setStartTime(new DateTime('2010-01-01 01:00:00'));

			$sut = $this->getSut();

			$this->assertIdentical('Jan 1 2010', $sut->displayDate);
		}

		function test_TableItem_WithPublishedGame_SetsEmptyPublishedClass(){
			$this->cashgame->setStatus(GameStatus::published);

			$sut = $this->getSut();

			$this->assertIdentical("", $sut->publishedClass);
		}

		function test_TableItem_WithUnpublishedGame_SetsPublishedClassToUnpublished(){
			$this->cashgame->setStatus(GameStatus::finished);

			$sut = $this->getSut();

			$this->assertIdentical("unpublished", $sut->publishedClass);
		}

		function getSut(){
			return new CashgameTableItemModel($this->homegame, $this->cashgame, $this->showYear);
		}

	}

}