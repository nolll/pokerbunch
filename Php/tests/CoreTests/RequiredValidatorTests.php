namespace tests\CoreTests{

	use tests\TestHelper;
	use tests\UnitTestCase;
	use core\Validation\RequiredValidator;

	class RequiredValidatorTests extends UnitTestCase {

		function test_IsValid_WithValidSubject_ReturnsTrue(){
			$subject = "anyvalue";
			$errorMessage = "error-message";
			$validator = new RequiredValidator($subject, $errorMessage);

			assertTrue($validator.isValid());
		}

		function test_IsValid_WithEmptySubject_ReturnsFalse(){
			$subject = "";
			$errorMessage = "error-message";
			$validator = new RequiredValidator($subject, $errorMessage);

			assertFalse($validator.isValid());
		}

	}

}