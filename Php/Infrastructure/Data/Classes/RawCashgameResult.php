namespace Infrastructure\Data\Classes {

	use entities\Checkpoints\Checkpoint;

	class RawCashgameResult{

		private $playerId;
		private $checkpoints;

		public function __construct($playerId){
			$this->playerId = $playerId;
			$this->checkpoints = array();
		}

		public function getPlayerId(){
			return $this->playerId;
		}

        public function getCheckpoints(){
            return $this->checkpoints;
        }

		public function addCheckpoint(Checkpoint $checkpoint){
			$this->checkpoints[] = $checkpoint;
		}

	}

}