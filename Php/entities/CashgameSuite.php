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
			cashgames = array();
			totalResults = array();
		}

		/**
		 * @return CashgameTotalResult[]
		 */
		public function getTotalResults(){
			return totalResults;
		}

		function setTotalResults(array $totalResults){
			totalResults = $totalResults;
		}

		/**
		 * @return Cashgame[]
		 */
		public function getCashgames(){
			return cashgames;
		}

		public function setCashgames(array $cashgames){
			cashgames = $cashgames;
		}

		public function getGameCount(){
			return gameCount;
		}

		public function setGameCount($gameCount){
			gameCount = $gameCount;
		}

		public function getBestTotalResult(){
			return bestTotalResult;
		}

		public function setBestTotalResult(CashgameTotalResult $totalResult = null){
			bestTotalResult = $totalResult;
		}

		public function getBestResult(){
			return bestResult;
		}

		public function setBestResult(CashgameResult $result = null){
			bestResult = $result;
		}

		public function getWorstResult(){
			return worstResult;
		}

		public function setWorstResult(CashgameResult $result = null){
			worstResult = $result;
		}

		public function getMostTimeResult(){
			return mostTimeResult;
		}

		public function setMostTimeResult(CashgameTotalResult $totalResult = null){
			mostTimeResult = $totalResult;
		}

		/**
		 * @return int
		 */
		public function getTotalGametime(){
			return totalGameTime;
		}

		public function setTotalGametime($gameTime){
			totalGameTime = $gameTime;
		}

	}

}