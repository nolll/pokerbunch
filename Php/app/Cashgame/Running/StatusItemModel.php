namespace app\Cashgame\Running{
	use core\Timer;
	use entities\Homegame;
	use app\Urls\CashgameReportUrlModel;
	use app\Urls\CashgameCashoutUrlModel;
	use app\Urls\CashgameBuyinUrlModel;
	use core\Util;
	use app\Urls\CashgameActionUrlModel;
	use core\Globalization;
	use entities\CashgameResult;
	use entities\Cashgame;

	class StatusItemModel{

		private $result;
		public $name;
		public $playerUrl;
		public $buyin;
		public $stack;
		public $winnings;
		public $time;
		public $winningsClass;
		public $managerButtonsEnabled;
		public $buyinUrl;
		public $reportUrl;
		public $cashoutUrl;
		public $hasCashedOut;

		public function __construct(Homegame $homegame,
									Cashgame $cashgame,
									CashgameResult $result,
									$isManager,
									Timer $timer){
			result = $result;
			$player = $result.getPlayer();
			if($player != null){
				name = $player.getDisplayName();
				playerUrl = new CashgameActionUrlModel($homegame, $cashgame, $player);
				if($isManager){
					buyinUrl = new CashgameBuyinUrlModel($homegame, $player);
					reportUrl = new CashgameReportUrlModel($homegame, $player);
					cashoutUrl = new CashgameCashoutUrlModel($homegame, $player);
				}
			}
			buyin = Globalization::formatCurrency($homegame.getCurrency(), $result.getBuyin());
			stack = Globalization::formatCurrency($homegame.getCurrency(), $result.getStack());
			$winnings = $result.getWinnings();
			winnings = Globalization::formatResult($homegame.getCurrency(), $winnings);
			$lastReportedTime = $result.getLastReportTime();
			if($lastReportedTime != null){
				$secondsAgo = $timer.getTime().getTimestamp() - $lastReportedTime.getTimestamp();
				time = Globalization::formatTimespan($secondsAgo);
			}
			winningsClass = Util::getWinningsCssClass($winnings);
			hasCashedOut = $result.getCashoutTime() != null;
			managerButtonsEnabled = $isManager;
		}

	}

}