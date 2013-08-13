<?php
namespace tests\AppTests\Chart{

	use app\Chart\ChartDateTimeColumnModel;
	use tests\UnitTestCase;

	class ChartDateTimeColumnModelTests extends UnitTestCase {

		private $label;
		private $pattern;

		function getSut(){
			return new ChartDateTimeColumnModel($this->label, $this->pattern);
		}

		function test_Type_IsSetToDateTime(){
			$this->label = 'any';
			$this->pattern = 'any';

			$sut = $this->getSut();

			$this->assertEqual('datetime', $sut->type);
		}

	}

}