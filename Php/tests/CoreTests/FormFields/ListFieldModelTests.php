<?php
namespace tests\CoreTests\FormFields{

	use tests\UnitTestCase;
	use core\FormFields\ListFieldModel;
	use tests\TestHelper;
	use core\FormFields\SelectFieldItem;

	class ListFieldModelTests extends UnitTestCase {

		private $sut;

		function setUp(){
			$this->sut = new ListFieldModel('name', 'id', 'value', $this->getTwoItems());
		}

		function test_FormField_WithTwoItems_ListHasTwoNamesAndTwoValues(){
			$this->assertIdentical(2, count($this->sut->names));
			$this->assertIdentical(2, count($this->sut->values));
		}

        function test_FormField_WithFieldName_SetsListName(){
            $this->assertIdentical('name-list', $this->sut->listName);
        }

        function test_FormField_WithFieldName_SetsDropdownName(){
            $this->assertIdentical("name-dropdown", $this->sut->dropdownName);
        }

		function getTwoItems(){
			$numItems = 2;
			$items = array();
			for($i = 1; $i < $numItems + 1; $i++){
				$items[] = new SelectFieldItem("name" . $i, "value" . $i);
			}
			return $items;
		}

	}

}