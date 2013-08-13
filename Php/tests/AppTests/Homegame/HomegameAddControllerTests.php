<?php
namespace tests\AppTests\Homegame{

	use app\Homegame\Add\HomegameAddController;
	use core\ClassNames;
	use core\Validation\ValidatorFake;
	use tests\TestHelper;
	use core\Validation\Validator;
	use entities\Homegame;
	use tests\UnitTestCase;

	class HomegameAddControllerTests extends UnitTestCase {

		/** @var HomegameAddController */
		private $sut;
		private $userContext;
		private $playerRepository;
		private $homegameStorage;
		private $slugGenerator;
		private $homegameValidatorFactory;

		function setUp(){
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->homegameStorage = TestHelper::getFake(ClassNames::$HomegameStorage);
			$this->playerRepository = TestHelper::getFake(ClassNames::$PlayerRepository);
			$this->slugGenerator = TestHelper::getFake(ClassNames::$SlugGenerator);
			$this->homegameValidatorFactory = TestHelper::getFake(ClassNames::$HomegameValidatorFactory);
			$request = TestHelper::getFake(ClassNames::$Request);
			$this->sut = new HomegameAddController($this->userContext, $this->homegameStorage, $this->playerRepository, $this->slugGenerator, $this->homegameValidatorFactory, $request);
		}

		function test_ActionAdd_NotAuthorized_ThrowsException(){
			$this->userContext->throwOn('requireUser');
			$this->expectException();

			$this->sut->action_add();
		}

		function test_ActionAddPost_NotAuthorized_ThrowsException(){
			$this->userContext->throwOn('requireUser');
			$this->expectException();

			$this->sut->action_add_post();
		}

		function test_ActionAddPost_WithValidHomegame_AddsHomegame(){
			TestHelper::setupUser($this->userContext);
			$this->setupValidValidator();
			$addedHomegame = new Homegame();
			$this->homegameStorage->returns("addHomegame", $addedHomegame);

			$this->homegameStorage->expectOnce("addHomegame");

			$this->sut->action_add_post();
		}

		function test_ActionAddPost_WithValidHomegame_AddsPlayer(){
			TestHelper::setupUser($this->userContext);
			$this->setupValidValidator();
			$addedHomegame = new Homegame();
			$this->homegameStorage->returns("addHomegame", $addedHomegame);

			$this->playerRepository->expectOnce("addPlayerWithUser");

			$this->sut->action_add_post();
		}

		function test_ActionAddPost_WithValidHomegame_RedirectsToConfirmation(){
			TestHelper::setupUser($this->userContext);
			$this->setupValidValidator();
			$addedHomegame = new Homegame();
			$this->homegameStorage->returns("addHomegame", $addedHomegame);

			$urlModel = $this->sut->action_add_post();

			$this->assertIsA($urlModel, 'app\Urls\HomegameAddConfirmationUrlModel');
		}

		function test_ActionAddPost_WithInvalidHomegame_DoesntCallAddHomegame(){
			TestHelper::setupUser($this->userContext);
			$this->setupInvalidValidator();

			$this->homegameStorage->expectNever("addHomegame");

			$this->sut->action_add_post();
		}

		function test_ActionAddPost_WithInvalidHomegame_ShowsForm(){
			TestHelper::setupUser($this->userContext);
			$this->setupInvalidValidator();

			$viewResult = $this->sut->action_add_post();

			$this->assertIsA($viewResult->model, 'app\Homegame\Add\HomegameAddModel');
		}

		function test_ActionAddPost_GeneratesSlug(){
			TestHelper::setupUser($this->userContext);
			$this->setupValidValidator();
			$addedHomegame = new Homegame();
			$this->homegameStorage->returns("addHomegame", $addedHomegame);

			$this->slugGenerator->expectOnce("getSlug");

			$this->sut->action_add_post();
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
			$this->homegameValidatorFactory->returns("getAddHomegameValidator", $validator);
		}

	}

}