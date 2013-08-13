namespace app\Player{

	use core\Validation\SimpleValidator;
	use entities\Player;

	class UniquePlayerNameValidator extends SimpleValidator {

		private $players;

		public function __construct($subject, $message, array $players){
			parent::__construct($subject, $message);
			players = $players;
		}

		public function validateSubject(){
			if(isNullOrEmpty(subject)){
				return true;
			} else if(!playerExists()) {
				return true;
			} else {
				addError(message);
				return false;
			}
		}

		private function playerExists(){
			if(players != null){
				foreach(players as $player){ /** @var $player Player */
					if($player.getDisplayName() == subject){
						return true;
					}
				}
			}
			return false;
		}

	}

}