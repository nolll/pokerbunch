namespace core\FormFields{
	use core\Globalization;

	class LocationFieldModel extends ListFieldModel{

		public function __construct($fieldName, $fieldId, $selectedValue, $locations, $firstItemText = null){
			$items = $this->getSelectItems($locations);
			parent::__construct($fieldName, $fieldId, $selectedValue, $items, $firstItemText);
		}

		private function getSelectItems($locations){
			$items = array();
			if($locations != null){
				foreach($locations as $location){
					$items[] = new SelectFieldItem($location);
				}
			}
			return $items;
		}

	}

}