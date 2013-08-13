<?php
namespace tests\CoreTests\FormFields{

	use tests\UnitTestCase;
	use core\FormFields\CurrencyLayoutFieldModel;
	use tests\TestHelper;

	class CurrencyLayoutFieldModelTests extends UnitTestCase {

		private $sut;

		function setUp(){
			$this->sut = new CurrencyLayoutFieldModel('name', 'id', 'value');
		}

		function test_CurrencyLayoutField_SetsFieldName(){
			$this->assertIdentical('name', $this->sut->fieldName);
		}

		function test_CurrencyLayoutField_SetsLayoutNamesAndValues(){
			$this->assertIdentical(4, count($this->sut->names));
			$this->assertIdentical(4, count($this->sut->values));
		}

	}

}