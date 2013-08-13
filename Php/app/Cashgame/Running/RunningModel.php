namespace app\Cashgame\Running{

	use app\Urls\CashgameDetailsChartJsonUrlModel;
	use app\Urls\CashgameEndUrlModel;
	use core\HomegamePageModel;
	use core\Timer;
	use entities\Homegame;
	use Domain\Classes\User;
	use app\Urls\CashgameCashoutUrlModel;
	use app\Urls\CashgameReportUrlModel;
	use app\Urls\CashgameBuyinUrlModel;
	use core\Globalization;
	use entities\Player;
	use entities\Cashgame;

	class RunningModel extends HomegamePageModel {

		private $homegame;
		private $cashgame;
		private $player;

		public $startTime;
		public $showStartTime;

		public $location;

		public $enableEnd;
		public $buyinButtonEnabled;
		public $reportButtonEnabled;
		public $cashoutButtonEnabled;
		public $endGameButtonEnabled;

		public $buyinUrl;
		public $reportUrl;
		public $cashoutUrl;
		public $endGameUrl;

		public $statusTableModel;
		public $showTable;

		public $chartDataUrl;
		public $showChart;

		public function __construct(User $user,
									Homegame $homegame,
									Cashgame $cashgame,
									Player $player,
									array $years = null,
									$isManager,
									Timer $timer,
									Cashgame $runningGame = null){
			parent::__construct($user, $homegame, $runningGame);
			$this->homegame = $homegame;
			$this->cashgame = $cashgame;
			$this->player = $player;

			$this->location = $cashgame->getLocation();

			if($cashgame->isStarted()){
				$this->showStartTime = true;
				$this->startTime = Globalization::formatTime($cashgame->getStartTime());
			} else {
				$this->showStartTime = false;
			}

			$this->setUrls();

			$this->setButtons($player);

			if($cashgame->isStarted()){
				$this->statusTableModel = new StatusTableModel($this->homegame, $this->cashgame, $isManager, $timer);
				$this->showTable = true;
			} else {
				$this->showTable = false;
			}

			if($cashgame->isStarted()){
				$this->chartDataUrl = new CashgameDetailsChartJsonUrlModel($homegame, $cashgame);
				$this->showChart = true;
			} else {
				$this->showChart = false;
			}
		}

		public function setButtons(Player $player){
			$canBeEnded = $this->canBeEnded();
			$canReport = !$canBeEnded;
			$isInGame = $this->cashgame->isInGame($player);

			$this->buyinButtonEnabled = $canReport;
			$this->reportButtonEnabled = $canReport && $isInGame;
			$this->cashoutButtonEnabled = $isInGame;
			$this->endGameButtonEnabled = $canBeEnded;
		}

		private function canBeEnded(){
			return $this->cashgame->isStarted() && !$this->cashgame->hasActivePlayers();
		}

		private function setUrls(){
			$this->buyinUrl = new CashgameBuyinUrlModel($this->homegame, $this->player);
			$this->reportUrl = new CashgameReportUrlModel($this->homegame, $this->player);
			$this->cashoutUrl = new CashgameCashoutUrlModel($this->homegame, $this->player);
			$this->endGameUrl = new CashgameEndUrlModel($this->homegame);
		}

	}

}