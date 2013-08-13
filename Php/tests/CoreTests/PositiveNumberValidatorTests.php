namespace tests\CoreTests{

	use tests\TestHelper;
	use tests\UnitTestCase;
	use core\Validation\PositiveNumberValidator;

	class PositiveNumberValidatorTests extends UnitTestCase {

		function test_IsValid_WithPositiveInteger_ReturnsTrue(){
			$subject = "1";
			$errorMessage = "error-message";
			$validator = new PositiveNumberValidator($subject, $errorMessage);

			assertTrue($validator.isValid());
		}

		function test_IsValid_WithEmptyValue_ReturnsTrue(){
			$subject = "";
			$errorMessage = "error-message";
			$validator = new PositiveNumberValidator($subject, $errorMessage);

			assertTrue($validator.isValid());
		}

		function test_IsValid_WithFloat_ReturnsTrue(){
			$subject = "1.1";
			$errorMessage = "error-message";
			$validator = new PositiveNumberValidator($subject, $errorMessage);

			assertTrue($validator.isValid());
		}

		function test_IsValid_WithZero_ReturnsTrue(){
			$subject = "0";
			$errorMessage = "error-message";
			$validator = new PositiveNumberValidator($subject, $errorMessage);

			assertTrue($validator.isValid());
		}

		function test_IsValid_WithNegativeFloat_ReturnsFalse(){
			$subject = "-1.1";
			$errorMessage = "error-message";
			$validator = new PositiveNumberValidator($subject, $errorMessage);

			assertFalse($validator.isValid());
		}

		function test_IsValid_WithNegativeInteger_ReturnsFalse(){
			$subject = "-1";
			$errorMessage = "error-message";
			$validator = new PositiveNumberValidator($subject, $errorMessage);

			assertFalse($validator.isValid());
		}

		function test_IsValid_WithString_ReturnsFalse(){
			$subject = "anyvalue";
			$errorMessage = "error-message";
			$validator = new PositiveNumberValidator($subject, $errorMessage);

			assertFalse($validator.isValid());
		}

	}

}