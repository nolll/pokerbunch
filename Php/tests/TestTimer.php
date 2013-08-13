namespace tests{

	class TestTimer {

		private $startTime;

		public function __construct(){
			startTime = microtime(true);
		}

		public function measure(){
			$measureTime = microtime(true);
			return $measureTime - startTime;
		}

	}

}