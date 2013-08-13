namespace core\FormFields{

	class FormFieldModel{

		public $fieldName;
		public $value;
		public $fieldId;

		public function __construct($fieldName, $fieldId, $selectedValue){
			fieldName = $fieldName;
			if(fieldName == null){
				fieldName = '';
			}
			value = $selectedValue;
			fieldId = $fieldId;
		}

	}

}