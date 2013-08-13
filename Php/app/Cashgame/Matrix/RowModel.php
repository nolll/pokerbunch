<?php
namespace app\Cashgame\Matrix{

	use entities\Cashgame;
	use entities\Homegame;
	use core\Util;
	use app\Cashgame\Matrix\CellModel;
	use app\Urls\PlayerDetailsUrlModel;
	use core\Globalization;
	use entities\CashgameTotalResult;
	use entities\CashgameSuite;
	use entities\Player;

	class RowModel{

		public $rank;
		public $name;
		public $urlEncodedName;
		public $totalResult;
		public $resultClass;
		public $gameTime;
		public $winRate;
        public $playerUrl;
		public $player;
		public $currency;
		public $cellModels;

		public function __construct(Homegame $homegame, CashgameSuite $suite, CashgameTotalResult $result = null, $rank){
			$this->rank = $rank;
			$cashgames = $suite->getCashgames();

			if($result != null){
				$player = $result->getPlayer();
				if($player != null){
					$this->name = $player->getDisplayName();
					$this->urlEncodedName = rawurlencode($player->getDisplayName());
					$this->playerUrl = new PlayerDetailsUrlModel($homegame, $player);
					$this->cellModels = $this->getCellModels($cashgames, $player);
				}

				$winnings = $result->getWinnings();
				$this->totalResult = Globalization::formatResult($homegame->getCurrency(), $winnings);
				$this->resultClass = Util::getWinningsCssClass($winnings);
			}
		}

		private function getCellModels(array $cashgames, Player $player){
			$models = array();
			if($cashgames != null){
				foreach($cashgames as $cashgame){ /** @var Cashgame $cashgame */
					$result = $cashgame->getResult($player);
					$models[] = new CellModel($cashgame, $result);
				}
			}
			return $models;
		}

	}

}