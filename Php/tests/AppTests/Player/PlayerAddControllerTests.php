namespace tests\AppTests\Player{

	use app\Player\Add\PlayerAddController;
	use core\Validation\ValidatorFake;
	use entities\Homegame;
	use core\ClassNames;
	use entities\Player;
	use tests\TestHelper;
	use core\Validation\Validator;
	use tests\UnitTestCase;

	class PlayerAddControllerTests extends UnitTestCase {

		/** @var PlayerAddController */
		private $sut;
		private $userContext;
		private $playerValidatorFactory;
		private $playerFactory;

		function setUp(){
			playerRepositoryMock = getFakePlayerRepository();
			homegameRepositoryMock = getFakeHomegameRepository();
			cashgameRepositoryMock = getFakeCashgameRepository();
			playerValidatorFactory = TestHelper::getFake(ClassNames::$PlayerValidatorFactory);
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			$request = TestHelper::getFake(ClassNames::$Request);
			playerFactory = TestHelper::getFake(ClassNames::$PlayerFactory);
			sut = new PlayerAddController(userContext, playerRepositoryMock, homegameRepositoryMock, cashgameRepositoryMock, playerValidatorFactory, $request, playerFactory);
		}

		function test_ActionAdd_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requireManager');
			expectException();

			sut.action_add("homegame1");
		}

		function test_ActionAddPost_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requireManager');
			expectException();

			sut.action_add_post("homegame1");
		}

		function test_ActionAddPost_WithValidPlayer_AddsPlayer(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			playerFactory.returns('create', new Player());
			TestHelper::setupUserWithManagerRights(userContext);
			setupValidValidator();

			playerRepositoryMock.expectOnce("addPlayer");

			sut.action_add_post("homegame1");
		}

		function test_ActionAddPost_WithValidPlayer_RedirectsToConfirmation(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			playerFactory.returns('create', new Player());
			TestHelper::setupUserWithManagerRights(userContext);
			setupValidValidator();

			$urlModel = sut.action_add_post("homegame1");

			assertIsA($urlModel, 'app\Urls\PlayerAddConfirmationUrlModel');
		}

		function test_ActionAddPost_WithInvalidPlayer_DoesntAddPlayer(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			playerFactory.returns('create', new Player());
			TestHelper::setupUserWithManagerRights(userContext);
			setupInvalidValidator();

			playerRepositoryMock.expectNever("addPlayer");

			sut.action_add_post("homegame1");
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
			playerValidatorFactory.returns("getAddPlayerValidator", $validator);
		}

	}

}