namespace app\Cashgame\Action{

	use app\Urls\CashgameCashoutUrlModel;
	use core\Globalization;
	use core\HomegamePageModel;
	use entities\Cashgame;
	use Domain\Classes\User;
	use entities\Homegame;
	use entities\Player;

    class CashoutModel extends HomegamePageModel {

		public $cashoutUrl;
		public $cashoutAmount;

        public function __construct(User $user,
									Homegame $homegame,
									Player $player,
									array $years = null,
									Cashgame $runningGame,
									$postedAmount){
			parent::__construct($user, $homegame, $runningGame);
			cashoutUrl = new CashgameCashoutUrlModel($homegame, $player);
			cashoutAmount = $postedAmount != null ? $postedAmount : '';
        }

	}

}