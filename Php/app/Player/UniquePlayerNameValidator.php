namespace app\Player{

	use core\Validation\SimpleValidator;
	use entities\Player;

	class UniquePlayerNameValidator extends SimpleValidator {

		private $players;

		public function __construct($subject, $message, array $players){
			parent::__construct($subject, $message);
			$this->players = $players;
		}

		public function validateSubject(){
			if($this->isNullOrEmpty($this->subject)){
				return true;
			} else if(!$this->playerExists()) {
				return true;
			} else {
				$this->addError($this->message);
				return false;
			}
		}

		private function playerExists(){
			if($this->players != null){
				foreach($this->players as $player){ /** @var $player Player */
					if($player->getDisplayName() == $this->subject){
						return true;
					}
				}
			}
			return false;
		}

	}

}