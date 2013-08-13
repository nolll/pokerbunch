<?php
namespace app\Cashgame\Listing\CashgameTableItem{
	use entities\Homegame;
	use app\Cashgame\StatusModel;
	use entities\GameStatus;
	use core\Globalization;
	use app\Urls\CashgameDetailsUrlModel;
	use entities\Cashgame;

	class CashgameTableItemModel{

		public $playerCount;
		public $location;
		public $duration;
		public $turnover;
		public $avgBuyin;
		public $detailsUrl;
		public $displayDate;
		public $publishedClass;

		public function __construct(Homegame $homegame, Cashgame $cashgame, $showYear){
			$playerCount = $cashgame->getNumPlayers();
			$this->playerCount = $playerCount;
			$this->location = $cashgame->getLocation();
			$this->duration = $this->getDuration($cashgame);
			$this->turnover = $this->getTurnover($homegame, $cashgame);
			$this->avgBuyin = $this->getAvgBuyin($homegame, $cashgame, $playerCount);
			$this->detailsUrl = new CashgameDetailsUrlModel($homegame, $cashgame);
			$this->displayDate = Globalization::formatShortDate($cashgame->getStartTime(), $showYear);
			$this->publishedClass = $this->getPublishedClass($cashgame);
		}

		private function getDuration(Cashgame $cashgame){
			$duration = $cashgame->getDuration();
			if($duration > 0){
				return Globalization::formatDuration($duration);
			} else {
				return '';
			}
		}

		private function getTurnover(Homegame $homegame, Cashgame $cashgame){
			return Globalization::formatCurrency($homegame->getCurrency(), $cashgame->getTurnover());
		}

		private function getAvgBuyin(Homegame $homegame, Cashgame $cashgame, $playerCount){
			return Globalization::formatCurrency($homegame->getCurrency(), $cashgame->getAverageBuyin());
		}

		private function getPublishedClass(Cashgame $cashgame){
			return $cashgame->getStatus() == GameStatus::published ? '' : 'unpublished';
		}

	}

}