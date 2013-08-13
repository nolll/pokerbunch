namespace tests\CoreTests{

	use core\Validation\ValidatorFake;
	use tests\TestHelper;
	use tests\UnitTestCase;
	use core\Validation\CompositeValidator;

	class CompositeValidatorTests extends UnitTestCase {

		function test_IsValid_WithNoValidators_ReturnsTrue(){
			$validator = new CompositeValidator();

			$this->assertTrue($validator->isValid());
		}

		function test_IsValid_WithOneValidValidator_ReturnsTrue(){
			$validator = new CompositeValidator();
			$validator->addValidator($this->getValidValidator());

			$this->assertTrue($validator->isValid());
		}

		function test_IsValid_WithOneInvalidValidator_ReturnsFalse(){
			$validator = new CompositeValidator();
			$validator->addValidator($this->getInvalidValidator());

			$this->assertFalse($validator->isValid());
		}

		function test_IsValid_WithOneValidAndOneInvalidValidator_ReturnsFalse(){
			$validator = new CompositeValidator();
			$validator->addValidator($this->getValidValidator());
			$validator->addValidator($this->getInvalidValidator());

			$this->assertFalse($validator->isValid());
		}

		function test_IsValid_WithOneCustomError_ReturnsFalse(){
			$validator = new CompositeValidator();
			$validator->addError('Any error');

			$this->assertFalse($validator->isValid());
		}

		function test_IsValid_WithErrorsInValidators_ReturnsAllErrors(){
			$sut = new CompositeValidator();
			$validator = new ValidatorFake();
			$validator->setErrors(array('error'));
			$sut->addValidator($validator);
			$sut->addValidator($validator);

			$result = count($sut->getErrors());
			$this->assertEqual(2, $result);
		}

		function getValidValidator(){
			return $this->getValidator(true);
		}

		function getInvalidValidator(){
			return $this->getValidator(false);
		}

		function getValidator($isValid){
			$validator = $this->getFakeValidator();
			$validator->returns("isValid", $isValid);
			return $validator;
		}

		function getFakeValidator(){
			return TestHelper::getFake('core\Validation\validator');
		}

	}

}