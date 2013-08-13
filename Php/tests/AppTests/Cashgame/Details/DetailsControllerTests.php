namespace tests\AppTests\Cashgame\Details{

	use app\Cashgame\Details\DetailsController;
	use entities\Cashgame;
	use entities\Homegame;
	use DateTime;
	use core\ClassNames;
	use entities\Player;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class DetailsControllerTests extends UnitTestCase {

		/** @var DetailsController */
		private $sut;
		private $userContext;
		private $cashgameRepository;
		private $homegameRepository;
		private $playerRepository;

		function setUp(){
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->cashgameRepository = TestHelper::getFake(ClassNames::$CashgameRepository);
			$this->homegameRepository = TestHelper::getFake(ClassNames::$HomegameRepository);
			$this->playerRepository = TestHelper::getFake(ClassNames::$PlayerRepository);
			$resultSharer = TestHelper::getFake(ClassNames::$ResultSharer);
			$this->sut = new DetailsController($this->userContext, $this->homegameRepository, $this->cashgameRepository, $this->playerRepository, $resultSharer);
		}

		function test_ActionDetails_NotAuthorized_ThrowsException(){
			$this->userContext->throwOn('requirePlayer');
			$this->homegameRepository->returns('getByName', new Homegame());
			$this->expectException();

			$this->sut->action_details('any', '2010-01-01');
		}

		function test_ActionDetails_ReturnsCorrectModel(){
			TestHelper::setupUserWithPlayerRights($this->userContext);
			$this->homegameRepository->returns('getByName', new Homegame());
			$this->cashgameRepository->returns('getByDate', new Cashgame());
			$this->playerRepository->returns('getByUserName', new Player());

			$viewResult = $this->sut->action_details('any', '2010-01-01');

			$this->assertIsA($viewResult->model, 'app\Cashgame\Details\DetailsModel');
		}

	}

}