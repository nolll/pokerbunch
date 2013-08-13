namespace tests\RepositoryTests{

	use core\ClassNames;
	use entities\Cashgame;
	use entities\CashgameSuiteFactoryImpl;
	use tests\Fakes\TimerFake;
	use DateTime;
	use Domain\Services\CashgameRepositoryImpl;
	use entities\GameStatus;
	use Infrastructure\Data\Classes\RawCashgame;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class CashgameRepositoryTests extends UnitTestCase {

		/** @var CashgameRepositoryImpl */
		private $sut;
		private $cashgameStorage;
		private $playerStorage;
		private $cashgameFactory;
		/** @var TimerFake */
		private $timer;
		private $cashgameSuiteFactory;
		private $cashgameResultFactory;

		function setUp(){
			cashgameStorage = TestHelper::getFake(ClassNames::$CashgameStorage);
			cashgameFactory = TestHelper::getFake(ClassNames::$CashgameFactory);
			playerStorage = TestHelper::getFake(ClassNames::$PlayerStorage);
			timer = new TimerFake();
			cashgameSuiteFactory = TestHelper::getFake(ClassNames::$CashgameSuiteFactory);
			cashgameResultFactory = TestHelper::getFake(ClassNames::$CashgameResultFactory);
			sut = new CashgameRepositoryImpl(cashgameStorage, cashgameFactory, playerStorage, timer, cashgameSuiteFactory, cashgameResultFactory);
		}

		function test_StartGame_CallsUpdateGameWithSetsCurrentDateAndStatusRunning(){
			$id = 1;
			$location = 'a';
			$status = GameStatus::created;
			$date = null;
			$cashgame = new Cashgame();
			$cashgame.setId($id);
			$cashgame.setLocation($location);
			$cashgame.setStatus($status);
			$cashgame.setStartTime($date);

			timer.setTime(new DateTime('2001-01-01 01:00:00'));
			$expectedDate = '2001-01-01';
			$expectedStatus = GameStatus::running;
			$expectedRawCashgame = new RawCashgame($id, $location, $expectedStatus, $expectedDate);

			cashgameStorage.expectOnce("updateGame", array($expectedRawCashgame));

			sut.startGame($cashgame);
		}

		function test_EndGame_CallsUpdateGameWithSetsCurrentDateAndStatusRunning(){
			$id = 1;
			$location = 'a';
			$status = GameStatus::running;
			$date = null;
			$cashgame = new Cashgame();
			$cashgame.setId($id);
			$cashgame.setLocation($location);
			$cashgame.setStatus($status);
			$cashgame.setStartTime($date);

			timer.setTime(new DateTime('2001-01-01 01:00:00'));
			$expectedDate = '2001-01-01';
			$expectedStatus = GameStatus::published;
			$expectedRawCashgame = new RawCashgame($id, $location, $expectedStatus, $expectedDate);

			cashgameStorage.expectOnce("updateGame", array($expectedRawCashgame));

			sut.endGame($cashgame);
		}

	}

}