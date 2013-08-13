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
				showResult = true;
				showTransactions = $result.getBuyin() > 0;
				buyin = $result.getBuyin();
				cashout = $result.getStack();
				winnings = Util::formatWinnings($result.getWinnings());
				resultClass = Util::getWinningsCssClass($result.getWinnings());
				hasBestResult = $cashgame.isBestResult($result);
			} else {
				showResult = false;
			}
		}

	}

}