<?php
namespace app\Cashgame\Listing{

	use app\Cashgame\CashgameNavigationModel;
	use core\HomegamePageModel;
	use entities\Cashgame;
	use entities\Homegame;
	use Domain\Classes\User;
	use app\Cashgame\Listing\CashgameTable\CashgameTableModel;

	class CashgameListingModel extends HomegamePageModel {

		public $listTableModel;
		public $cashgameNavModel;

		public function __construct(User $user,
									Homegame $homegame,
									array $cashgames,
									array $years = null,
									$year = null,
									Cashgame $runningGame = null){
			parent::__construct($user, $homegame, $runningGame);
			$this->listTableModel = new CashgameTableModel($homegame, $cashgames);
			$this->cashgameNavModel = new CashgameNavigationModel($homegame, 'listing', $years, $year, $runningGame);
		}

	}

}