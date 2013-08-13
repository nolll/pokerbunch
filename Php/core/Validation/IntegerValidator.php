<?php
namespace core\Validation{

	use core\Util;

	class IntegerValidator extends SimpleValidator {

		public function validateSubject(){
			if($this->isNullOrEmpty($this->subject)){
				return true;
			} else if(Util::isInteger($this->subject)){
				return true;
			} else {
				$this->addError($this->message);
				return false;
			}
		}

	}

}