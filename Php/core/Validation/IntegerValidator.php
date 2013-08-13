namespace core\Validation{

	use core\Util;

	class IntegerValidator extends SimpleValidator {

		public function validateSubject(){
			if(isNullOrEmpty(subject)){
				return true;
			} else if(Util::isInteger(subject)){
				return true;
			} else {
				addError(message);
				return false;
			}
		}

	}

}