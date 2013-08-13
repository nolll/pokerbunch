namespace tests\AppTests\Player{

	use entities\Homegame;
	use Domain\Classes\User;
	use entities\Player;
	use tests\UnitTestCase;
	use app\Player\Listing\PlayerListingModel;
	use tests\TestHelper;

	class PlayerListingModelTests extends UnitTestCase {

		/** @var Homegame */
		private $homegame;
		private $players;
		private $isInManagerMode;

		function setUp(){
			homegame = new Homegame();
			players = array();
			isInManagerMode = false;
		}

		function test_AddUrl_IsCorrectType(){
			$sut = getModel();

			assertIsA($sut.addUrl, 'app\Urls\PlayerAddUrlModel');
		}

		function test_ShowAddLink_WithPlayerRights_IsFalse(){
			$sut = getModel();

			assertFalse($sut.showAddLink);
		}

		function test_ShowAddLink_WithManagerRights_IsTrue(){
			isInManagerMode = true;

			$sut = getModel();

			assertTrue($sut.showAddLink);
		}

		function test_PlayerModels_WithoutPlayers_ContainsNoItems(){
			$sut = getModel();

			assertIdentical(0, count($sut.playerModels));
		}

		function test_PlayerModels_With3Players_Contains3Items(){
			$dummyPlayer = new Player();
			players = array($dummyPlayer, $dummyPlayer, $dummyPlayer);

			$sut = getModel();

			assertIdentical(3, count($sut.playerModels));
		}

		function getModel(){
			return new PlayerListingModel(new User(), homegame, players, isInManagerMode);
		}

	}

}