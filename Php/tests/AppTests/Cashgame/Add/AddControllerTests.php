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
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->homegameRepositoryMock = $this->getFakeHomegameRepository();
			$this->cashgameRepositoryMock = $this->getFakeCashgameRepository();
			$this->cashgameValidatorFactory = TestHelper::getFake(ClassNames::$CashgameValidatorFactory);
			$request = TestHelper::getFake(ClassNames::$Request);
			$this->cashgameFactory = TestHelper::getFake(ClassNames::$CashgameFactory);
			$this->sut = new AddController($this->userContext, $this->homegameRepositoryMock, $this->cashgameRepositoryMock, $this->cashgameValidatorFactory, $request, $this->cashgameFactory);
		}

		function test_ActionAdd_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requirePlayer');
			$this->expectException();

			$this->sut->action_add("homegame1");
		}

		function test_ActionAdd_WithRunningGame_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithPlayerRights($this->userContext);
			$this->cashgameRepositoryMock->returns('getRunning', new Cashgame());
			$this->expectException(new AccessDeniedException('Game already running'));

			$this->sut->action_add("homegame1");
		}

		function test_ShowForm_WithCashgame_ReturnsCorrectModel(){
			TestHelper::setupNullUser($this->userContext);
			$this->cashgameRepositoryMock->returns('getLocations', array());
			$homegame = new Homegame();
			$cashgame = new Cashgame();
			$cashgame->setLocation('location');

			$viewResult = $this->sut->showForm($homegame, $cashgame);

			$this->assertIsA($viewResult->model, 'app\Cashgame\Add\AddModel');
		}

		function test_ActionAddPost_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requirePlayer');
			$this->expectException();

			$this->sut->action_add_post("homegame1");
		}

		function test_ActionAddPost_WithValidValues_CallsAddCashgame(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->cashgameFactory->returns('create', new Cashgame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$this->setupValidCashgameValidator();
			$this->cashgameRepositoryMock->expectOnce("addGame");

			$this->sut->action_add_post("homegame1");
		}

		function test_ActionAddPost_WithValidCashgame_RedirectsToRunningGame(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->cashgameFactory->returns('create', new Cashgame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$this->setupValidCashgameValidator();

			$urlModel = $this->sut->action_add_post("homegame1");

			$this->assertIsA($urlModel, 'app\Urls\RunningCashgameUrlModel');
		}

		function setupValidCashgameValidator(){
			$validator = new ValidatorFake(true);
			$this->setupCashgameValidator($validator);
		}

		function setupInvalidCashgameValidator(){
			$validator = new ValidatorFake(false);
			$this->setupCashgameValidator($validator);
		}

		function setupCashgameValidator(Validator $validator){
			$this->cashgameValidatorFactory->returns("getAddCashgameValidator", $validator);
		}

	}

}