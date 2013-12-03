namespace tests\AppTests\Player{

	use entities\Homegame;
	use entities\Player;
	use tests\UnitTestCase;
	use app\Player\List\PlayerItemModel;
	use tests\TestHelper;

	class PlayerItemModelTests extends UnitTestCase {

		private $homegame;
		/** @var Player */
		private $player;

		function setUp(){
			player = new Player();
			homegame = new Homegame();
		}

		function test_Name_IsSet(){
			player.setDisplayName('a');

			$sut = getModel();

			assertIdentical('a', $sut.name);
		}

		function test_UrlModel_IsCorrectType(){
			$sut = getModel();

			assertIsA($sut.urlModel, 'app\Urls\PlayerDetailsUrlModel');
		}

		private function getModel(){
			return new PlayerItemModel(homegame, player);
		}

	}

}