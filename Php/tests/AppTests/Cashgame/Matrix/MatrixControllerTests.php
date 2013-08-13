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
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			homegameRepositoryMock = getFakeHomegameRepository();
			matrixModelFactory = TestHelper::getFake(ClassNames::$MatrixModelFactory);
			sut = new MatrixController(userContext, homegameRepositoryMock, matrixModelFactory);
		}

		function test_ActionMatrix_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requirePlayer');
			expectException();

			sut.action_matrix("homegame1");
		}

		function test_ActionMatrix_Authorized_ShowsCorrectView(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.returns('getUser', new User());

			$viewResult = sut.action_matrix("homegame1");

			assertEqual('app/Cashgame/Matrix/Matrix', $viewResult.view);
		}

	}

}