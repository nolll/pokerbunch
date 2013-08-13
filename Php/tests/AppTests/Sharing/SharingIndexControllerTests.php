<?php
namespace tests\AppTests\Sharing{

	use app\Sharing\Index\SharingIndexController;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class SharingIndexControllerTests extends UnitTestCase {

		/** @var SharingIndexController */
		private $sut;
		private $userContext;

		function setUp(){
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$sharingStorage = TestHelper::getFake(ClassNames::$SharingStorage);
			$this->sut = new SharingIndexController($this->userContext, $sharingStorage);
		}

		function test_ActionIndex_NotAuthorized_ThrowsException(){
			$this->userContext->throwOn('requireUser');
			$this->expectException();

			$this->sut->action_index("homegame1");
		}

	}

}