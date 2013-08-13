namespace tests\AppTests\Player{

	use app\Player\PlayerValidatorFactory;
	use entities\Homegame;
	use entities\Player;
	use tests\TestHelper;
	use tests\UnitTestCase;
	use core\ClassNames;
	use app\Player\PlayerValidatorFactoryImpl;

	class PlayerValidatorTests extends UnitTestCase {

		private $playerRepository;

		function setUp(){
			playerRepository = TestHelper::getFake(ClassNames::$PlayerRepository);
		}

		function test_IsValid_WithValidValues_ReturnsTrue(){
			playerRepository.returns('getAll', array());
			$player = new Player();
			$player.setDisplayName('a');
			$validator = getValidator($player);

			assertTrue($validator.isValid());
		}

		function test_IsValid_WithEmptyDisplayName_ReturnsFalse(){
			playerRepository.returns('getAll', array());
			$player = new Player();
			$player.setDisplayName('');
			$validator = getValidator($player);

			assertFalse($validator.isValid());
		}

		function test_IsValid_WithNonExistingPlayerName_ReturnsTrue(){
			$homegame = new Homegame();
			$player = new Player();
			$player.setDisplayName('a');
			playerRepository.returns("getAll", array());
			$validatorFactory = new PlayerValidatorFactoryImpl(playerRepository);
			$validator = $validatorFactory.getAddPlayerValidator($player, $homegame);

			assertTrue($validator.isValid());
		}

		function test_IsValid_WithExistingPlayerName_ReturnsFalse(){
			$homegame = new Homegame();
			$player = new Player();
			$player.setDisplayName('a');
			$existingPlayer = new Player();
			$existingPlayer.setDisplayName('a');
			$players = array($existingPlayer);
			playerRepository.returns('getAll', $players);
			$validatorFactory = new PlayerValidatorFactoryImpl(playerRepository);
			$validator = $validatorFactory.getAddPlayerValidator($player, $homegame);

			assertFalse($validator.isValid());
		}

		function getValidator(Player $player){
			$homegame = new Homegame();
			return getValidatorFactory().getAddPlayerValidator($player, $homegame);
		}

		/**
		 * @return PlayerValidatorFactory;
		 */
		function getValidatorFactory(){
			return new PlayerValidatorFactoryImpl(playerRepository);
		}

	}

}