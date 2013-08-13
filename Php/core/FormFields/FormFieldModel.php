namespace core\FormFields{

	class FormFieldModel{

		public $fieldName;
		public $value;
		public $fieldId;

		public function __construct($fieldName, $fieldId, $selectedValue){
			$this->fieldName = $fieldName;
			if($this->fieldName == null){
				$this->fieldName = '';
			}
			$this->value = $selectedValue;
			$this->fieldId = $fieldId;
		}

	}

}