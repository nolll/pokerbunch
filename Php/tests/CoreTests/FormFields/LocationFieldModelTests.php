namespace tests\CoreTests\FormFields{

	use tests\UnitTestCase;
	use core\FormFields\LocationFieldModel;
	use tests\TestHelper;

	class LocationFieldModelTests extends UnitTestCase {

		private $sut;

		function setUp(){
			$locations = array('location1', 'location2');
			$this->sut = new LocationFieldModel('name', 'id', 'value', $locations);
		}

		function test_LocationField_SetsFieldName(){
			$this->assertIdentical('name', $this->sut->fieldName);
		}

		function test_LocationField_SetsLocationNamesAndValues(){
			$this->assertIdentical(2, count($this->sut->names));
			$this->assertIdentical(2, count($this->sut->values));
		}

	}

}