<?php
namespace tests\AppTests\Player{

	use app\Player\Listing\PlayerListingController;
	use entities\Homegame;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class PlayerListingControllerTests extends UnitTestCase {

		/** @var PlayerListingController */
		private $sut;
		private $userContext;

		function setUp(){
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->homegameRepositoryMock = $this->getFakeHomegameRepository();
			$this->playerRepositoryMock = $this->getFakePlayerRepository();
			$this->cashgameRepositoryMock = $this->getFakeCashgameRepository();
			$this->sut = new PlayerListingController($this->userContext, $this->homegameRepositoryMock, $this->playerRepositoryMock, $this->cashgameRepositoryMock);
		}

		function test_ActionIndex_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requirePlayer');
			$this->expectException();

			$this->sut->action_index("homegame1");
		}

	}

}