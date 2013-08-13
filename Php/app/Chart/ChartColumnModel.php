namespace app\Chart{

	class ChartColumnModel {

		public $type;
		public $label;
		public $pattern;

		public function __construct($type, $label, $pattern = null){
			$this->type = $type;
			$this->label = $label;
			$this->pattern = $pattern;
		}

	}

}