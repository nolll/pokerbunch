namespace tests\AppTests\Cashgame\Action{

	use core\Validation\Validator;
	use core\Validation\ValidatorFake;
	use DateTime;
	use app\Cashgame\Action\CashoutController;
	use entities\Cashgame;
	use entities\CashgameResult;
	use entities\Player;
	use entities\Homegame;
	use entities\GameStatus;
	use exceptions\AccessDeniedException;
	use core\ClassNames;
	use SimpleMock;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class CashoutControllerTests extends UnitTestCase {

		/** @var CashoutController */
		private $sut;
		private $userContext;
		/** @var SimpleMock */
		private $cashgameValidatorFactory;

		function setUp(){
			homegameRepositoryMock = getFakeHomegameRepository();
			cashgameRepositoryMock = getFakeCashgameRepository();
			playerRepositoryMock = getFakePlayerRepository();
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			$request = TestHelper::getFake(ClassNames::$Request);
			cashgameValidatorFactory = TestHelper::getFake(ClassNames::$CashgameValidatorFactory);
			sut = new CashoutController(userContext, homegameRepositoryMock, cashgameRepositoryMock, playerRepositoryMock, $request, cashgameValidatorFactory);
		}

		function test_ActionCashout_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requirePlayer');
			expectException();

			sut.action_cashout("homegame1", "Player 1");
		}

		function test_ActionCashout_WithPlayerRightsAndIsAnotherPlayer_ThrowsException(){
			$player = new Player();
			$player.setUserName('otherUser');

			$cashgame = new Cashgame();
			$cashgame.setStartTime(new DateTime());

			TestHelper::setupUserWithPlayerRights(userContext);
			homegameRepositoryMock.returns('getByName', new Homegame());
			playerRepositoryMock.returns('getByName', $player);
			cashgameRepositoryMock.returns('getRunning', $cashgame);

			expectException(new AccessDeniedException());

			sut.action_cashout("homegame1", "Player 1");
		}

		function test_ActionCashout_ReturnsCorrectModel(){
			$cashgame = new Cashgame();
			$cashgame.setStartTime(new DateTime());

			$player = new Player();
			$player.setUserName('user1');

			TestHelper::setupUserWithPlayerRights(userContext);
			setupValidCashoutValidator();
			homegameRepositoryMock.returns('getByName', new Homegame());
            cashgameRepositoryMock.returns('getRunning', $cashgame);
			playerRepositoryMock.returns('getByName', $player);

			$viewResult = sut.action_cashout("homegame1", "Player 1");

			assertIsA($viewResult.model, 'app\Cashgame\Action\CashoutModel');
		}

		function test_ActionCashoutPost_AddsCheckpointAndUpdatesResult(){
			$player = new Player();
			$player.setUserName('user1');
			$player.setId(1);

			$cashgame = new Cashgame();
			$cashgame.setStatus(GameStatus::running);
			$cashgameResult = new CashgameResult();
			$cashgameResult.setPlayer($player);
			$cashgame.setResults(array($cashgameResult));

			TestHelper::setupUserWithManagerRights(userContext);
			setupValidCashoutValidator();
			homegameRepositoryMock.returns('getByName', new Homegame());
			cashgameRepositoryMock.returns('getRunning', $cashgame);
			playerRepositoryMock.returns('getByName', $player);

			cashgameRepositoryMock.expectOnce("addCheckpoint");

			sut.action_cashout_post("homegame1", "Player 1");
		}

		function test_ActionCashoutPost_HasResultsThatAreNotCheckedOut_RedirectsToRunningGame(){
			$player = new Player();
			$player.setUserName('user1');
			$player.setId(1);

			$cashgame = new Cashgame();
			$cashgame.setStatus(GameStatus::running);
			$cashgameResult = new CashgameResult();
			$cashgameResult.setPlayer($player);
			$cashgame.setResults(array($cashgameResult));

			TestHelper::setupUserWithManagerRights(userContext);
			setupValidCashoutValidator();
			homegameRepositoryMock.returns('getByName', new Homegame());
			cashgameRepositoryMock.returns('getRunning', $cashgame);
			playerRepositoryMock.returns('getByName', $player);

			$urlModel = sut.action_cashout_post("homegame1", "Player 1");

			assertIsA($urlModel, 'app\Urls\RunningCashgameUrlModel');
		}

		public function test_ActionCashoutPost_WithInvalidForm_ReturnsModel(){
			$player = new Player();
			$player.setUserName('user1');
			$player.setId(1);

			$cashgame = new Cashgame();
			$cashgame.setStatus(GameStatus::running);
			$cashgameResult = new CashgameResult();
			$cashgameResult.setPlayer($player);
			$cashgame.setResults(array($cashgameResult));

			TestHelper::setupUserWithManagerRights(userContext);
			setupInvalidCashoutValidator();
			homegameRepositoryMock.returns('getByName', new Homegame());
			cashgameRepositoryMock.returns('getRunning', $cashgame);
			playerRepositoryMock.returns('getByName', $player);

			$viewResult = sut.action_cashout_post("homegame1", "Player 1");

			assertIsA($viewResult.model, 'app\Cashgame\Action\CashoutModel');
		}

		private function setupValidCashoutValidator(){
			$validator = new ValidatorFake(true);
			setupCashoutValidator($validator);
		}

		private function setupInvalidCashoutValidator(){
			$validator = new ValidatorFake(false);
			setupCashoutValidator($validator);
		}

		private function setupCashoutValidator(Validator $validator){
			cashgameValidatorFactory.returns("getCashoutValidator", $validator);
		}

	}

}