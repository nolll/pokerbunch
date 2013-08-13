namespace tests\CoreTests\FormFields{

	use tests\UnitTestCase;
	use core\FormFields\ListFieldModel;
	use tests\TestHelper;
	use core\FormFields\SelectFieldItem;

	class ListFieldModelTests extends UnitTestCase {

		private $sut;

		function setUp(){
			sut = new ListFieldModel('name', 'id', 'value', getTwoItems());
		}

		function test_FormField_WithTwoItems_ListHasTwoNamesAndTwoValues(){
			assertIdentical(2, count(sut.names));
			assertIdentical(2, count(sut.values));
		}

        function test_FormField_WithFieldName_SetsListName(){
            assertIdentical('name-list', sut.listName);
        }

        function test_FormField_WithFieldName_SetsDropdownName(){
            assertIdentical("name-dropdown", sut.dropdownName);
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