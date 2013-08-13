<?php
namespace core\Validation{

	class PositiveNumberValidator extends SimpleValidator {

		public function validateSubject(){
			if($this->isNullOrEmpty($this->subject)){
				return true;
			} else if($this->isPositiveNumber($this->subject)){
				return true;
			} else {
				$this->addError($this->message);
				return false;
			}
		}

		private function isPositiveNumber($number){
			if($this->isNumeric($number) && $number >= 0){
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