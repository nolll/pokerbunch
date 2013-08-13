namespace core\FormFields{

	class SelectFieldModel extends FormFieldModel{

		public $names;
		public $values;

		public function __construct($fieldName, $fieldId, $selectedValue, $items = null, $firstItemText = null){
			parent::__construct($fieldName, $fieldId, $selectedValue);
			initArrays();
			setFirstItem($firstItemText);
			setItems($items);
		}

		private function initArrays(){
			names = array();
			values = array();
		}

		private function setFirstItem($firstItemText){
			if ($firstItemText != null && $firstItemText != '') {
				names[] = $firstItemText;
				values[] = '';
			}
		}

		private function setItems($items){
			if ($items != null) {
				foreach ($items as $item) {
					names[] = $item.name;
					values[] = $item.value;
				}
			}
		}

	}

}