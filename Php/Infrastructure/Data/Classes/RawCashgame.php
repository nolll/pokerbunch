namespace Infrastructure\Data\Classes {

	use Infrastructure\Data\Classes\RawCashgameResult;

	class RawCashgame{

		private $location;
		/** @var RawCashgameResult[] */
		private $results;
		private $id;
		private $status;
		private $date;

		public function __construct($id, $location, $status, $date){
			results = array();
			id = $id;
			location = $location;
			status = $status;
			date = $date;
		}

		public function getId(){
			return id;
		}

		public function getLocation(){
			return location;
		}

		public function getStatus(){
			return status;
		}

		public function getDate(){
			return date;
		}

		/**
		 * @param RawCashgameResult $result
		 * @return void
		 */
		public function addResult(RawCashgameResult $result){
			results[] = $result;
		}

		/**
		 * @return RawCashgameResult[]
		 */
		public function getResults(){
			return results;
		}

	}

}