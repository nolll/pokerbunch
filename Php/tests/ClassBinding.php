namespace tests{

	class ClassBinding {

		private $interfaceName;
		private $mockClassName;

		public function __construct($interfaceName, $mockClassName = null){
			interfaceName = $interfaceName;
			mockClassName = $mockClassName;
		}

		public function getInterfaceName(){
			return interfaceName;
		}

		public function getMockClassName(){
			if(!hasMockClassName()){
				mockClassName = createMockClassName();
			}
			return mockClassName;
		}

		private function hasMockClassName(){
			return mockClassName != null;
		}

		private function createMockClassName(){
			if(interfaceHasNamespace()){
				return getNamespacedMockClassName();
			} else {
				return getSimpleMockClassName();
			}
		}

		private function getSimpleMockClassName(){
			return "Mock" . interfaceName;
		}

		private function getNamespacedMockClassName(){
			$lastNameSpaceSeparatorPos = lastPos(interfaceName, '\\');
			$className = substr(interfaceName, $lastNameSpaceSeparatorPos);
			return "Mock" . $className;
		}

		private function interfaceHasNamespace(){
			if(strstr(interfaceName, '\\') === false){
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