<?php
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
			$this->playerRepository = TestHelper::getFake(ClassNames::$PlayerRepository);
		}

		function test_IsValid_WithValidValues_ReturnsTrue(){
			$this->playerRepository->returns('getAll', array());
			$player = new Player();
			$player->setDisplayName('a');
			$validator = $this->getValidator($player);

			$this->assertTrue($validator->isValid());
		}

		function test_IsValid_WithEmptyDisplayName_ReturnsFalse(){
			$this->playerRepository->returns('getAll', array());
			$player = new Player();
			$player->setDisplayName('');
			$validator = $this->getValidator($player);

			$this->assertFalse($validator->isValid());
		}

		function test_IsValid_WithNonExistingPlayerName_ReturnsTrue(){
			$homegame = new Homegame();
			$player = new Player();
			$player->setDisplayName('a');
			$this->playerRepository->returns("getAll", array());
			$validatorFactory = new PlayerValidatorFactoryImpl($this->playerRepository);
			$validator = $validatorFactory->getAddPlayerValidator($player, $homegame);

			$this->assertTrue($validator->isValid());
		}

		function test_IsValid_WithExistingPlayerName_ReturnsFalse(){
			$homegame = new Homegame();
			$player = new Player();
			$player->setDisplayName('a');
			$existingPlayer = new Player();
			$existingPlayer->setDisplayName('a');
			$players = array($existingPlayer);
			$this->playerRepository->returns('getAll', $players);
			$validatorFactory = new PlayerValidatorFactoryImpl($this->playerRepository);
			$validator = $validatorFactory->getAddPlayerValidator($player, $homegame);

			$this->assertFalse($validator->isValid());
		}

		function getValidator(Player $player){
			$homegame = new Homegame();
			return $this->getValidatorFactory()->getAddPlayerValidator($player, $homegame);
		}

		/**
		 * @return PlayerValidatorFactory;
		 */
		function getValidatorFactory(){
			return new PlayerValidatorFactoryImpl($this->playerRepository);
		}

	}

}