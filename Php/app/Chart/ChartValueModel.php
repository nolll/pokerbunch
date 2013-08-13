namespace app\Chart{

	class ChartValueModel {

		public $v;
		public $f;

		public function __construct($val){
			$this->v = $val;
			$this->f = null;
		}

	}

}