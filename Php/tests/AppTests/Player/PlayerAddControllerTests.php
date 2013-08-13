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
			$this->playerRepositoryMock = $this->getFakePlayerRepository();
			$this->homegameRepositoryMock = $this->getFakeHomegameRepository();
			$this->cashgameRepositoryMock = $this->getFakeCashgameRepository();
			$this->playerValidatorFactory = TestHelper::getFake(ClassNames::$PlayerValidatorFactory);
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$request = TestHelper::getFake(ClassNames::$Request);
			$this->playerFactory = TestHelper::getFake(ClassNames::$PlayerFactory);
			$this->sut = new PlayerAddController($this->userContext, $this->playerRepositoryMock, $this->homegameRepositoryMock, $this->cashgameRepositoryMock, $this->playerValidatorFactory, $request, $this->playerFactory);
		}

		function test_ActionAdd_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requireManager');
			$this->expectException();

			$this->sut->action_add("homegame1");
		}

		function test_ActionAddPost_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requireManager');
			$this->expectException();

			$this->sut->action_add_post("homegame1");
		}

		function test_ActionAddPost_WithValidPlayer_AddsPlayer(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->playerFactory->returns('create', new Player());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$this->setupValidValidator();

			$this->playerRepositoryMock->expectOnce("addPlayer");

			$this->sut->action_add_post("homegame1");
		}

		function test_ActionAddPost_WithValidPlayer_RedirectsToConfirmation(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->playerFactory->returns('create', new Player());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$this->setupValidValidator();

			$urlModel = $this->sut->action_add_post("homegame1");

			$this->assertIsA($urlModel, 'app\Urls\PlayerAddConfirmationUrlModel');
		}

		function test_ActionAddPost_WithInvalidPlayer_DoesntAddPlayer(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->playerFactory->returns('create', new Player());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$this->setupInvalidValidator();

			$this->playerRepositoryMock->expectNever("addPlayer");

			$this->sut->action_add_post("homegame1");
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
			$this->playerValidatorFactory->returns("getAddPlayerValidator", $validator);
		}

	}

}