<?php
namespace core\FormFields{

	class TimezoneFieldModel extends SelectFieldModel{

		public function __construct($fieldName, $fieldId, $selectedValue, $timezones, $firstItemText = null){
			$items = $this->getSelectItems($timezones);
			parent::__construct($fieldName, $fieldId, $selectedValue, $items, $firstItemText);
		}

		private function getSelectItems($timezones){
			$items = array();
			if($timezones != null){
				foreach($timezones as $timezone){
					$items[] = new SelectFieldItem($timezone);
				}
			}
			return $items;
		}

	}

}