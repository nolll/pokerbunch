<?php
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
			$player = $result->getPlayer();
			if($player != null){
				$this->name = $player->getDisplayName();
				$this->playerUrl = new CashgameActionUrlModel($homegame, $cashgame, $player);
			}
			$this->buyin = Globalization::formatCurrency($homegame->getCurrency(), $result->getBuyin());
			$this->cashout = Globalization::formatCurrency($homegame->getCurrency(), $result->getStack());
			$winnings = $result->getWinnings();
			$this->winnings = Globalization::formatResult($homegame->getCurrency(), $winnings);
			$this->winningsClass = Util::getWinningsCssClass($winnings);
			$duration = $result->getPlayedTime();
			if($duration > 0){
				$winrate = $winrate = round($winnings / $duration * 60);
				$this->winrate = Globalization::formatWinrate($homegame->getCurrency(), $winrate);
			} else {
				$this->winrate = '';
			}
		}

	}

}