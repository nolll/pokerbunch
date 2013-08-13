<?php
namespace tests\AppTests\Chart{

	use app\Chart\ChartColumnModel;
	use tests\UnitTestCase;

	class ChartColumnModelTests extends UnitTestCase {

		private $type;
		private $label;
		private $pattern;

		function getSut(){
			return new ChartColumnModel($this->type, $this->label, $this->pattern);
		}

		function test_Type_IsSet(){
			$this->type = 'a';

			$sut = $this->getSut();

			$this->assertEqual('a', $sut->type);
		}

		function test_Label_IsSet(){
			$this->label = 'a';

			$sut = $this->getSut();

			$this->assertEqual('a', $sut->label);
		}

		function test_Pattern_IsSet(){
			$this->pattern = 'a';

			$sut = $this->getSut();

			$this->assertEqual('a', $sut->pattern);
		}

		function test_Pattern_AcceptsNull(){
			$this->pattern = null;

			$sut = $this->getSut();

			$this->assertNull($sut->pattern);
		}

	}

}