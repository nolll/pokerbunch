namespace core\Validation{

	class EmailValidator extends SimpleValidator {

		public function validateSubject(){
			if(isNullOrEmpty(subject)){
				return true;
			} else if(isEmail(subject)){
				return true;
			} else {
				addError(message);
				return false;
			}
		}
		
	}

}