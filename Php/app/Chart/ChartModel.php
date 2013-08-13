<?php
namespace app\Chart{

	class ChartModel {

		public $cols;
		public $rows;
		public $p;

		public function __construct(){
			$this->cols = array();
			$this->rows = array();
			$this->p = null;
		}

		protected function addColumn(ChartColumnModel $col){
			$this->cols[] = $col;
		}

		protected function addRow(ChartRowModel $row){
			$this->rows[] = $row;
		}

	}

}