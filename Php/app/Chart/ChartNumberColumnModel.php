<?php
namespace app\Chart{

	class ChartNumberColumnModel extends ChartColumnModel {

		public function __construct($label){
			parent::__construct('number', $label);
		}

	}

}