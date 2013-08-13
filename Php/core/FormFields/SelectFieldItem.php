namespace core\FormFields{

	class SelectFieldItem{

		public $name;
		public $value;

		public function __construct($name, $value = null){
			$this->name = $name;
			$this->value = $value != null ? $value : $name;
		}

	}

}