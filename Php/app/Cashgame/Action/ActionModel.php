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
			homegame = $homegame;
			cashgame = $cashgame;
			player = $player;
			result = $result;
			role = $role;
			$dateString = Globalization::formatShortDate($cashgame.getStartTime(), true);
			heading = sprintf('Cashgame %1$s, %2$s', $dateString, $player.getDisplayName());
			checkpoints = getCheckpointModels();
			chartDataUrl = new CashgameActionChartJsonUrlModel($homegame, $cashgame, $player);
        }

		private function getCheckpointModels(){
			$models = array();
			$checkpoints = getCheckpoints();
			foreach($checkpoints as $checkpoint){
				$models[] = new CheckpointModel(homegame, cashgame, player, $checkpoint, role);
			}
			return $models;
		}

		private function getCheckpoints(){
			if(playerIsInGame()){
				return result.getCheckpoints();
			} else {
				return array();
			}
		}

		private function playerIsInGame(){
			return result != null;
		}

	}

}