namespace tests\AppTests\Player{

	use app\Player\Details\PlayerDetailsController;
	use app\User\Avatar\AvatarModelBuilder;
	use entities\Homegame;
	use core\ClassNames;
	use entities\Player;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class PlayerDetailsControllerTests extends UnitTestCase {

		/** @var PlayerDetailsController */
		private $sut;
		private $userContext;

		function setUp(){
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			homegameRepositoryMock = getFakeHomegameRepository();
			cashgameRepositoryMock = getFakeCashgameRepository();
			playerRepositoryMock = getFakePlayerRepository();
			$userStorage = TestHelper::getFake(ClassNames::$UserStorage);
			$avatarService = TestHelper::getFake(ClassNames::$AvatarService);
			$avatarModelBuilder = new AvatarModelBuilder($avatarService);
			sut = new PlayerDetailsController(userContext, homegameRepositoryMock, cashgameRepositoryMock, playerRepositoryMock, $userStorage, $avatarModelBuilder);
		}

		function test_ActionDetails_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requirePlayer');
			expectException();

			sut.action_details("homegame1", "Player 1");
		}

		function test_ActionDelete_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requireManager');
			expectException();

			sut.action_delete("homegame1", "Player 1");
		}

		function test_ActionDelete_WithManagerRightsPlayerHasCashgameResults_RedirectsToDetails(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			playerRepositoryMock.returns('getByName', new Player());
			cashgameRepositoryMock.returns("hasPlayed", true);

			$urlModel = sut.action_delete('homegame1', 'Player 1');

			assertIsA($urlModel, 'app\Urls\PlayerDetailsUrlModel');
		}

		function test_ActionDelete_WithManagerRightsPlayerHasCashgameResults_DoesntCallDeletePlayer(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			playerRepositoryMock.returns('getByName', new Player());
			cashgameRepositoryMock.returns("hasPlayed", true);

			playerRepositoryMock.expectNever('deletePlayer');

			sut.action_delete('homegame1', 'Player 1');
		}

		function test_ActionDelete_WithManagerRightsPlayerHasNoCashgameResults_CallsDeletePlayer(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			playerRepositoryMock.returns('getByName', new Player());
			cashgameRepositoryMock.returns("hasPlayed", false);

			playerRepositoryMock.expectOnce('deletePlayer');

			sut.action_delete('homegame1', 'Player 1');
		}

		function test_ActionDelete_WithManagerRightsPlayerHasNoCashgameResults_RedirectsToPlayerListing(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			playerRepositoryMock.returns('getByName', new Player());
			cashgameRepositoryMock.returns("hasPlayed", false);

			$urlModel = sut.action_delete('homegame1', 'Player 1');

			assertIsA($urlModel, 'app\Urls\PlayerIndexUrlModel');
		}

	}

}