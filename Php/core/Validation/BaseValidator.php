<?php
namespace core\Validation{

	abstract class BaseValidator implements Validator{

		protected $errors;

		public function addError($message){
			$this->ensureErrorArray();
			$this->errors[] = $message;
		}

		public function getErrors(){
			$this->ensureErrorArray();
			return $this->errors;
		}

		protected function ensureErrorArray(){
			if($this->errors == null){
				$this->errors = array();
			}
		}

		abstract protected function validate();

	}

}