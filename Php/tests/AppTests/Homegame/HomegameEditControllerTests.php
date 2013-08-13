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
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			homegameStorage = TestHelper::getFake(ClassNames::$HomegameStorage);
			homegameRepositoryMock = getFakeHomegameRepository();
			homegameValidatorFactory = TestHelper::getFake(ClassNames::$HomegameValidatorFactory);
			cashgameRepositoryMock = getFakeCashgameRepository();
			$request = TestHelper::getFake(ClassNames::$Request);
			sut = new HomegameEditController(userContext, homegameStorage, homegameRepositoryMock, homegameValidatorFactory, cashgameRepositoryMock, $request);
		}

		function test_ActionEdit_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requireManager');
			expectException();

			sut.action_edit("homegame1");
		}

		function test_ActionEditPost_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requireManager');
			expectException();

			sut.action_edit_post("homegame1");
		}

		function test_ActionEditPost_WithValidHomegame_CallsUpdateHomegame(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			setupValidValidator();

			homegameStorage.expectOnce("updateHomegame");

			sut.action_edit_post("homegame1");
		}

		function test_ActionEditPost_WithValidHomegame_RedirectsToDetails(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			setupValidValidator();

			$urlModel = sut.action_edit_post("homegame1");

			assertIsA($urlModel, 'app\Urls\HomegameDetailsUrlModel');
		}

		function test_ActionEditPost_WithInvalidHomegame_DoesNotRedirect(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			setupInvalidValidator();

			$urlModel = sut.action_edit_post("homegame1");

			assertNotA($urlModel, 'app\Urls\HomegameDetailsUrlModel');
		}

		function test_ActionEditPost_WithInvalidHomegame_DoesntCallUpdateHomegame(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			setupInvalidValidator();

			homegameStorage.expectNever("updateHomegame");

			sut.action_edit_post("homegame1");
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
			homegameValidatorFactory.returns("getEditHomegameValidator", $validator);
		}

	}

}