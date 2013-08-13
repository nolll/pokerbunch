namespace tests\AppTests\Cashgame\Matrix{

	use app\Cashgame\Matrix\MatrixController;
	use entities\Homegame;
	use core\ClassNames;
	use Domain\Classes\User;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class MatrixControllerTests extends UnitTestCase {

		/** @var MatrixController */
		private $sut;
		private $userContext;
		private $matrixModelFactory;

		function setUp(){
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->homegameRepositoryMock = $this->getFakeHomegameRepository();
			$this->matrixModelFactory = TestHelper::getFake(ClassNames::$MatrixModelFactory);
			$this->sut = new MatrixController($this->userContext, $this->homegameRepositoryMock, $this->matrixModelFactory);
		}

		function test_ActionMatrix_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requirePlayer');
			$this->expectException();

			$this->sut->action_matrix("homegame1");
		}

		function test_ActionMatrix_Authorized_ShowsCorrectView(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->returns('getUser', new User());

			$viewResult = $this->sut->action_matrix("homegame1");

			$this->assertEqual('app/Cashgame/Matrix/Matrix', $viewResult->view);
		}

	}

}