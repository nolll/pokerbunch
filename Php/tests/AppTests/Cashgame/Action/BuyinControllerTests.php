<?php
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
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->homegameRepositoryMock = $this->getFakeHomegameRepository();
			$this->cashgameRepositoryMock = $this->getFakeCashgameRepository();
			$this->playerRepositoryMock = $this->getFakePlayerRepository();
			$request = TestHelper::getFake(ClassNames::$Request);
			$this->cashgameValidatorFactory = TestHelper::getFake(ClassNames::$CashgameValidatorFactory);
			$this->sut = new BuyinController($this->userContext, $this->homegameRepositoryMock, $this->cashgameRepositoryMock, $this->playerRepositoryMock, $request, $this->cashgameValidatorFactory);
		}

		public function test_ActionBuyin_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requirePlayer');
			$this->expectException();

			$this->sut->action_buyin("homegame1", "Player 1");
		}

		public function test_ActionBuyin_WithPlayerRightsAndIsAnotherPlayer_ThrowsException(){
			$homegame = new Homegame();
			$cashgame = new Cashgame();
			$this->homegameRepositoryMock->returns('getByName', $homegame);
			$this->cashgameRepositoryMock->returns('getRunning', $cashgame);
			TestHelper::setupUserWithPlayerRights($this->userContext);
			$player = new Player();
			$player->setUserName('otherUser');
			$this->playerRepositoryMock->returns('getByName', $player);

			$this->expectException(new AccessDeniedException());

			$this->sut->action_buyin("homegame1", "Player 1");
		}

		public function test_ActionBuyin_ReturnsModel(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
            TestHelper::setupUserWithPlayerRights($this->userContext);
            $cashgame = new Cashgame();
			$this->cashgameRepositoryMock->returns('getRunning', $cashgame);
			$player = new Player();
			$player->setUserName('user1');
			$this->playerRepositoryMock->returns('getByName', $player);

			$viewResult = $this->sut->action_buyin("homegame1", "Player 1");

			$this->assertIsA($viewResult->model, 'app\Cashgame\Action\BuyinModel');
		}

		public function test_ActionBuyinPost_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requirePlayer');
			$this->expectException();

			$this->sut->action_buyin_post("homegame1", "Player 1");
		}

		public function test_ActionBuyinPost_PlayerIsNotInGame_AddsCheckpoint(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$cashgame = new Cashgame();
			$this->cashgameRepositoryMock->returns('getRunning', $cashgame);
			$player = new Player();
			$this->playerRepositoryMock->returns('getByName', $player);
			$this->setupValidBuyinValidator();

			$this->cashgameRepositoryMock->expectOnce("addCheckpoint");

			$this->sut->action_buyin_post("homegame1", "Player 1");
		}

		public function test_ActionBuyinPost_GameIsNotStarted_StartsGame(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$cashgame = new Cashgame();
			$this->cashgameRepositoryMock->returns('getRunning', $cashgame);
			$player = new Player();
			$this->playerRepositoryMock->returns('getByName', $player);
			$this->setupValidBuyinValidator();

			$this->cashgameRepositoryMock->expectOnce("startGame");

			$this->sut->action_buyin_post("homegame1", "Player 1");
		}

		public function test_ActionBuyinPost_GameIsStarted_DoesNotStartGame(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$cashgame = new Cashgame();
			$cashgame->setIsStarted(true);
			$this->cashgameRepositoryMock->returns('getRunning', $cashgame);
			$player = new Player();
			$this->playerRepositoryMock->returns('getByName', $player);
			$this->setupValidBuyinValidator();

			$this->cashgameRepositoryMock->expectNever("startGame");

			$this->sut->action_buyin_post("homegame1", "Player 1");
		}

		public function test_ActionBuyinPost_RedirectsToRunningCashgame(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$cashgame = new Cashgame();
			$this->cashgameRepositoryMock->returns('getRunning', $cashgame);
			$player = new Player();
			$this->playerRepositoryMock->returns('getByName', $player);
			$this->setupValidBuyinValidator();

			$urlModel = $this->sut->action_buyin_post("homegame1", "Player 1");

			$this->assertIsA($urlModel, 'app\Urls\RunningCashgameUrlModel');
		}

		public function test_ActionBuyinPost_WithInvalidForm_ReturnsModel(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$cashgame = new Cashgame();
			$cashgame->setIsStarted(true);
			$this->cashgameRepositoryMock->returns('getRunning', $cashgame);
			$player = new Player();
			$player->setUserName('user1');
			$this->playerRepositoryMock->returns('getByName', $player);
			$this->setupInvalidBuyinValidator();

			$viewResult = $this->sut->action_buyin_post("homegame1", "Player 1");

			$this->assertIsA($viewResult->model, 'app\Cashgame\Action\BuyinModel');
		}

		private function setupValidBuyinValidator(){
			$validator = new ValidatorFake(true);
			$this->setupBuyinValidator($validator);
		}

		private function setupInvalidBuyinValidator(){
			$validator = new ValidatorFake(false);
			$this->setupBuyinValidator($validator);
		}

		private function setupBuyinValidator(Validator $validator){
			$this->cashgameValidatorFactory->returns("getBuyinValidator", $validator);
		}

	}

}