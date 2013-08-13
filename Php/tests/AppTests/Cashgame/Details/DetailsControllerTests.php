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
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			cashgameRepository = TestHelper::getFake(ClassNames::$CashgameRepository);
			homegameRepository = TestHelper::getFake(ClassNames::$HomegameRepository);
			playerRepository = TestHelper::getFake(ClassNames::$PlayerRepository);
			$resultSharer = TestHelper::getFake(ClassNames::$ResultSharer);
			sut = new DetailsController(userContext, homegameRepository, cashgameRepository, playerRepository, $resultSharer);
		}

		function test_ActionDetails_NotAuthorized_ThrowsException(){
			userContext.throwOn('requirePlayer');
			homegameRepository.returns('getByName', new Homegame());
			expectException();

			sut.action_details('any', '2010-01-01');
		}

		function test_ActionDetails_ReturnsCorrectModel(){
			TestHelper::setupUserWithPlayerRights(userContext);
			homegameRepository.returns('getByName', new Homegame());
			cashgameRepository.returns('getByDate', new Cashgame());
			playerRepository.returns('getByUserName', new Player());

			$viewResult = sut.action_details('any', '2010-01-01');

			assertIsA($viewResult.model, 'app\Cashgame\Details\DetailsModel');
		}

	}

}