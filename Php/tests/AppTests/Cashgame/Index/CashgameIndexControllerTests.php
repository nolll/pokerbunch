<?php
namespace tests\AppTests\Cashgame\Index{

	use app\Cashgame\Index\CashgameIndexController;
	use core\Util;
	use entities\Homegame;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class CashgameIndexControllerTests extends UnitTestCase {

		/** @var CashgameIndexController */
		private $sut;
		private $userContext;

		function setUp(){
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->homegameRepositoryMock = $this->getFakeHomegameRepository();
			$this->cashgameRepositoryMock = $this->getFakeCashgameRepository();
			$this->sut = new CashgameIndexController($this->userContext, $this->cashgameRepositoryMock, $this->homegameRepositoryMock);
		}

		function test_ActionIndex_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requirePlayer');
			$this->expectException();

			$this->sut->action_index("homegame1");
		}

		function test_ActionAdd_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requirePlayer');
			$this->expectException();

			$this->sut->action_index("homegame1");
		}

		function test_ActionIndex_WithYears_RedirectsToMatrixWithLatestYearSelected(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithPlayerRights($this->userContext);
			$years = array(2011, 2010, 2009);
			$this->cashgameRepositoryMock->returns("getYears", $years);

			$urlModel = $this->sut->action_index("homegame1");

			$this->assertIsA($urlModel, 'app\Urls\CashgameMatrixUrlModel');
			$this->assertTrue(Util::endsWith($urlModel->url, '2011'));
		}

		function test_ActionIndex_NoYears_RedirectsToAddCashgame(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithPlayerRights($this->userContext);
			$years = array();
			$this->cashgameRepositoryMock->returns("getYears", $years);

			$urlModel = $this->sut->action_index("homegame1");

			$this->assertIsA($urlModel, 'app\Urls\CashgameAddUrlModel');
		}

	}

}