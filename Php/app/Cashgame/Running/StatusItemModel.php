<?php
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
			$this->result = $result;
			$player = $result->getPlayer();
			if($player != null){
				$this->name = $player->getDisplayName();
				$this->playerUrl = new CashgameActionUrlModel($homegame, $cashgame, $player);
				if($isManager){
					$this->buyinUrl = new CashgameBuyinUrlModel($homegame, $player);
					$this->reportUrl = new CashgameReportUrlModel($homegame, $player);
					$this->cashoutUrl = new CashgameCashoutUrlModel($homegame, $player);
				}
			}
			$this->buyin = Globalization::formatCurrency($homegame->getCurrency(), $result->getBuyin());
			$this->stack = Globalization::formatCurrency($homegame->getCurrency(), $result->getStack());
			$winnings = $result->getWinnings();
			$this->winnings = Globalization::formatResult($homegame->getCurrency(), $winnings);
			$lastReportedTime = $result->getLastReportTime();
			if($lastReportedTime != null){
				$secondsAgo = $timer->getTime()->getTimestamp() - $lastReportedTime->getTimestamp();
				$this->time = Globalization::formatTimespan($secondsAgo);
			}
			$this->winningsClass = Util::getWinningsCssClass($winnings);
			$this->hasCashedOut = $result->getCashoutTime() != null;
			$this->managerButtonsEnabled = $isManager;
		}

	}

}