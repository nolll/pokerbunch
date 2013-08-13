namespace tests\CoreTests\FormFields{

	use tests\UnitTestCase;
	use core\FormFields\LocationFieldModel;
	use tests\TestHelper;

	class LocationFieldModelTests extends UnitTestCase {

		private $sut;

		function setUp(){
			$locations = array('location1', 'location2');
			sut = new LocationFieldModel('name', 'id', 'value', $locations);
		}

		function test_LocationField_SetsFieldName(){
			assertIdentical('name', sut.fieldName);
		}

		function test_LocationField_SetsLocationNamesAndValues(){
			assertIdentical(2, count(sut.names));
			assertIdentical(2, count(sut.values));
		}

	}

}