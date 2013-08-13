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
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->homegameRepositoryMock = $this->getFakeHomegameRepository();
			$this->cashgameRepositoryMock = $this->getFakeCashgameRepository();
			$this->playerRepositoryMock = $this->getFakePlayerRepository();
			$userStorage = TestHelper::getFake(ClassNames::$UserStorage);
			$avatarService = TestHelper::getFake(ClassNames::$AvatarService);
			$avatarModelBuilder = new AvatarModelBuilder($avatarService);
			$this->sut = new PlayerDetailsController($this->userContext, $this->homegameRepositoryMock, $this->cashgameRepositoryMock, $this->playerRepositoryMock, $userStorage, $avatarModelBuilder);
		}

		function test_ActionDetails_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requirePlayer');
			$this->expectException();

			$this->sut->action_details("homegame1", "Player 1");
		}

		function test_ActionDelete_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requireManager');
			$this->expectException();

			$this->sut->action_delete("homegame1", "Player 1");
		}

		function test_ActionDelete_WithManagerRightsPlayerHasCashgameResults_RedirectsToDetails(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$this->playerRepositoryMock->returns('getByName', new Player());
			$this->cashgameRepositoryMock->returns("hasPlayed", true);

			$urlModel = $this->sut->action_delete('homegame1', 'Player 1');

			$this->assertIsA($urlModel, 'app\Urls\PlayerDetailsUrlModel');
		}

		function test_ActionDelete_WithManagerRightsPlayerHasCashgameResults_DoesntCallDeletePlayer(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$this->playerRepositoryMock->returns('getByName', new Player());
			$this->cashgameRepositoryMock->returns("hasPlayed", true);

			$this->playerRepositoryMock->expectNever('deletePlayer');

			$this->sut->action_delete('homegame1', 'Player 1');
		}

		function test_ActionDelete_WithManagerRightsPlayerHasNoCashgameResults_CallsDeletePlayer(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$this->playerRepositoryMock->returns('getByName', new Player());
			$this->cashgameRepositoryMock->returns("hasPlayed", false);

			$this->playerRepositoryMock->expectOnce('deletePlayer');

			$this->sut->action_delete('homegame1', 'Player 1');
		}

		function test_ActionDelete_WithManagerRightsPlayerHasNoCashgameResults_RedirectsToPlayerListing(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$this->playerRepositoryMock->returns('getByName', new Player());
			$this->cashgameRepositoryMock->returns("hasPlayed", false);

			$urlModel = $this->sut->action_delete('homegame1', 'Player 1');

			$this->assertIsA($urlModel, 'app\Urls\PlayerIndexUrlModel');
		}

	}

}