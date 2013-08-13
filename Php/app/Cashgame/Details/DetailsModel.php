<?php
namespace app\Cashgame\Details{

	use app\Urls\CashgameDetailsChartJsonUrlModel;
	use core\HomegamePageModel;
	use entities\Homegame;
	use Domain\Classes\User;
	use app\Urls\CashgameActionUrlModel;
	use app\Urls\CashgameEditUrlModel;
	use app\Urls\CashgameUnpublishUrlModel;
	use app\Urls\CashgamePublishUrlModel;
	use core\Globalization;
	use app\Cashgame\Details\ResultTable\ResultTableModel;
	use entities\Player;
	use entities\GameStatus;
	use entities\Cashgame;

	class DetailsModel extends HomegamePageModel {

		private $homegame;
		private $cashgame;
		private $player;

		public $heading;
		public $date;
		public $duration;
		public $startTime;
		public $endTime;

		public $location;

		public $results;

		public $showStartTime;
		public $showEndTime;

		public $enableEdit;
		public $enableCheckpointsButton;
		public $enablePublish;
		public $enableUnpublish;
		public $enableStart;
		public $enableEnd;

		public $editUrl;
		public $checkpointsUrl;
		public $publishUrl;
		public $unpublishUrl;
		public $startUrl;
		public $endUrl;

		public $status;

		public $resultTableModel;

		public $chartDataUrl;

		public function __construct(User $user,
									Homegame $homegame,
									Cashgame $cashgame,
									Player $player,
									array $years = null,
									$isManager,
									Cashgame $runningGame = null){
			parent::__construct($user, $homegame, $runningGame);
			$this->homegame = $homegame;
			$this->cashgame = $cashgame;
			$this->player = $player;

			$this->heading = sprintf('Cashgame %1$s', Globalization::formatShortDate($cashgame->getStartTime(), true));
			$this->location = $cashgame->getLocation();

			$duration = $cashgame->getDuration();
			$this->duration = Globalization::formatDuration($duration);
			$this->durationEnabled = $duration > 0;

			$startTime = $cashgame->getStartTime();
			$this->showStartTime = $cashgame->getStatus() >= GameStatus::running && $startTime != null;
			if($this->showStartTime){
				$this->startTime = Globalization::formatTime($startTime);
			}

			$endTime = $cashgame->getEndTime();
			$this->showEndTime = $cashgame->getStatus() >= GameStatus::finished && $endTime != null;
			if($this->showEndTime){
				$this->endTime = Globalization::formatTime($endTime);
			}

			$this->status = GameStatus::getName($cashgame->getStatus());

			$results = $this->getResults($cashgame);
			$this->results = $results;

			$this->setUrls();
			$this->setButtons($results, $player, $isManager);

			$this->resultTableModel = new ResultTableModel($this->homegame, $this->cashgame);

			$this->chartDataUrl = new CashgameDetailsChartJsonUrlModel($homegame, $cashgame);
		}

		private function setButtons($results, Player $player, $isManager){
			$numResults = count($results);
			$this->enablePublish = $this->publishButtonVisible($isManager, $numResults);
			$this->enableUnpublish = $this->unpublishButtonVisible($isManager, $numResults);
			$this->enableEdit = $isManager;
			$this->enableCheckpointsButton = $this->cashgame->isInGame($player);
		}

		private function setUrls(){
			$this->publishUrl = new CashgamePublishUrlModel($this->homegame, $this->cashgame);
			$this->unpublishUrl = new CashgameUnpublishUrlModel($this->homegame, $this->cashgame);
			$this->editUrl = new CashgameEditUrlModel($this->homegame, $this->cashgame);
			$this->checkpointsUrl = new CashgameActionUrlModel($this->homegame, $this->cashgame, $this->player);
		}

		private function getResults(Cashgame $cashgame){
			$results = $cashgame->getResults();
			usort($results, 'entities\CashgameResultComparer::compareName');
			return $results;
		}

		private function publishButtonVisible($isManager, $numResults){
			return $isManager && $this->cashgame->getStatus() == GameStatus::finished && $numResults >= 2;
		}

		private function unpublishButtonVisible($isManager){
			return $isManager && $this->cashgame->getStatus() == GameStatus::published;
		}

	}

}