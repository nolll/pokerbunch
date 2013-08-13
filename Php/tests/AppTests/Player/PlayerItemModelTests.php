namespace tests\AppTests\Player{

	use entities\Homegame;
	use entities\Player;
	use tests\UnitTestCase;
	use app\Player\Listing\PlayerItemModel;
	use tests\TestHelper;

	class PlayerItemModelTests extends UnitTestCase {

		private $homegame;
		/** @var Player */
		private $player;

		function setUp(){
			$this->player = new Player();
			$this->homegame = new Homegame();
		}

		function test_Name_IsSet(){
			$this->player->setDisplayName('a');

			$sut = $this->getModel();

			$this->assertIdentical('a', $sut->name);
		}

		function test_UrlModel_IsCorrectType(){
			$sut = $this->getModel();

			$this->assertIsA($sut->urlModel, 'app\Urls\PlayerDetailsUrlModel');
		}

		private function getModel(){
			return new PlayerItemModel($this->homegame, $this->player);
		}

	}

}