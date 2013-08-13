<?php
namespace core\FormFields{
	use core\Globalization;

	class CurrencyLayoutFieldModel extends SelectFieldModel{

		public function __construct($fieldName, $fieldId, $selectedValue, $firstItemText = null){
			$items = $this->getSelectItems();
			parent::__construct($fieldName, $fieldId, $selectedValue, $items, $firstItemText);
		}

		private function getSelectItems(){
			$layouts = Globalization::getCurrencyLayouts();
			$items = array();
			if($layouts != null){
				foreach($layouts as $layout){
					$items[] = new SelectFieldItem($layout);
				}
			}
			return $items;
		}

	}

}