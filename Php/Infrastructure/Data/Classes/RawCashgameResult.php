namespace Infrastructure\Data\Classes {

	use entities\Checkpoints\Checkpoint;

	class RawCashgameResult{

		private $playerId;
		private $checkpoints;

		public function __construct($playerId){
			playerId = $playerId;
			checkpoints = array();
		}

		public function getPlayerId(){
			return playerId;
		}

        public function getCheckpoints(){
            return checkpoints;
        }

		public function addCheckpoint(Checkpoint $checkpoint){
			checkpoints[] = $checkpoint;
		}

	}

}