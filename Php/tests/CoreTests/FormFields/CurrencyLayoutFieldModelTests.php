namespace tests\CoreTests\FormFields{

	use tests\UnitTestCase;
	use core\FormFields\CurrencyLayoutFieldModel;
	use tests\TestHelper;

	class CurrencyLayoutFieldModelTests extends UnitTestCase {

		private $sut;

		function setUp(){
			sut = new CurrencyLayoutFieldModel('name', 'id', 'value');
		}

		function test_CurrencyLayoutField_SetsFieldName(){
			assertIdentical('name', sut.fieldName);
		}

		function test_CurrencyLayoutField_SetsLayoutNamesAndValues(){
			assertIdentical(4, count(sut.names));
			assertIdentical(4, count(sut.values));
		}

	}

}