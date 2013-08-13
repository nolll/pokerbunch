namespace tests\CoreTests\FormFields{

	use tests\UnitTestCase;
	use core\FormFields\TimezoneFieldModel;
	use tests\TestHelper;

	class TimezoneFieldModelTests extends UnitTestCase {

		/** @var TimezoneFieldModel */
		private $sut;

		function setUp(){
			$timezones = array('timezone1', 'timezone2');
			sut = new TimezoneFieldModel('name', 'id', 'timezone1', $timezones);
		}

		function test_TimezoneField_SetsFieldName(){
			assertIdentical('name', sut.fieldName);
		}

		function test_TimezoneField_SetsTimezoneNamesAndValues(){
			assertIdentical(2, count(sut.names));
			assertIdentical(2, count(sut.values));
		}

	}

}