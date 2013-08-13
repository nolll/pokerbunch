namespace tests\AppTests\Homegame{

	use DateTimeZone;
	use app\Homegame\HomegameValidatorFactory;
	use entities\CurrencySettings;
	use tests\TestHelper;
	use tests\UnitTestCase;
	use core\ClassNames;
	use app\Homegame\HomegameValidatorFactoryImpl;
	use Infrastructure\Data\Interfaces\HomegameStorage;
	use entities\Homegame;

	class UniqueHomegameValidatorTests extends UnitTestCase {

		private function getValidHomegame(){
			$homegame = new Homegame();
			$homegame.setDisplayName('a');
			$homegame.setCurrency(new CurrencySettings('b', 'c'));
			$homegame.setTimezone(new DateTimeZone('Europe/Stockholm'));
			return $homegame;
		}

		function test_IsValid_WithNonExistingSlug_ReturnsTrue(){
			$homegame = getValidHomegame();
			$homegameStorage = getHomegameStorage();
			$homegameStorage.returns("getRawHomegameByName", null);
			$validator = getValidator($homegameStorage, $homegame);

			assertTrue($validator.isValid());
		}

		function test_IsValid_WithExistingSlug_ReturnsFalse(){
			$homegame = getValidHomegame();
			$homegameStorage = getHomegameStorage();
			$existingHomegame = new Homegame();
			$homegameStorage.returns("getRawHomegameByName", $existingHomegame);
			$validator = getValidator($homegameStorage, $homegame);

			assertFalse($validator.isValid());
		}

		function getValidator(HomegameStorage $homegameStorage, Homegame $homegame){
			return getValidatorFactory($homegameStorage).getAddHomegameValidator($homegame);
		}

		/**
		 * @param HomegameStorage $homegameStorage
		 * @return HomegameValidatorFactory
		 */
		function getValidatorFactory(HomegameStorage $homegameStorage){
			return new HomegameValidatorFactoryImpl($homegameStorage);
		}

		function getHomegameStorage(){
			return TestHelper::getFake(ClassNames::$HomegameStorage);
		}

	}

}