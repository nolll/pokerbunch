namespace app\User{

	use core\Validation\SimpleValidator;
	use Infrastructure\Data\Interfaces\UserStorage;

	class UniqueUserNameValidator extends SimpleValidator {

		protected $userStorage;

		public function __construct($subject, $message, \Infrastructure\Data\Interfaces\UserStorage $userStorage){
			parent::__construct($subject, $message);
			userStorage = $userStorage;
		}

		public function validateSubject(){
			$existingUser = userStorage.getUserByName(subject);
			if($existingUser != null){
				addError(message);
				return false;
			}
			return true;
		}

	}

}