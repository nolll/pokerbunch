namespace tests\CoreTests{

	use tests\TestHelper;
	use tests\UnitTestCase;
	use core\Validation\EmailValidator;

	class EmailValidatorTests extends UnitTestCase {

		function test_IsValid_WithValidEmail_ReturnsTrue(){
			$email = "test@example.com";
			$errorMessage = "error-message";
			$validator = new EmailValidator($email, $errorMessage);

			assertTrue($validator.isValid());
		}

		function test_IsValid_WithInvalidEmail_ReturnsTrue(){
			$email = "testexamplecom";
			$errorMessage = "error-message";
			$validator = new EmailValidator($email, $errorMessage);

			assertFalse($validator.isValid());
		}

		function test_IsValid_WithEmptyEmail_ReturnsTrue(){
			$email = "";
			$errorMessage = "error-message";
			$validator = new EmailValidator($email, $errorMessage);

			assertTrue($validator.isValid());
		}

	}

}