<?php
namespace tests\AppTests\Cashgame\Action{

	use app\Cashgame\Action\ReportController;
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

	class ReportControllerTests extends UnitTestCase {

		/** @var ReportController */
		private $sut;
		private $userContext;
		private $cashgameValidatorFactory;

		function setUp(){
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->homegameRepositoryMock = $this->getFakeHomegameRepository();
			$this->cashgameRepositoryMock = $this->getFakeCashgameRepository();
			$this->playerRepositoryMock = $this->getFakePlayerRepository();
			$request = TestHelper::getFake(ClassNames::$Request);
			$this->cashgameValidatorFactory = TestHelper::getFake(ClassNames::$CashgameValidatorFactory);
			$this->sut = new ReportController($this->userContext, $this->homegameRepositoryMock, $this->cashgameRepositoryMock, $this->playerRepositoryMock, $request, $this->cashgameValidatorFactory);
		}

		function test_ActionReport_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requirePlayer');
			$this->expectException();

			$this->sut->action_report("homegame1", "Player 1");
		}

		function test_ActionReport_WithPlayerRightsAndIsAnotherPlayer_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithPlayerRights($this->userContext);
			$player = new Player();
			$player->setUserName('otherUser');
			$this->playerRepositoryMock->returns('getByName', $player);

			$this->expectException(new AccessDeniedException());

			$this->sut->action_report("homegame1", "Player 1");
		}

		function test_ActionReport_ReturnsCorrectModel(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
            TestHelper::setupUserWithPlayerRights($this->userContext);
            $cashgame = new Cashgame();
			$cashgame->setStatus(GameStatus::running);
			$this->cashgameRepositoryMock->returns('getRunning', $cashgame);
			$player = new Player();
			$player->setUserName('user1');
			$this->playerRepositoryMock->returns('getByName', $player);

			$viewResult = $this->sut->action_report("homegame1", "Player 1");

			$this->assertIsA($viewResult->model, 'app\Cashgame\Action\ReportModel');
		}

		function test_ActionReportPost_AddsCheckpoint(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$cashgame = new Cashgame();
			$cashgame->setStatus(GameStatus::running);
			$this->cashgameRepositoryMock->returns('getRunning', $cashgame);
			$player = new Player();
			$this->playerRepositoryMock->returns('getByName', $player);
			$this->setupValidReportValidator();

			$this->cashgameRepositoryMock->expectOnce("addCheckpoint");

			$this->sut->action_report_post("homegame1", "2010-01-01", "Player 1");
		}

		function test_ActionReportPost_RedirectsToRunningCashgame(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$cashgame = new Cashgame();
			$cashgame->setStatus(GameStatus::running);
			$this->cashgameRepositoryMock->returns('getRunning', $cashgame);
			$player = new Player();
			$this->playerRepositoryMock->returns('getByName', $player);
			$this->setupValidReportValidator();

			$urlModel = $this->sut->action_report_post("homegame1", "Player 1");

			$this->assertIsA($urlModel, 'app\Urls\RunningCashgameUrlModel');
		}

		private function setupValidReportValidator(){
			$validator = new ValidatorFake(true);
			$this->setupReportValidator($validator);
		}

		private function setupReportValidator(Validator $validator){
			$this->cashgameValidatorFactory->returns("getReportValidator", $validator);
		}

	}

}