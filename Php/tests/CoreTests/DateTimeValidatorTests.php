namespace tests\CoreTests{

	use tests\TestHelper;
	use core\Validation\DateTimeValidator;
	use tests\UnitTestCase;

	class DateTimeValidatorTests extends UnitTestCase {

		function test_IsValid_WithDateTime_ReturnsTrue(){
			$subject = "2010-01-01 10:00:00";
			$errorMessage = "error-message";
			$validator = new DateTimeValidator($subject, $errorMessage);

			$this->assertTrue($validator->isValid());
		}

		function test_IsValid_WithEmptyValue_ReturnsTrue(){
			$subject = "";
			$errorMessage = "error-message";
			$validator = new DateTimeValidator($subject, $errorMessage);

			$this->assertTrue($validator->isValid());
		}

		function test_IsValid_WithString_ReturnsFalse(){
			$subject = "invalidvalue";
			$errorMessage = "error-message";
			$validator = new DateTimeValidator($subject, $errorMessage);

			$this->assertFalse($validator->isValid());
		}

	}

}