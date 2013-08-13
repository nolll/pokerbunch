namespace tests\AppTests\Player{

	use app\Player\PlayerValidatorFactory;
	use tests\TestHelper;
	use tests\UnitTestCase;
	use core\ClassNames;
	use app\Player\PlayerValidatorFactoryImpl;

	class InvitePlayerValidatorTests extends UnitTestCase {

		function test_IsValid_WithValidValues_ReturnsTrue(){
			$email = "valid@email.com";
			$validator = $this->getValidator($email);

			$this->assertTrue($validator->isValid());
		}

		function test_IsValid_WithEmptyEmail_ReturnsFalse(){
			$email = "";
			$validator = $this->getValidator($email);

			$this->assertFalse($validator->isValid());
		}

		function test_IsValid_WithInvalidEmail_ReturnsTrue(){
			$email = "invalidemail";
			$validator = $this->getValidator($email);

			$this->assertFalse($validator->isValid());
		}

		function getValidator($email){
			return $this->getValidatorFactory()->getInvitePlayerValidator($email);
		}

		/**
		 * @return PlayerValidatorFactory;
		 */
		function getValidatorFactory(){
			$playerRepository = $this->getPlayerRepository();
			return new PlayerValidatorFactoryImpl($playerRepository);
		}

		function getPlayerRepository(){
			return TestHelper::getFake(ClassNames::$PlayerRepository);
		}

	}

}