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
			$this->cashgameStorage = TestHelper::getFake(ClassNames::$CashgameStorage);
			$this->cashgameFactory = TestHelper::getFake(ClassNames::$CashgameFactory);
			$this->playerStorage = TestHelper::getFake(ClassNames::$PlayerStorage);
			$this->timer = new TimerFake();
			$this->cashgameSuiteFactory = TestHelper::getFake(ClassNames::$CashgameSuiteFactory);
			$this->cashgameResultFactory = TestHelper::getFake(ClassNames::$CashgameResultFactory);
			$this->sut = new CashgameRepositoryImpl($this->cashgameStorage, $this->cashgameFactory, $this->playerStorage, $this->timer, $this->cashgameSuiteFactory, $this->cashgameResultFactory);
		}

		function test_StartGame_CallsUpdateGameWithSetsCurrentDateAndStatusRunning(){
			$id = 1;
			$location = 'a';
			$status = GameStatus::created;
			$date = null;
			$cashgame = new Cashgame();
			$cashgame->setId($id);
			$cashgame->setLocation($location);
			$cashgame->setStatus($status);
			$cashgame->setStartTime($date);

			$this->timer->setTime(new DateTime('2001-01-01 01:00:00'));
			$expectedDate = '2001-01-01';
			$expectedStatus = GameStatus::running;
			$expectedRawCashgame = new RawCashgame($id, $location, $expectedStatus, $expectedDate);

			$this->cashgameStorage->expectOnce("updateGame", array($expectedRawCashgame));

			$this->sut->startGame($cashgame);
		}

		function test_EndGame_CallsUpdateGameWithSetsCurrentDateAndStatusRunning(){
			$id = 1;
			$location = 'a';
			$status = GameStatus::running;
			$date = null;
			$cashgame = new Cashgame();
			$cashgame->setId($id);
			$cashgame->setLocation($location);
			$cashgame->setStatus($status);
			$cashgame->setStartTime($date);

			$this->timer->setTime(new DateTime('2001-01-01 01:00:00'));
			$expectedDate = '2001-01-01';
			$expectedStatus = GameStatus::published;
			$expectedRawCashgame = new RawCashgame($id, $location, $expectedStatus, $expectedDate);

			$this->cashgameStorage->expectOnce("updateGame", array($expectedRawCashgame));

			$this->sut->endGame($cashgame);
		}

	}

}