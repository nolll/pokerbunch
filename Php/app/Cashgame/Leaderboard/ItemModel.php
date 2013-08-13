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
			rank = $rank;
			if($result != null){
				$winnings = $result.getWinnings();
				totalResult = Globalization::formatResult($homegame.getCurrency(), $winnings);
				resultClass = Util::getWinningsCssClass($winnings);
				gameTime = Globalization::formatDuration($result.getTimePlayed());
				winRate = Globalization::formatWinrate($homegame.getCurrency(), $result.getWinRate());
				$player = $result.getPlayer();
				if($player != null){
					name = $player.getDisplayName();
					urlEncodedName = rawurlencode($player.getDisplayName());
					playerUrl = new PlayerDetailsUrlModel($homegame, $player);
				}
			}
		}

	}

}