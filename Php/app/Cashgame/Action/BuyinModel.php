namespace app\Cashgame\Action{

	use core\Globalization;
	use core\HomegamePageModel;
	use entities\Cashgame;
	use Domain\Classes\User;
	use entities\CashgameResult;
	use entities\Homegame;
	use app\Urls\CashgameBuyinUrlModel;
	use entities\Player;

    class BuyinModel extends HomegamePageModel {

		/** @var CashgameResult */
		private $result;
		public $buyinUrl;
		public $stackFieldEnabled;
		public $buyinAmount;

        public function __construct(User $user,
									Homegame $homegame,
									Player $player,
									array $years = null,
									Cashgame $runningGame,
									$postedAmount = null){
			parent::__construct($user, $homegame, $runningGame);
            $this->result = $runningGame->getResult($player);
			$this->buyinUrl = new CashgameBuyinUrlModel($homegame, $player);
			$this->stackFieldEnabled = $runningGame->isInGame($player);
			$this->buyinAmount = $postedAmount != null ? $postedAmount : $homegame->getDefaultBuyin();
        }

	}

}