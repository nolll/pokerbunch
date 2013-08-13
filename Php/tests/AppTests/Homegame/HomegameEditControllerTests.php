<?php
namespace tests\AppTests\Homegame{

	use app\Homegame\Edit\HomegameEditController;
	use core\Validation\ValidatorFake;
	use entities\Homegame;
	use core\ClassNames;
	use tests\TestHelper;
	use core\Validation\Validator;
	use tests\UnitTestCase;

	class HomegameEditControllerTests extends UnitTestCase {

		/** @var HomegameEditController */
		private $sut;
		private $userContext;
		private $homegameStorage;
		private $homegameValidatorFactory;

		function setUp(){
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->homegameStorage = TestHelper::getFake(ClassNames::$HomegameStorage);
			$this->homegameRepositoryMock = $this->getFakeHomegameRepository();
			$this->homegameValidatorFactory = TestHelper::getFake(ClassNames::$HomegameValidatorFactory);
			$this->cashgameRepositoryMock = $this->getFakeCashgameRepository();
			$request = TestHelper::getFake(ClassNames::$Request);
			$this->sut = new HomegameEditController($this->userContext, $this->homegameStorage, $this->homegameRepositoryMock, $this->homegameValidatorFactory, $this->cashgameRepositoryMock, $request);
		}

		function test_ActionEdit_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requireManager');
			$this->expectException();

			$this->sut->action_edit("homegame1");
		}

		function test_ActionEditPost_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requireManager');
			$this->expectException();

			$this->sut->action_edit_post("homegame1");
		}

		function test_ActionEditPost_WithValidHomegame_CallsUpdateHomegame(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$this->setupValidValidator();

			$this->homegameStorage->expectOnce("updateHomegame");

			$this->sut->action_edit_post("homegame1");
		}

		function test_ActionEditPost_WithValidHomegame_RedirectsToDetails(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$this->setupValidValidator();

			$urlModel = $this->sut->action_edit_post("homegame1");

			$this->assertIsA($urlModel, 'app\Urls\HomegameDetailsUrlModel');
		}

		function test_ActionEditPost_WithInvalidHomegame_DoesNotRedirect(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$this->setupInvalidValidator();

			$urlModel = $this->sut->action_edit_post("homegame1");

			$this->assertNotA($urlModel, 'app\Urls\HomegameDetailsUrlModel');
		}

		function test_ActionEditPost_WithInvalidHomegame_DoesntCallUpdateHomegame(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$this->setupInvalidValidator();

			$this->homegameStorage->expectNever("updateHomegame");

			$this->sut->action_edit_post("homegame1");
		}

		function setupValidValidator(){
			$validator = new ValidatorFake(true);
			$this->setupValidator($validator);
		}

		function setupInvalidValidator(){
			$validator = new ValidatorFake(false);
			$this->setupValidator($validator);
		}

		function setupValidator(Validator $validator){
			$this->homegameValidatorFactory->returns("getEditHomegameValidator", $validator);
		}

	}

}