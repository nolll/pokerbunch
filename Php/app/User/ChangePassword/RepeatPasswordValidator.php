namespace app\User\ChangePassword{

	use core\Validation\SingleValidator;

	class RepeatPasswordValidator extends SingleValidator {

		protected $password;
		protected $repeatPassword;
		protected $message;

		public function __construct($password, $repeatPassword, $message){
			$this->password = $password;
			$this->repeatPassword = $repeatPassword;
			$this->message = $message;
		}

		public function validate(){
			$this->validatePasswords();
		}

		public function validatePasswords(){
			if($this->password !== $this->repeatPassword){
				$this->addError('The passwords does not match');
				return false;
			}
			return true;
		}

	}

}