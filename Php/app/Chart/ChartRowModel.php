namespace app\Chart{

	class ChartRowModel {

		public $c;

		public function __construct(){
			c = array();
		}

		public function addValue(ChartValueModel $val){
			c[] = $val;
		}

	}

}