namespace app\Cashgame\Details\ResultTableItem{
	use entities\Homegame;
	use core\Util;
	use app\Urls\CashgameActionUrlModel;
	use core\Globalization;
	use entities\CashgameResult;
	use entities\Cashgame;

	class ResultTableItemModel{

		public $name;
		public $playerUrl;
		public $buyin;
		public $cashout;
		public $winnings;
		public $winningsClass;
		public $winrate;

		public function __construct(Homegame $homegame,
									Cashgame $cashgame,
									CashgameResult $result){
			$player = $result.getPlayer();
			if($player != null){
				name = $player.getDisplayName();
				playerUrl = new CashgameActionUrlModel($homegame, $cashgame, $player);
			}
			buyin = Globalization::formatCurrency($homegame.getCurrency(), $result.getBuyin());
			cashout = Globalization::formatCurrency($homegame.getCurrency(), $result.getStack());
			$winnings = $result.getWinnings();
			winnings = Globalization::formatResult($homegame.getCurrency(), $winnings);
			winningsClass = Util::getWinningsCssClass($winnings);
			$duration = $result.getPlayedTime();
			if($duration > 0){
				$winrate = $winrate = round($winnings / $duration * 60);
				winrate = Globalization::formatWinrate($homegame.getCurrency(), $winrate);
			} else {
				winrate = '';
			}
		}

	}

}