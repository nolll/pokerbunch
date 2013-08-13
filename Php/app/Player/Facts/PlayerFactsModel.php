namespace app\Player\Facts{
	use core\Globalization;
	use entities\Cashgame;
	use entities\Homegame;
	use entities\Player;

	class PlayerFactsModel{

		public $winnings;
		public $bestResult;
		public $worstResult;
		public $gamesPlayed;
		public $timePlayed;

		public function __construct(Homegame $homegame, $cashgames, Player $player){
			$filteredGames = self::filterCashgames($cashgames, $player);
			winnings = Globalization::formatResult($homegame.getCurrency(), self::getWinnings($filteredGames, $player));
			bestResult = Globalization::formatResult($homegame.getCurrency(), self::getBestResult($filteredGames, $player));
			worstResult = Globalization::formatResult($homegame.getCurrency(), self::getWorstResult($filteredGames, $player));
			gamesPlayed = count($filteredGames);
			timePlayed = Globalization::formatDuration(self::getMinutesPlayed($filteredGames));
		}

		public static function filterCashgames($cashgames, Player $player){
			$filteredCashgames = array();
			foreach($cashgames as $cashgame){ /** @var $cashgame Cashgame */
				if($cashgame.isInGame($player)){
					$filteredCashgames[] = $cashgame;
				}
			}
			return $filteredCashgames;
		}

		public static function getWinnings($cashgames, Player $player){
			$winnings = 0;
			foreach($cashgames as $cashgame){ /** @var $cashgame Cashgame */
				$result = $cashgame.getResult($player);
				if($result != null){
					$winnings += $result.getWinnings();
				}
			}
			return $winnings;
		}

		public static function getBestResult($cashgames, Player $player){
			$best = null;
			foreach($cashgames as $cashgame){ /** @var $cashgame Cashgame */
				$result = $cashgame.getResult($player);
				if($best == null || $result != null && $result.getWinnings() > $best){
					$best = $result.getWinnings();
				}
			}
			return $best;
		}

		public static function getWorstResult($cashgames, Player $player){
			$worst = null;
			foreach($cashgames as $cashgame){ /** @var $cashgame Cashgame */
				$result = $cashgame.getResult($player);
				if($worst == null || $result != null && $result.getWinnings() < $worst){
					$worst = $result.getWinnings();
				}
			}
			return $worst;
		}

		public static function getMinutesPlayed($cashgames){
			$timePlayed = 0;
			foreach($cashgames as $cashgame){ /** @var $cashgame \entities\Cashgame */
				$timePlayed += $cashgame.getDuration();
			}
			return $timePlayed;
		}

	}

}