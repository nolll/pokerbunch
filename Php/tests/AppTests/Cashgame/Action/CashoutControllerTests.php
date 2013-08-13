<?php
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
			$this->homegameRepositoryMock = $this->getFakeHomegameRepository();
			$this->cashgameRepositoryMock = $this->getFakeCashgameRepository();
			$this->playerRepositoryMock = $this->getFakePlayerRepository();
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$request = TestHelper::getFake(ClassNames::$Request);
			$this->cashgameValidatorFactory = TestHelper::getFake(ClassNames::$CashgameValidatorFactory);
			$this->sut = new CashoutController($this->userContext, $this->homegameRepositoryMock, $this->cashgameRepositoryMock, $this->playerRepositoryMock, $request, $this->cashgameValidatorFactory);
		}

		function test_ActionCashout_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requirePlayer');
			$this->expectException();

			$this->sut->action_cashout("homegame1", "Player 1");
		}

		function test_ActionCashout_WithPlayerRightsAndIsAnotherPlayer_ThrowsException(){
			$player = new Player();
			$player->setUserName('otherUser');

			$cashgame = new Cashgame();
			$cashgame->setStartTime(new DateTime());

			TestHelper::setupUserWithPlayerRights($this->userContext);
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->playerRepositoryMock->returns('getByName', $player);
			$this->cashgameRepositoryMock->returns('getRunning', $cashgame);

			$this->expectException(new AccessDeniedException());

			$this->sut->action_cashout("homegame1", "Player 1");
		}

		function test_ActionCashout_ReturnsCorrectModel(){
			$cashgame = new Cashgame();
			$cashgame->setStartTime(new DateTime());

			$player = new Player();
			$player->setUserName('user1');

			TestHelper::setupUserWithPlayerRights($this->userContext);
			$this->setupValidCashoutValidator();
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
            $this->cashgameRepositoryMock->returns('getRunning', $cashgame);
			$this->playerRepositoryMock->returns('getByName', $player);

			$viewResult = $this->sut->action_cashout("homegame1", "Player 1");

			$this->assertIsA($viewResult->model, 'app\Cashgame\Action\CashoutModel');
		}

		function test_ActionCashoutPost_AddsCheckpointAndUpdatesResult(){
			$player = new Player();
			$player->setUserName('user1');
			$player->setId(1);

			$cashgame = new Cashgame();
			$cashgame->setStatus(GameStatus::running);
			$cashgameResult = new CashgameResult();
			$cashgameResult->setPlayer($player);
			$cashgame->setResults(array($cashgameResult));

			TestHelper::setupUserWithManagerRights($this->userContext);
			$this->setupValidCashoutValidator();
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->cashgameRepositoryMock->returns('getRunning', $cashgame);
			$this->playerRepositoryMock->returns('getByName', $player);

			$this->cashgameRepositoryMock->expectOnce("addCheckpoint");

			$this->sut->action_cashout_post("homegame1", "Player 1");
		}

		function test_ActionCashoutPost_HasResultsThatAreNotCheckedOut_RedirectsToRunningGame(){
			$player = new Player();
			$player->setUserName('user1');
			$player->setId(1);

			$cashgame = new Cashgame();
			$cashgame->setStatus(GameStatus::running);
			$cashgameResult = new CashgameResult();
			$cashgameResult->setPlayer($player);
			$cashgame->setResults(array($cashgameResult));

			TestHelper::setupUserWithManagerRights($this->userContext);
			$this->setupValidCashoutValidator();
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->cashgameRepositoryMock->returns('getRunning', $cashgame);
			$this->playerRepositoryMock->returns('getByName', $player);

			$urlModel = $this->sut->action_cashout_post("homegame1", "Player 1");

			$this->assertIsA($urlModel, 'app\Urls\RunningCashgameUrlModel');
		}

		public function test_ActionCashoutPost_WithInvalidForm_ReturnsModel(){
			$player = new Player();
			$player->setUserName('user1');
			$player->setId(1);

			$cashgame = new Cashgame();
			$cashgame->setStatus(GameStatus::running);
			$cashgameResult = new CashgameResult();
			$cashgameResult->setPlayer($player);
			$cashgame->setResults(array($cashgameResult));

			TestHelper::setupUserWithManagerRights($this->userContext);
			$this->setupInvalidCashoutValidator();
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->cashgameRepositoryMock->returns('getRunning', $cashgame);
			$this->playerRepositoryMock->returns('getByName', $player);

			$viewResult = $this->sut->action_cashout_post("homegame1", "Player 1");

			$this->assertIsA($viewResult->model, 'app\Cashgame\Action\CashoutModel');
		}

		private function setupValidCashoutValidator(){
			$validator = new ValidatorFake(true);
			$this->setupCashoutValidator($validator);
		}

		private function setupInvalidCashoutValidator(){
			$validator = new ValidatorFake(false);
			$this->setupCashoutValidator($validator);
		}

		private function setupCashoutValidator(Validator $validator){
			$this->cashgameValidatorFactory->returns("getCashoutValidator", $validator);
		}

	}

}