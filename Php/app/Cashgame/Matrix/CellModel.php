<?php
namespace app\Cashgame\Matrix{

	use entities\Cashgame;
	use core\Util;
	use entities\CashgameResult;

	class CellModel{

		public $buyin;
		public $cashout;
		public $winnings;
		public $showResult;
		public $resultClass;
		public $showTransactions;
		public $hasBestResult;

		public function __construct(Cashgame $cashgame, CashgameResult $result = null){
			if($result != null){
				$this->showResult = true;
				$this->showTransactions = $result->getBuyin() > 0;
				$this->buyin = $result->getBuyin();
				$this->cashout = $result->getStack();
				$this->winnings = Util::formatWinnings($result->getWinnings());
				$this->resultClass = Util::getWinningsCssClass($result->getWinnings());
				$this->hasBestResult = $cashgame->isBestResult($result);
			} else {
				$this->showResult = false;
			}
		}

	}

}