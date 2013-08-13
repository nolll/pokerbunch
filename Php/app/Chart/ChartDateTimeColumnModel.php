<?php
namespace app\Chart{

	class ChartDateTimeColumnModel extends ChartColumnModel {

		public function __construct($label, $pattern = null){
			parent::__construct('datetime', $label, $pattern);
		}

	}

}