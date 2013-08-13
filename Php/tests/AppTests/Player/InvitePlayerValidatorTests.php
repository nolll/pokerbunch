namespace tests\AppTests\Player{

	use app\Player\PlayerValidatorFactory;
	use tests\TestHelper;
	use tests\UnitTestCase;
	use core\ClassNames;
	use app\Player\PlayerValidatorFactoryImpl;

	class InvitePlayerValidatorTests extends UnitTestCase {

		function test_IsValid_WithValidValues_ReturnsTrue(){
			$email = "valid@email.com";
			$validator = getValidator($email);

			assertTrue($validator.isValid());
		}

		function test_IsValid_WithEmptyEmail_ReturnsFalse(){
			$email = "";
			$validator = getValidator($email);

			assertFalse($validator.isValid());
		}

		function test_IsValid_WithInvalidEmail_ReturnsTrue(){
			$email = "invalidemail";
			$validator = getValidator($email);

			assertFalse($validator.isValid());
		}

		function getValidator($email){
			return getValidatorFactory().getInvitePlayerValidator($email);
		}

		/**
		 * @return PlayerValidatorFactory;
		 */
		function getValidatorFactory(){
			$playerRepository = getPlayerRepository();
			return new PlayerValidatorFactoryImpl($playerRepository);
		}

		function getPlayerRepository(){
			return TestHelper::getFake(ClassNames::$PlayerRepository);
		}

	}

}