namespace tests\AppTests\Sharing{

	use entities\Cashgame;
	use entities\CashgameResult;
	use entities\Player;
	use Domain\Classes\User;
	use integration\Sharing\ResultSharerImpl;
	use tests\SharbatUnitTestCase;
	use core\ClassNames;
	use tests\TestHelper;

	class ResultSharerTests extends SharbatUnitTestCase {

		/** @var ResultSharerImpl */
		private $sut;
		private $sharingStorage;
		private $userStorage;
		private $playerRepository;
		private $socialServiceFactory;

		function setUp(){
			parent::setUp();
			$this->sharingStorage = $this->registerFake(ClassNames::$SharingStorage);
			$this->userStorage = $this->registerFake(ClassNames::$UserStorage);
			$this->playerRepository = $this->registerFake(ClassNames::$PlayerRepository);
			$this->socialServiceFactory = $this->registerFake(ClassNames::$SocialServiceFactory);
			$this->sut = new ResultSharerImpl($this->sharingStorage, $this->userStorage, $this->socialServiceFactory);;
		}

		function test_ShareSingleResult_OneUserHasNoServices_ShareResultIsNeverCalled(){
			$this->setupPlayer();
			$cashgameResult = new CashgameResult();
			$cashgameResult->setPlayer(new Player());
			$this->setupUser();
			$socialService = $this->setupSocialService();
			$this->sharingStorage->returns("getServices", array());

			$socialService->expectNever("shareResult");

			$this->sut->shareSingleResult($cashgameResult);
		}

		function test_ShareSingleResult_UserHasOneService_ShareResultIsCalledOnce(){
			$player = $this->setupPlayer();
			$cashgameResult = new CashgameResult();
			$cashgameResult->setPlayer($player);
			$this->setupUser();
			$socialService = $this->setupSocialService();
			$this->sharingStorage->returns("getServices", array("service1"));

			$socialService->expectOnce("shareResult");

			$this->sut->shareSingleResult($cashgameResult);
		}

		function test_ShareSingleResult_UserHasTwoServices_ShareResultIsCalledTwice(){
			$player = $this->setupPlayer();
			$cashgameResult = new CashgameResult();
			$cashgameResult->setPlayer($player);
			$this->setupUser();
			$socialService = $this->setupSocialService();
			$this->sharingStorage->returns("getServices", array("service1", "service2"));

			$socialService->expectCallCount("shareResult", 2);

			$this->sut->shareSingleResult($cashgameResult);
		}

		function test_ShareResult_TwoUsersHasOneServiceEach_ShareResultCalledTwice(){
			$cashgame = new Cashgame();
			$result1 = new CashgameResult();
			$player1 = new Player();
			$result1->setPlayer($player1);
			$result2 = new CashgameResult();
			$player2 = new Player();
			$result2->setPlayer($player2);
			$cashgame->setResults(array($result1, $result2));
			$this->setupPlayer();
			$this->setupUser();
			$socialService = $this->setupSocialService();
			$this->sharingStorage->returns("getServices", array("service1"));

			$socialService->expectCallCount("shareResult", 2);

			$this->sut->shareResult($cashgame);
		}

		function setupPlayer(){
			$player = new Player();
			$this->playerRepository->returns("getPlayerById", $player);
			return $player;
		}

		function setupUser(){
			$user = new User();
			$this->userStorage->returns("getUserByName", $user);
		}

		function setupSocialService(){
			$socialService = TestHelper::getFake('integration\Sharing\SocialService');
			$this->socialServiceFactory->returns("makeSocialService", $socialService);
			return $socialService;
		}

	}

}