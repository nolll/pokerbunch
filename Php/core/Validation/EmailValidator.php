namespace core\Validation{

	class EmailValidator extends SimpleValidator {

		public function validateSubject(){
			if($this->isNullOrEmpty($this->subject)){
				return true;
			} else if($this->isEmail($this->subject)){
				return true;
			} else {
				$this->addError($this->message);
				return false;
			}
		}
		
	}

}