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
			$this->homegame = new Homegame();
			$this->players = array();
			$this->isInManagerMode = false;
		}

		function test_AddUrl_IsCorrectType(){
			$sut = $this->getModel();

			$this->assertIsA($sut->addUrl, 'app\Urls\PlayerAddUrlModel');
		}

		function test_ShowAddLink_WithPlayerRights_IsFalse(){
			$sut = $this->getModel();

			$this->assertFalse($sut->showAddLink);
		}

		function test_ShowAddLink_WithManagerRights_IsTrue(){
			$this->isInManagerMode = true;

			$sut = $this->getModel();

			$this->assertTrue($sut->showAddLink);
		}

		function test_PlayerModels_WithoutPlayers_ContainsNoItems(){
			$sut = $this->getModel();

			$this->assertIdentical(0, count($sut->playerModels));
		}

		function test_PlayerModels_With3Players_Contains3Items(){
			$dummyPlayer = new Player();
			$this->players = array($dummyPlayer, $dummyPlayer, $dummyPlayer);

			$sut = $this->getModel();

			$this->assertIdentical(3, count($sut->playerModels));
		}

		function getModel(){
			return new PlayerListingModel(new User(), $this->homegame, $this->players, $this->isInManagerMode);
		}

	}

}