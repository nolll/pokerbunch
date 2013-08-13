namespace app\User{

	use core\Validation\SimpleValidator;
	use Infrastructure\Data\Interfaces\UserStorage;

	class UniqueEmailValidator extends SimpleValidator {

		protected $userStorage;

		public function __construct($subject, $message, \Infrastructure\Data\Interfaces\UserStorage $userStorage){
			parent::__construct($subject, $message);
			$this->userStorage = $userStorage;
		}

		public function validateSubject(){
			$existingUser = $this->userStorage->getUserByEmail($this->subject);
			if($existingUser != null){
				$this->addError($this->message);
				return false;
			}
			return true;
		}

	}

}