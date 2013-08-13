namespace app\Cashgame\Leaderboard{

	use app\Cashgame\CashgameNavigationModel;
	use core\HomegamePageModel;
	use entities\Cashgame;
	use entities\CashgameSuite;
	use entities\Homegame;
	use Domain\Classes\User;

	class LeaderboardModel extends HomegamePageModel {

		public $tableModel;
		public $cashgameNavModel;

		public function __construct(User $user,
									Homegame $homegame,
									CashgameSuite $suite,
									array $years = null,
									$year = null,
									Cashgame $runningGame = null){
			parent::__construct($user, $homegame, $runningGame);
			$this->tableModel = new TableModel($homegame, $suite);
			$this->cashgameNavModel = new CashgameNavigationModel($homegame, 'leaderboard', $years, $year, $runningGame);
		}

	}

}