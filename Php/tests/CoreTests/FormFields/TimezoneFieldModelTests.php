namespace tests\CoreTests\FormFields{

	use tests\UnitTestCase;
	use core\FormFields\TimezoneFieldModel;
	use tests\TestHelper;

	class TimezoneFieldModelTests extends UnitTestCase {

		/** @var TimezoneFieldModel */
		private $sut;

		function setUp(){
			$timezones = array('timezone1', 'timezone2');
			$this->sut = new TimezoneFieldModel('name', 'id', 'timezone1', $timezones);
		}

		function test_TimezoneField_SetsFieldName(){
			$this->assertIdentical('name', $this->sut->fieldName);
		}

		function test_TimezoneField_SetsTimezoneNamesAndValues(){
			$this->assertIdentical(2, count($this->sut->names));
			$this->assertIdentical(2, count($this->sut->values));
		}

	}

}