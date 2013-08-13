namespace app\Cashgame\Action{

	use app\Urls\CashgameReportUrlModel;
	use core\Globalization;
	use core\HomegamePageModel;
	use entities\Cashgame;
	use Domain\Classes\User;
	use entities\Homegame;
	use entities\Player;

    class ReportModel extends HomegamePageModel {

		public $reportUrl;
		public $reportAmount;

        public function __construct(User $user,
									Homegame $homegame,
									Player $player,
									array $years = null,
									Cashgame $runningGame,
									$postedAmount = null){
			parent::__construct($user, $homegame, $runningGame);
			$this->reportUrl = new CashgameReportUrlModel($homegame, $player);
			$this->reportAmount = $postedAmount != null ? $postedAmount : '';
        }

	}

}