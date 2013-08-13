<?php
namespace core\FormFields{

	class ListFieldModel extends SelectFieldModel{

        public $listName;
        public $dropdownName;

		public function __construct($fieldName, $fieldId, $selectedValue, $items = null, $firstItemText = null){
			parent::__construct($fieldName, $fieldId, $selectedValue, $items, $firstItemText);
            $this->listName = $fieldName . "-list";
            $this->dropdownName = $fieldName . "-dropdown";
		}

	}

}