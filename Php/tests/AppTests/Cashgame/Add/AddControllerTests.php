namespace tests\AppTests\Cashgame\Add{

	use app\Cashgame\Add\AddController;
	use core\Validation\ValidatorFake;
	use entities\Cashgame;
	use entities\Homegame;
	use exceptions\AccessDeniedException;
	use core\ClassNames;
	use tests\TestHelper;
	use core\Validation\Validator;
	use tests\UnitTestCase;

	class AddControllerTests extends UnitTestCase {

		/** @var AddController */
		private $sut;
		private $userContext;
		private $cashgameValidatorFactory;
		private $cashgameFactory;

		function setUp(){
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			homegameRepositoryMock = getFakeHomegameRepository();
			cashgameRepositoryMock = getFakeCashgameRepository();
			cashgameValidatorFactory = TestHelper::getFake(ClassNames::$CashgameValidatorFactory);
			$request = TestHelper::getFake(ClassNames::$Request);
			cashgameFactory = TestHelper::getFake(ClassNames::$CashgameFactory);
			sut = new AddController(userContext, homegameRepositoryMock, cashgameRepositoryMock, cashgameValidatorFactory, $request, cashgameFactory);
		}

		function test_ActionAdd_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requirePlayer');
			expectException();

			sut.action_add("homegame1");
		}

		function test_ActionAdd_WithRunningGame_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithPlayerRights(userContext);
			cashgameRepositoryMock.returns('getRunning', new Cashgame());
			expectException(new AccessDeniedException('Game already running'));

			sut.action_add("homegame1");
		}

		function test_ShowForm_WithCashgame_ReturnsCorrectModel(){
			TestHelper::setupNullUser(userContext);
			cashgameRepositoryMock.returns('getLocations', array());
			$homegame = new Homegame();
			$cashgame = new Cashgame();
			$cashgame.setLocation('location');

			$viewResult = sut.showForm($homegame, $cashgame);

			assertIsA($viewResult.model, 'app\Cashgame\Add\AddModel');
		}

		function test_ActionAddPost_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requirePlayer');
			expectException();

			sut.action_add_post("homegame1");
		}

		function test_ActionAddPost_WithValidValues_CallsAddCashgame(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			cashgameFactory.returns('create', new Cashgame());
			TestHelper::setupUserWithManagerRights(userContext);
			setupValidCashgameValidator();
			cashgameRepositoryMock.expectOnce("addGame");

			sut.action_add_post("homegame1");
		}

		function test_ActionAddPost_WithValidCashgame_RedirectsToRunningGame(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			cashgameFactory.returns('create', new Cashgame());
			TestHelper::setupUserWithManagerRights(userContext);
			setupValidCashgameValidator();

			$urlModel = sut.action_add_post("homegame1");

			assertIsA($urlModel, 'app\Urls\RunningCashgameUrlModel');
		}

		function setupValidCashgameValidator(){
			$validator = new ValidatorFake(true);
			setupCashgameValidator($validator);
		}

		function setupInvalidCashgameValidator(){
			$validator = new ValidatorFake(false);
			setupCashgameValidator($validator);
		}

		function setupCashgameValidator(Validator $validator){
			cashgameValidatorFactory.returns("getAddCashgameValidator", $validator);
		}

	}

}