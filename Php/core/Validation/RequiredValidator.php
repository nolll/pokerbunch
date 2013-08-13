namespace core\Validation{

	class RequiredValidator extends SimpleValidator {

		public function validateSubject(){
			if(isNullOrEmpty(subject)){
				addError(message);
				return false;
			}
			return true;
		}
		
	}

}