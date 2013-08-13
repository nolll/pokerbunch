namespace core\FormFields{

	class SelectFieldItem{

		public $name;
		public $value;

		public function __construct($name, $value = null){
			name = $name;
			value = $value != null ? $value : $name;
		}

	}

}