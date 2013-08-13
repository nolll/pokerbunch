<?php
namespace tests\AppTests\Homegame{

	use app\Homegame\HomegameValidatorFactory;
	use DateTimeZone;
	use entities\CurrencySettings;
	use tests\TestHelper;
	use tests\UnitTestCase;
	use core\ClassNames;
	use entities\Homegame;
	use app\Homegame\HomegameValidatorFactoryImpl;

	class HomegameValidatorTests extends UnitTestCase {

		private function getValidHomegame(){
			$homegame = new Homegame();
			$homegame->setDisplayName('a');
			$homegame->setCurrency(new CurrencySettings('b', 'c'));
			$homegame->setTimezone(new DateTimeZone('Europe/Stockholm'));
			return $homegame;
		}

		function test_IsValid_WithValidValues_ReturnsTrue(){
			$homegame = $this->getValidHomegame();

			$validator = $this->getValidator($homegame);

			$this->assertTrue($validator->isValid());
		}

		function test_IsValid_WithEmptyDisplayName_ReturnsFalse(){
			$homegame = $this->getValidHomegame();
			$homegame->setDisplayName('');

			$validator = $this->getValidator($homegame);

			$this->assertFalse($validator->isValid());
		}

		function test_IsValid_WithEmptyCurrencySymbol_ReturnsFalse(){
			$homegame = $this->getValidHomegame();
			$homegame->getCurrency()->setSymbol('')
			;
			$validator = $this->getValidator($homegame);

			$this->assertFalse($validator->isValid());
		}

		function test_IsValid_WithEmptyCurrencyLayout_ReturnsFalse(){
			$homegame = $this->getValidHomegame();
			$homegame->getCurrency()->setLayout('');

			$validator = $this->getValidator($homegame);

			$this->assertFalse($validator->isValid());
		}

		function test_IsValid_WithEmptyTimezone_ReturnsFalse(){
			$homegame = $this->getValidHomegame();
			$homegame->setTimezone(null);

			$validator = $this->getValidator($homegame);

			$this->assertFalse($validator->isValid());
		}

		function getValidator(Homegame $homegame){
			return $this->getValidatorFactory()->getAddHomegameValidator($homegame);
		}

		/**
		 * @return HomegameValidatorFactory;
		 */
		function getValidatorFactory(){
			$homegameStorage = $this->getHomegameStorage();
			return new HomegameValidatorFactoryImpl($homegameStorage);
		}

		function getHomegameStorage(){
			return TestHelper::getFake(ClassNames::$HomegameStorage);
		}

	}

}