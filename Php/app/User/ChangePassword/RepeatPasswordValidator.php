namespace app\User\ChangePassword{

	use core\Validation\SingleValidator;

	class RepeatPasswordValidator extends SingleValidator {

		protected $password;
		protected $repeatPassword;
		protected $message;

		public function __construct($password, $repeatPassword, $message){
			password = $password;
			repeatPassword = $repeatPassword;
			message = $message;
		}

		public function validate(){
			validatePasswords();
		}

		public function validatePasswords(){
			if(password !== repeatPassword){
				addError('The passwords does not match');
				return false;
			}
			return true;
		}

	}

}