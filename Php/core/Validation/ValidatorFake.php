namespace core\Validation{

	class ValidatorFake implements Validator{

		private $isValid;
		private $errors;

		public function __construct($isValid = false){
			isValid = $isValid;
			errors = array();
		}

		public function isValid(){
			return isValid;
		}

		public function setIsValid($isValid){
			isValid = $isValid;
		}

		public function getErrors(){
			return errors;
		}

		public function setErrors($errors){
			errors = $errors;
		}

	}

}