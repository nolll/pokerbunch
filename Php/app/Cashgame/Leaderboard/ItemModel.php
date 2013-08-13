namespace app\Cashgame\Leaderboard{

	use entities\Homegame;
	use app\Urls\PlayerDetailsUrlModel;
	use core\Util;
	use core\Globalization;
	use entities\CashgameTotalResult;

	class ItemModel{

		public $rank;
		public $name;
		public $urlEncodedName;
		public $totalResult;
		public $resultClass;
		public $gameTime;
		public $winRate;
        public $playerUrl;

		public function __construct(Homegame $homegame, CashgameTotalResult $result = null, $rank){
			$this->rank = $rank;
			if($result != null){
				$winnings = $result->getWinnings();
				$this->totalResult = Globalization::formatResult($homegame->getCurrency(), $winnings);
				$this->resultClass = Util::getWinningsCssClass($winnings);
				$this->gameTime = Globalization::formatDuration($result->getTimePlayed());
				$this->winRate = Globalization::formatWinrate($homegame->getCurrency(), $result->getWinRate());
				$player = $result->getPlayer();
				if($player != null){
					$this->name = $player->getDisplayName();
					$this->urlEncodedName = rawurlencode($player->getDisplayName());
					$this->playerUrl = new PlayerDetailsUrlModel($homegame, $player);
				}
			}
		}

	}

}