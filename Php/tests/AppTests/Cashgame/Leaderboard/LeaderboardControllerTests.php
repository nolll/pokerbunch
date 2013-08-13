<?php
namespace tests\AppTests\Cashgame\Leaderboard{

	use app\Cashgame\Leaderboard\LeaderboardController;
	use entities\CashgameSuite;
	use entities\Homegame;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class LeaderboardControllerTests extends UnitTestCase {

		/** @var LeaderboardController */
		private $sut;
		private $userContext;

		function setUp(){
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->homegameRepositoryMock = $this->getFakeHomegameRepository();
			$this->cashgameRepositoryMock = $this->getFakeCashgameRepository();
			$this->sut = new LeaderboardController($this->userContext, $this->homegameRepositoryMock, $this->cashgameRepositoryMock);
		}

		function test_ActionLeaderboard_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requirePlayer');
			$this->expectException();

			$this->sut->action_leaderboard("homegame1");
		}

		function test_ActionLeaderboard_SetsTableModel(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithPlayerRights($this->userContext);
			$this->cashgameRepositoryMock->returns('getSuite', new CashgameSuite());

			$viewResult = $this->sut->action_leaderboard("homegame1");

			$this->assertIsA($viewResult->model->tableModel, 'app\Cashgame\Leaderboard\TableModel');
		}

	}

}