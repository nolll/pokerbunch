<?php
namespace app\Cashgame\Matrix{

	use app\Cashgame\CashgameNavigationModel;
	use core\HomegamePageModel;
	use entities\Homegame;
	use Domain\Classes\User;
	use entities\Cashgame;
	use entities\CashgameSuite;

	class MatrixModel extends HomegamePageModel {

		public $cashgameNavModel;
		public $tableModel;
		public $cashgameIsRunning;
		public $cashgameUrl;

		public function __construct(User $user,
									Homegame $homegame,
									CashgameSuite $suite,
									array $years = null,
									$year = null,
									Cashgame $runningGame = null){
			parent::__construct($user, $homegame, $runningGame);
			$this->tableModel = new TableModel($homegame, $suite);
			$this->cashgameNavModel = new CashgameNavigationModel($homegame, 'matrix', $years, $year, $runningGame);
		}

	}

}