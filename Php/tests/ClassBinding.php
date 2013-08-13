<?php
namespace tests{

	class ClassBinding {

		private $interfaceName;
		private $mockClassName;

		public function __construct($interfaceName, $mockClassName = null){
			$this->interfaceName = $interfaceName;
			$this->mockClassName = $mockClassName;
		}

		public function getInterfaceName(){
			return $this->interfaceName;
		}

		public function getMockClassName(){
			if(!$this->hasMockClassName()){
				$this->mockClassName = $this->createMockClassName();
			}
			return $this->mockClassName;
		}

		private function hasMockClassName(){
			return $this->mockClassName != null;
		}

		private function createMockClassName(){
			if($this->interfaceHasNamespace()){
				return $this->getNamespacedMockClassName();
			} else {
				return $this->getSimpleMockClassName();
			}
		}

		private function getSimpleMockClassName(){
			return "Mock" . $this->interfaceName;
		}

		private function getNamespacedMockClassName(){
			$lastNameSpaceSeparatorPos = $this->lastPos($this->interfaceName, '\\');
			$className = substr($this->interfaceName, $lastNameSpaceSeparatorPos);
			return "Mock" . $className;
		}

		private function interfaceHasNamespace(){
			if(strstr($this->interfaceName, '\\') === false){
				return false;
			}
			return true;
		}

		private function lastPos($haystack, $needle){
			$size = strlen($haystack);
			$pos = strpos(strrev($haystack), $needle);
			if ($pos === false){
				return false;
			}
			return $size - $pos;
		}

	}

}