<?php
namespace tests\AppTests\Homegame{

	use app\Homegame\Details\HomegameDetailsController;
	use entities\Homegame;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class HomegameDetailsControllerTests extends UnitTestCase {

		/** @var HomegameDetailsController */
		private $sut;
		private $userContext;

		function setUp(){
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->cashgameRepositoryMock = $this->getFakeCashgameRepository();
			$this->homegameRepositoryMock = $this->getFakeHomegameRepository();
			$this->sut = new HomegameDetailsController($this->userContext, $this->cashgameRepositoryMock, $this->homegameRepositoryMock);
		}

		function test_ActionDetails_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requirePlayer');
			$this->expectException();

			$this->sut->action_details('any');
		}

	}

}