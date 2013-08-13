namespace tests\AppTests\Cashgame\Action{

	use app\Cashgame\Action\ActionController;
	use entities\Cashgame;
	use entities\CashgameResult;
	use entities\Player;
	use entities\Homegame;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class ActionControllerTests extends UnitTestCase {

		/** @var ActionController */
		private $sut;
		private $userContext;

		function setUp(){
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			homegameRepositoryMock = getFakeHomegameRepository();
			cashgameRepositoryMock = getFakeCashgameRepository();
			playerRepositoryMock = getFakePlayerRepository();
			sut = new ActionController(userContext, homegameRepositoryMock, cashgameRepositoryMock, playerRepositoryMock);
		}

		function test_ActionAction_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requirePlayer');
			expectException();

			sut.action_action("homegame1", "2010-01-01", "Player 1");
		}

		function test_ActionAction_ReturnsCorrectModel(){
			homegameRepositoryMock.returns('getByName', new Homegame());
            TestHelper::setupUserWithPlayerRights(userContext);
            $cashgame = new Cashgame();
			$cashgameResult = new CashgameResult();
			$player = new Player();
			$player.setUserName('user1');
			$player.setId(1);
			$cashgameResult.setPlayer($player);
			$cashgame.setResults(array($cashgameResult));
			cashgameRepositoryMock.returns('getByDateString', $cashgame);
			playerRepositoryMock.returns('getByName', $player);

			$viewResult = sut.action_action("homegame1", "2010-01-01", "Player 1");

			assertIsA($viewResult.model, 'app\Cashgame\Action\ActionModel');
		}

	}

}