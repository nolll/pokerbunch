<?php
namespace core\Validation{

	class RequiredValidator extends SimpleValidator {

		public function validateSubject(){
			if($this->isNullOrEmpty($this->subject)){
				$this->addError($this->message);
				return false;
			}
			return true;
		}
		
	}

}