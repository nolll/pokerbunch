namespace app\Cashgame\Chart{

	class ChartValue{

		public $gameIndex;
		public $playerIndex;
		public $value;

		function __construct($gameIndex, $playerIndex, $value){
			$this->gameIndex = $gameIndex;
			$this->playerIndex = $playerIndex;
			$this->value = $value;
		}

	}

}