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
			homegame = $homegame;
			cashgame = $cashgame;
			player = $player;

			heading = sprintf('Cashgame %1$s', Globalization::formatShortDate($cashgame.getStartTime(), true));
			location = $cashgame.getLocation();

			$duration = $cashgame.getDuration();
			duration = Globalization::formatDuration($duration);
			durationEnabled = $duration > 0;

			$startTime = $cashgame.getStartTime();
			showStartTime = $cashgame.getStatus() >= GameStatus::running && $startTime != null;
			if(showStartTime){
				startTime = Globalization::formatTime($startTime);
			}

			$endTime = $cashgame.getEndTime();
			showEndTime = $cashgame.getStatus() >= GameStatus::finished && $endTime != null;
			if(showEndTime){
				endTime = Globalization::formatTime($endTime);
			}

			status = GameStatus::getName($cashgame.getStatus());

			$results = getResults($cashgame);
			results = $results;

			setUrls();
			setButtons($results, $player, $isManager);

			resultTableModel = new ResultTableModel(homegame, cashgame);

			chartDataUrl = new CashgameDetailsChartJsonUrlModel($homegame, $cashgame);
		}

		private function setButtons($results, Player $player, $isManager){
			$numResults = count($results);
			enablePublish = publishButtonVisible($isManager, $numResults);
			enableUnpublish = unpublishButtonVisible($isManager, $numResults);
			enableEdit = $isManager;
			enableCheckpointsButton = cashgame.isInGame($player);
		}

		private function setUrls(){
			publishUrl = new CashgamePublishUrlModel(homegame, cashgame);
			unpublishUrl = new CashgameUnpublishUrlModel(homegame, cashgame);
			editUrl = new CashgameEditUrlModel(homegame, cashgame);
			checkpointsUrl = new CashgameActionUrlModel(homegame, cashgame, player);
		}

		private function getResults(Cashgame $cashgame){
			$results = $cashgame.getResults();
			usort($results, 'entities\CashgameResultComparer::compareName');
			return $results;
		}

		private function publishButtonVisible($isManager, $numResults){
			return $isManager && cashgame.getStatus() == GameStatus::finished && $numResults >= 2;
		}

		private function unpublishButtonVisible($isManager){
			return $isManager && cashgame.getStatus() == GameStatus::published;
		}

	}

}