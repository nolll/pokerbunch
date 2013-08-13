namespace entities{

	class CashgameSuite {

		/** @var Cashgame[] */
		private $cashgames;

		/** @var CashgameTotalResult[] */
		private $totalResults;

		/** @var int */
		private $totalGameTime;

		/** @var CashgameResult */
		private $bestResult;

		/** @var CashgameResult */
		private $worstResult;

		/** @var CashgameTotalResult */
		private $bestTotalResult;

		/** @var CashgameTotalResult */
		private $mostTimeResult;

		private $gameCount;

		public function __construct(){
			$this->cashgames = array();
			$this->totalResults = array();
		}

		/**
		 * @return CashgameTotalResult[]
		 */
		public function getTotalResults(){
			return $this->totalResults;
		}

		function setTotalResults(array $totalResults){
			$this->totalResults = $totalResults;
		}

		/**
		 * @return Cashgame[]
		 */
		public function getCashgames(){
			return $this->cashgames;
		}

		public function setCashgames(array $cashgames){
			$this->cashgames = $cashgames;
		}

		public function getGameCount(){
			return $this->gameCount;
		}

		public function setGameCount($gameCount){
			$this->gameCount = $gameCount;
		}

		public function getBestTotalResult(){
			return $this->bestTotalResult;
		}

		public function setBestTotalResult(CashgameTotalResult $totalResult = null){
			$this->bestTotalResult = $totalResult;
		}

		public function getBestResult(){
			return $this->bestResult;
		}

		public function setBestResult(CashgameResult $result = null){
			$this->bestResult = $result;
		}

		public function getWorstResult(){
			return $this->worstResult;
		}

		public function setWorstResult(CashgameResult $result = null){
			$this->worstResult = $result;
		}

		public function getMostTimeResult(){
			return $this->mostTimeResult;
		}

		public function setMostTimeResult(CashgameTotalResult $totalResult = null){
			$this->mostTimeResult = $totalResult;
		}

		/**
		 * @return int
		 */
		public function getTotalGametime(){
			return $this->totalGameTime;
		}

		public function setTotalGametime($gameTime){
			$this->totalGameTime = $gameTime;
		}

	}

}