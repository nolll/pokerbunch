namespace tests\CoreTests{

	use tests\TestHelper;
	use tests\UnitTestCase;
	use core\Validation\IntegerValidator;

	class IntegerValidatorTests extends UnitTestCase {

		function test_IsValid_WithInteger_ReturnsTrue(){
			$subject = "1";
			$errorMessage = "error-message";
			$validator = new IntegerValidator($subject, $errorMessage);

			$this->assertTrue($validator->isValid());
		}

		function test_IsValid_WithEmptyValue_ReturnsTrue(){
			$subject = "";
			$errorMessage = "error-message";
			$validator = new IntegerValidator($subject, $errorMessage);

			$this->assertTrue($validator->isValid());
		}

		function test_IsValid_WithString_ReturnsFalse(){
			$subject = "anyvalue";
			$errorMessage = "error-message";
			$validator = new IntegerValidator($subject, $errorMessage);

			$this->assertFalse($validator->isValid());
		}

		function test_IsValid_WithFloat_ReturnsFalse(){
			$subject = "1.1";
			$errorMessage = "error-message";
			$validator = new IntegerValidator($subject, $errorMessage);

			$this->assertFalse($validator->isValid());
		}

	}

}