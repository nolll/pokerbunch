namespace core\FormFields{

	class ListFieldModel extends SelectFieldModel{

        public $listName;
        public $dropdownName;

		public function __construct($fieldName, $fieldId, $selectedValue, $items = null, $firstItemText = null){
			parent::__construct($fieldName, $fieldId, $selectedValue, $items, $firstItemText);
            listName = $fieldName . "-list";
            dropdownName = $fieldName . "-dropdown";
		}

	}

}