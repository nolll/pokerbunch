<?php
namespace tests\AppTests\Cashgame\Listing{

	use app\Cashgame\Listing\CashgameListingController;
	use entities\Homegame;
	use entities\GameStatus;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class CashgameListingControllerTests extends UnitTestCase {

		/** @var CashgameListingController */
		private $sut;
		private $userContext;

		function setUp(){
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->homegameRepositoryMock = $this->getFakeHomegameRepository();
			$this->cashgameRepositoryMock = $this->getFakeCashgameRepository();
			$this->sut = new CashgameListingController($this->userContext, $this->homegameRepositoryMock, $this->cashgameRepositoryMock);
		}

		function test_ActionListing_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requirePlayer');
			$this->expectException();

			$this->sut->action_listing("homegame1");
		}

		function test_ActionListing_ReturnsCorrectModel(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithPlayerRights($this->userContext);
			$this->cashgameRepositoryMock->returns('getAll', array());

			$viewResult = $this->sut->action_listing("homegame1");

			$this->assertIsA($viewResult->model, 'app\Cashgame\Listing\CashgameListingModel');
		}

	}

}