namespace tests\CoreTests{

	use core\Validation\ValidatorFake;
	use tests\TestHelper;
	use tests\UnitTestCase;
	use core\Validation\CompositeValidator;

	class CompositeValidatorTests extends UnitTestCase {

		function test_IsValid_WithNoValidators_ReturnsTrue(){
			$validator = new CompositeValidator();

			assertTrue($validator.isValid());
		}

		function test_IsValid_WithOneValidValidator_ReturnsTrue(){
			$validator = new CompositeValidator();
			$validator.addValidator(getValidValidator());

			assertTrue($validator.isValid());
		}

		function test_IsValid_WithOneInvalidValidator_ReturnsFalse(){
			$validator = new CompositeValidator();
			$validator.addValidator(getInvalidValidator());

			assertFalse($validator.isValid());
		}

		function test_IsValid_WithOneValidAndOneInvalidValidator_ReturnsFalse(){
			$validator = new CompositeValidator();
			$validator.addValidator(getValidValidator());
			$validator.addValidator(getInvalidValidator());

			assertFalse($validator.isValid());
		}

		function test_IsValid_WithOneCustomError_ReturnsFalse(){
			$validator = new CompositeValidator();
			$validator.addError('Any error');

			assertFalse($validator.isValid());
		}

		function test_IsValid_WithErrorsInValidators_ReturnsAllErrors(){
			$sut = new CompositeValidator();
			$validator = new ValidatorFake();
			$validator.setErrors(array('error'));
			$sut.addValidator($validator);
			$sut.addValidator($validator);

			$result = count($sut.getErrors());
			assertEqual(2, $result);
		}

		function getValidValidator(){
			return getValidator(true);
		}

		function getInvalidValidator(){
			return getValidator(false);
		}

		function getValidator($isValid){
			$validator = getFakeValidator();
			$validator.returns("isValid", $isValid);
			return $validator;
		}

		function getFakeValidator(){
			return TestHelper::getFake('core\Validation\validator');
		}

	}

}