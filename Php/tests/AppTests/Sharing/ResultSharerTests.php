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
			sharingStorage = registerFake(ClassNames::$SharingStorage);
			userStorage = registerFake(ClassNames::$UserStorage);
			playerRepository = registerFake(ClassNames::$PlayerRepository);
			socialServiceFactory = registerFake(ClassNames::$SocialServiceFactory);
			sut = new ResultSharerImpl(sharingStorage, userStorage, socialServiceFactory);;
		}

		function test_ShareSingleResult_OneUserHasNoServices_ShareResultIsNeverCalled(){
			setupPlayer();
			$cashgameResult = new CashgameResult();
			$cashgameResult.setPlayer(new Player());
			setupUser();
			$socialService = setupSocialService();
			sharingStorage.returns("getServices", array());

			$socialService.expectNever("shareResult");

			sut.shareSingleResult($cashgameResult);
		}

		function test_ShareSingleResult_UserHasOneService_ShareResultIsCalledOnce(){
			$player = setupPlayer();
			$cashgameResult = new CashgameResult();
			$cashgameResult.setPlayer($player);
			setupUser();
			$socialService = setupSocialService();
			sharingStorage.returns("getServices", array("service1"));

			$socialService.expectOnce("shareResult");

			sut.shareSingleResult($cashgameResult);
		}

		function test_ShareSingleResult_UserHasTwoServices_ShareResultIsCalledTwice(){
			$player = setupPlayer();
			$cashgameResult = new CashgameResult();
			$cashgameResult.setPlayer($player);
			setupUser();
			$socialService = setupSocialService();
			sharingStorage.returns("getServices", array("service1", "service2"));

			$socialService.expectCallCount("shareResult", 2);

			sut.shareSingleResult($cashgameResult);
		}

		function test_ShareResult_TwoUsersHasOneServiceEach_ShareResultCalledTwice(){
			$cashgame = new Cashgame();
			$result1 = new CashgameResult();
			$player1 = new Player();
			$result1.setPlayer($player1);
			$result2 = new CashgameResult();
			$player2 = new Player();
			$result2.setPlayer($player2);
			$cashgame.setResults(array($result1, $result2));
			setupPlayer();
			setupUser();
			$socialService = setupSocialService();
			sharingStorage.returns("getServices", array("service1"));

			$socialService.expectCallCount("shareResult", 2);

			sut.shareResult($cashgame);
		}

		function setupPlayer(){
			$player = new Player();
			playerRepository.returns("getPlayerById", $player);
			return $player;
		}

		function setupUser(){
			$user = new User();
			userStorage.returns("getUserByName", $user);
		}

		function setupSocialService(){
			$socialService = TestHelper::getFake('integration\Sharing\SocialService');
			socialServiceFactory.returns("makeSocialService", $socialService);
			return $socialService;
		}

	}

}