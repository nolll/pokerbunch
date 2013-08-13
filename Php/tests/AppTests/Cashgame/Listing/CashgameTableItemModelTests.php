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
			homegame = new Homegame();
			cashgame = new Cashgame();
			showYear = false;
		}

		function test_TableItem_SetsPlayerCount(){
			cashgame.setNumPlayers(2);

			$sut = getSut();

			assertIdentical(2, $sut.playerCount);
		}

		function test_TableItem_SetsLocation(){
			cashgame.setLocation('a');

			$sut = getSut();

			assertIdentical('a', $sut.location);
		}

		function test_TableItem_WithDuration_SetsDuration(){
			cashgame.setDuration(1);

			$sut = getSut();

			assertIdentical("1m", $sut.duration);
		}

		function test_TableItem_SetsTurnover(){
			cashgame.setTurnOver(1);

			$sut = getSut();

			assertIdentical("$1", $sut.turnover);
		}

		function test_TableItem_SetsAvgBuyin(){
			cashgame.setAverageBuyin(1);

			$sut = getSut();

			assertIdentical("$1", $sut.avgBuyin);
		}

		function test_TableItem_WithNoPlayers_DoesNotThrowDivisionByZeroException(){
			cashgame = new Cashgame();
			cashgame.setStartTime(new DateTime());

			getSut();
		}

		function test_TableItem_SetsDetailsUrl(){
			$sut = getSut();

			assertIsA($sut.detailsUrl, 'app\Urls\CashgameDetailsUrlModel');
		}

		function test_TableItem_SetsDisplayDate(){
			cashgame.setStartTime(new DateTime('2010-01-01 01:00:00'));

			$sut = getSut();

			assertIdentical('Jan 1', $sut.displayDate);
		}

		function test_TableItem_WithShowDateSetToTrue_SetsDisplayDate(){
			showYear = true;
			cashgame.setStartTime(new DateTime('2010-01-01 01:00:00'));

			$sut = getSut();

			assertIdentical('Jan 1 2010', $sut.displayDate);
		}

		function test_TableItem_WithPublishedGame_SetsEmptyPublishedClass(){
			cashgame.setStatus(GameStatus::published);

			$sut = getSut();

			assertIdentical("", $sut.publishedClass);
		}

		function test_TableItem_WithUnpublishedGame_SetsPublishedClassToUnpublished(){
			cashgame.setStatus(GameStatus::finished);

			$sut = getSut();

			assertIdentical("unpublished", $sut.publishedClass);
		}

		function getSut(){
			return new CashgameTableItemModel(homegame, cashgame, showYear);
		}

	}

}