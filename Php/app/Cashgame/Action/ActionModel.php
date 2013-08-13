namespace app\Cashgame\Action{

	use app\Urls\CashgameActionChartJsonUrlModel;
	use core\HomegamePageModel;
	use entities\Cashgame;
	use core\Globalization;
	use entities\CashgameResult;
	use Domain\Classes\User;
	use entities\GameStatus;
	use entities\Homegame;
	use entities\Player;

    class ActionModel extends HomegamePageModel {

		private $homegame;
		private $cashgame;
		private $player;
		private $role;
		/** @var CashgameResult */
		private $result;
        public $checkpoints;
		public $cashoutUrl;
		public $chartDataUrl;
		public $heading;

        public function __construct(User $user,
									Homegame $homegame,
									Cashgame $cashgame,
									Player $player,
									CashgameResult $result,
									$role,
									array $years = null,
									Cashgame $runningGame = null){
			parent::__construct($user, $homegame, $runningGame);
			$this->homegame = $homegame;
			$this->cashgame = $cashgame;
			$this->player = $player;
			$this->result = $result;
			$this->role = $role;
			$dateString = Globalization::formatShortDate($cashgame->getStartTime(), true);
			$this->heading = sprintf('Cashgame %1$s, %2$s', $dateString, $player->getDisplayName());
			$this->checkpoints = $this->getCheckpointModels();
			$this->chartDataUrl = new CashgameActionChartJsonUrlModel($homegame, $cashgame, $player);
        }

		private function getCheckpointModels(){
			$models = array();
			$checkpoints = $this->getCheckpoints();
			foreach($checkpoints as $checkpoint){
				$models[] = new CheckpointModel($this->homegame, $this->cashgame, $this->player, $checkpoint, $this->role);
			}
			return $models;
		}

		private function getCheckpoints(){
			if($this->playerIsInGame()){
				return $this->result->getCheckpoints();
			} else {
				return array();
			}
		}

		private function playerIsInGame(){
			return $this->result != null;
		}

	}

}