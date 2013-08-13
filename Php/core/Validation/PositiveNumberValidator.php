namespace core\Validation{

	class PositiveNumberValidator extends SimpleValidator {

		public function validateSubject(){
			if(isNullOrEmpty(subject)){
				return true;
			} else if(isPositiveNumber(subject)){
				return true;
			} else {
				addError(message);
				return false;
			}
		}

		private function isPositiveNumber($number){
			if(isNumeric($number) && $number >= 0){
				return true;
			} else {
				return false;
			}
		}

		private function isNumeric($number){
			if(is_numeric($number)){
				return true;
			} else {
				return false;
			}
		}
		
	}

}