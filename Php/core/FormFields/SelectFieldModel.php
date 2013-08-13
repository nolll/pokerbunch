<?php
namespace core\FormFields{

	class SelectFieldModel extends FormFieldModel{

		public $names;
		public $values;

		public function __construct($fieldName, $fieldId, $selectedValue, $items = null, $firstItemText = null){
			parent::__construct($fieldName, $fieldId, $selectedValue);
			$this->initArrays();
			$this->setFirstItem($firstItemText);
			$this->setItems($items);
		}

		private function initArrays(){
			$this->names = array();
			$this->values = array();
		}

		private function setFirstItem($firstItemText){
			if ($firstItemText != null && $firstItemText != '') {
				$this->names[] = $firstItemText;
				$this->values[] = '';
			}
		}

		private function setItems($items){
			if ($items != null) {
				foreach ($items as $item) {
					$this->names[] = $item->name;
					$this->values[] = $item->value;
				}
			}
		}

	}

}