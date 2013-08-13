<?php
namespace app\Chart{

	class ChartRowModel {

		public $c;

		public function __construct(){
			$this->c = array();
		}

		public function addValue(ChartValueModel $val){
			$this->c[] = $val;
		}

	}

}