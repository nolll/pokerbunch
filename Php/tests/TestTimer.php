<?php
namespace tests{

	class TestTimer {

		private $startTime;

		public function __construct(){
			$this->startTime = microtime(true);
		}

		public function measure(){
			$measureTime = microtime(true);
			return $measureTime - $this->startTime;
		}

	}

}