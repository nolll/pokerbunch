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
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			homegameStorage = TestHelper::getFake(ClassNames::$HomegameStorage);
			playerRepository = TestHelper::getFake(ClassNames::$PlayerRepository);
			slugGenerator = TestHelper::getFake(ClassNames::$SlugGenerator);
			homegameValidatorFactory = TestHelper::getFake(ClassNames::$HomegameValidatorFactory);
			$request = TestHelper::getFake(ClassNames::$Request);
			sut = new HomegameAddController(userContext, homegameStorage, playerRepository, slugGenerator, homegameValidatorFactory, $request);
		}

		function test_ActionAdd_NotAuthorized_ThrowsException(){
			userContext.throwOn('requireUser');
			expectException();

			sut.action_add();
		}

		function test_ActionAddPost_NotAuthorized_ThrowsException(){
			userContext.throwOn('requireUser');
			expectException();

			sut.action_add_post();
		}

		function test_ActionAddPost_WithValidHomegame_AddsHomegame(){
			TestHelper::setupUser(userContext);
			setupValidValidator();
			$addedHomegame = new Homegame();
			homegameStorage.returns("addHomegame", $addedHomegame);

			homegameStorage.expectOnce("addHomegame");

			sut.action_add_post();
		}

		function test_ActionAddPost_WithValidHomegame_AddsPlayer(){
			TestHelper::setupUser(userContext);
			setupValidValidator();
			$addedHomegame = new Homegame();
			homegameStorage.returns("addHomegame", $addedHomegame);

			playerRepository.expectOnce("addPlayerWithUser");

			sut.action_add_post();
		}

		function test_ActionAddPost_WithValidHomegame_RedirectsToConfirmation(){
			TestHelper::setupUser(userContext);
			setupValidValidator();
			$addedHomegame = new Homegame();
			homegameStorage.returns("addHomegame", $addedHomegame);

			$urlModel = sut.action_add_post();

			assertIsA($urlModel, 'app\Urls\HomegameAddConfirmationUrlModel');
		}

		function test_ActionAddPost_WithInvalidHomegame_DoesntCallAddHomegame(){
			TestHelper::setupUser(userContext);
			setupInvalidValidator();

			homegameStorage.expectNever("addHomegame");

			sut.action_add_post();
		}

		function test_ActionAddPost_WithInvalidHomegame_ShowsForm(){
			TestHelper::setupUser(userContext);
			setupInvalidValidator();

			$viewResult = sut.action_add_post();

			assertIsA($viewResult.model, 'app\Homegame\Add\HomegameAddModel');
		}

		function test_ActionAddPost_GeneratesSlug(){
			TestHelper::setupUser(userContext);
			setupValidValidator();
			$addedHomegame = new Homegame();
			homegameStorage.returns("addHomegame", $addedHomegame);

			slugGenerator.expectOnce("getSlug");

			sut.action_add_post();
		}

		function setupValidValidator(){
			$validator = new ValidatorFake(true);
			setupValidator($validator);
		}

		function setupInvalidValidator(){
			$validator = new ValidatorFake(false);
			setupValidator($validator);
		}

		function setupValidator(Validator $validator){
			homegameValidatorFactory.returns("getAddHomegameValidator", $validator);
		}

	}

}