namespace app\Chart{

	class ChartModel {

		public $cols;
		public $rows;
		public $p;

		public function __construct(){
			cols = array();
			rows = array();
			p = null;
		}

		protected function addColumn(ChartColumnModel $col){
			cols[] = $col;
		}

		protected function addRow(ChartRowModel $row){
			rows[] = $row;
		}

	}

}