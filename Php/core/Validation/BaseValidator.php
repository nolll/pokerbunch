namespace core\Validation{

	abstract class BaseValidator implements Validator{

		protected $errors;

		public function addError($message){
			ensureErrorArray();
			errors[] = $message;
		}

		public function getErrors(){
			ensureErrorArray();
			return errors;
		}

		protected function ensureErrorArray(){
			if(errors == null){
				errors = array();
			}
		}

		abstract protected function validate();

	}

}