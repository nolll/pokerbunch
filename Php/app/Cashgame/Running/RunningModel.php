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
			homegame = $homegame;
			cashgame = $cashgame;
			player = $player;

			location = $cashgame.getLocation();

			if($cashgame.isStarted()){
				showStartTime = true;
				startTime = Globalization::formatTime($cashgame.getStartTime());
			} else {
				showStartTime = false;
			}

			setUrls();

			setButtons($player);

			if($cashgame.isStarted()){
				statusTableModel = new StatusTableModel(homegame, cashgame, $isManager, $timer);
				showTable = true;
			} else {
				showTable = false;
			}

			if($cashgame.isStarted()){
				chartDataUrl = new CashgameDetailsChartJsonUrlModel($homegame, $cashgame);
				showChart = true;
			} else {
				showChart = false;
			}
		}

		public function setButtons(Player $player){
			$canBeEnded = canBeEnded();
			$canReport = !$canBeEnded;
			$isInGame = cashgame.isInGame($player);

			buyinButtonEnabled = $canReport;
			reportButtonEnabled = $canReport && $isInGame;
			cashoutButtonEnabled = $isInGame;
			endGameButtonEnabled = $canBeEnded;
		}

		private function canBeEnded(){
			return cashgame.isStarted() && !cashgame.hasActivePlayers();
		}

		private function setUrls(){
			buyinUrl = new CashgameBuyinUrlModel(homegame, player);
			reportUrl = new CashgameReportUrlModel(homegame, player);
			cashoutUrl = new CashgameCashoutUrlModel(homegame, player);
			endGameUrl = new CashgameEndUrlModel(homegame);
		}

	}

}