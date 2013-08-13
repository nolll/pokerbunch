namespace tests\AppTests\Cashgame\Action{

	use app\Cashgame\Action\BuyinController;
	use core\Validation\Validator;
	use core\Validation\ValidatorFake;
	use entities\Cashgame;
	use entities\Homegame;
	use entities\GameStatus;
	use entities\Player;
	use exceptions\AccessDeniedException;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class BuyinControllerTests extends UnitTestCase {

		/** @var BuyinController */
		private $sut;
		private $userContext;
		private $cashgameValidatorFactory;

		public function setUp(){
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			homegameRepositoryMock = getFakeHomegameRepository();
			cashgameRepositoryMock = getFakeCashgameRepository();
			playerRepositoryMock = getFakePlayerRepository();
			$request = TestHelper::getFake(ClassNames::$Request);
			cashgameValidatorFactory = TestHelper::getFake(ClassNames::$CashgameValidatorFactory);
			sut = new BuyinController(userContext, homegameRepositoryMock, cashgameRepositoryMock, playerRepositoryMock, $request, cashgameValidatorFactory);
		}

		public function test_ActionBuyin_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requirePlayer');
			expectException();

			sut.action_buyin("homegame1", "Player 1");
		}

		public function test_ActionBuyin_WithPlayerRightsAndIsAnotherPlayer_ThrowsException(){
			$homegame = new Homegame();
			$cashgame = new Cashgame();
			homegameRepositoryMock.returns('getByName', $homegame);
			cashgameRepositoryMock.returns('getRunning', $cashgame);
			TestHelper::setupUserWithPlayerRights(userContext);
			$player = new Player();
			$player.setUserName('otherUser');
			playerRepositoryMock.returns('getByName', $player);

			expectException(new AccessDeniedException());

			sut.action_buyin("homegame1", "Player 1");
		}

		public function test_ActionBuyin_ReturnsModel(){
			homegameRepositoryMock.returns('getByName', new Homegame());
            TestHelper::setupUserWithPlayerRights(userContext);
            $cashgame = new Cashgame();
			cashgameRepositoryMock.returns('getRunning', $cashgame);
			$player = new Player();
			$player.setUserName('user1');
			playerRepositoryMock.returns('getByName', $player);

			$viewResult = sut.action_buyin("homegame1", "Player 1");

			assertIsA($viewResult.model, 'app\Cashgame\Action\BuyinModel');
		}

		public function test_ActionBuyinPost_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requirePlayer');
			expectException();

			sut.action_buyin_post("homegame1", "Player 1");
		}

		public function test_ActionBuyinPost_PlayerIsNotInGame_AddsCheckpoint(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			$cashgame = new Cashgame();
			cashgameRepositoryMock.returns('getRunning', $cashgame);
			$player = new Player();
			playerRepositoryMock.returns('getByName', $player);
			setupValidBuyinValidator();

			cashgameRepositoryMock.expectOnce("addCheckpoint");

			sut.action_buyin_post("homegame1", "Player 1");
		}

		public function test_ActionBuyinPost_GameIsNotStarted_StartsGame(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			$cashgame = new Cashgame();
			cashgameRepositoryMock.returns('getRunning', $cashgame);
			$player = new Player();
			playerRepositoryMock.returns('getByName', $player);
			setupValidBuyinValidator();

			cashgameRepositoryMock.expectOnce("startGame");

			sut.action_buyin_post("homegame1", "Player 1");
		}

		public function test_ActionBuyinPost_GameIsStarted_DoesNotStartGame(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			$cashgame = new Cashgame();
			$cashgame.setIsStarted(true);
			cashgameRepositoryMock.returns('getRunning', $cashgame);
			$player = new Player();
			playerRepositoryMock.returns('getByName', $player);
			setupValidBuyinValidator();

			cashgameRepositoryMock.expectNever("startGame");

			sut.action_buyin_post("homegame1", "Player 1");
		}

		public function test_ActionBuyinPost_RedirectsToRunningCashgame(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			$cashgame = new Cashgame();
			cashgameRepositoryMock.returns('getRunning', $cashgame);
			$player = new Player();
			playerRepositoryMock.returns('getByName', $player);
			setupValidBuyinValidator();

			$urlModel = sut.action_buyin_post("homegame1", "Player 1");

			assertIsA($urlModel, 'app\Urls\RunningCashgameUrlModel');
		}

		public function test_ActionBuyinPost_WithInvalidForm_ReturnsModel(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			$cashgame = new Cashgame();
			$cashgame.setIsStarted(true);
			cashgameRepositoryMock.returns('getRunning', $cashgame);
			$player = new Player();
			$player.setUserName('user1');
			playerRepositoryMock.returns('getByName', $player);
			setupInvalidBuyinValidator();

			$viewResult = sut.action_buyin_post("homegame1", "Player 1");

			assertIsA($viewResult.model, 'app\Cashgame\Action\BuyinModel');
		}

		private function setupValidBuyinValidator(){
			$validator = new ValidatorFake(true);
			setupBuyinValidator($validator);
		}

		private function setupInvalidBuyinValidator(){
			$validator = new ValidatorFake(false);
			setupBuyinValidator($validator);
		}

		private function setupBuyinValidator(Validator $validator){
			cashgameValidatorFactory.returns("getBuyinValidator", $validator);
		}

	}

}